using System.IO.Compression;
using System.Text;


class Program
{
    static void Main()
    {
        Console.Write("Введите имя файла: ");
        string filename = Console.ReadLine()!;
        Console.Write("Введите путь: ");
        string path = Console.ReadLine()!;
        FileStream? fstream = null;

        if (Directory.Exists(path))
        {
            // Проверка существования файла в указанном пути
            if (Directory.GetFiles(path).Contains(Path.Combine(path, filename)))
            {
                fstream = File.OpenRead(Path.Combine(path, filename));
            }
            else
            {
                // Поиск файла в подкаталогах
                foreach (var subdirectory in Directory.GetDirectories(path))
                {
                    string newPath = new DirectoryInfo(subdirectory).FullName;
                    if (Directory.GetFiles(newPath).Contains(Path.Combine(newPath, filename)))
                    {
                        fstream = File.OpenRead(Path.Combine(newPath, filename));
                        break;
                    }
                }
            }

            // Если файл найден
            if (fstream != null)
            {
                byte[] buffer = new byte[fstream.Length];
                fstream.Read(buffer, 0, buffer.Length);
                string textFromFile = Encoding.Default.GetString(buffer);
                Console.WriteLine(textFromFile);
                Console.WriteLine("After compress&decompress");

                // Сжатие, разжатие и создание zip-архива
                FileInfo file = new FileInfo(filename);
                CompressFile(Path.Combine(path, "ToSearch.txt"), Path.Combine(path, "ToSearch.txt.gzip"));
                DecompressFile(Path.Combine(path, "ToSearch.txt.gzip"));
                CreateZipFile(Path.Combine(path, "arch"), Path.Combine(path, "ToSearch.zip"));
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }
        }
    }

    // Создание zip-архива из файлов в указанном каталоге
    static void CreateZipFile(string sourceDirectory, string zipFile)
    {
        InitSampleFilesForZip(sourceDirectory);

        using (FileStream zipStream = File.Create(zipFile))
        {
            using (ZipArchive archive = new(zipStream, ZipArchiveMode.Create))
            {
                IEnumerable<string> files = Directory.EnumerateFiles(sourceDirectory, "*", SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    ZipArchiveEntry entry = archive.CreateEntry(Path.GetFileName(file));
                    using (FileStream inputStream = File.OpenRead(file))
                    using (Stream outputStream = entry.Open())
                    {
                        inputStream.CopyTo(outputStream);
                    }
                }
            }
        }
    }

    // Инициализация примерных файлов для zip-архива
    static void InitSampleFilesForZip(string directory)
    {
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);

            // Создание или копирование файла "file.txt" в целевую папку
            string sourceFilePath = Path.Combine(directory, "file.txt");
            if (!File.Exists(sourceFilePath))
            {
                File.WriteAllText(sourceFilePath, "Содержимое файла по умолчанию для file.txt");
            }

            for (int i = 0; i < 10; i++)
            {
                string destFileName = Path.Combine(directory, $"test{i}.txt");

                // Копирование файла "file.txt" из исходного местоположения в целевую папку
                File.Copy(sourceFilePath, destFileName, true);
            }
        }
    }

    // Сжатие файла с использованием GZip
    static void CompressFile(string fileName, string compressedFileName)
    {
        using (FileStream inputStream = File.OpenRead(fileName))
        using (FileStream outputStream = File.Create(compressedFileName))
        using (GZipStream compressStream = new GZipStream(outputStream, CompressionMode.Compress))
        {
            inputStream.CopyTo(compressStream);
        }
    }

        // Разжатие файла с использованием GZip
    static void DecompressFile(string fileName)
    {
        using (FileStream inputStream = File.OpenRead(fileName))
        using (GZipStream decompressStream = new GZipStream(inputStream, CompressionMode.Decompress))
        using (StreamReader reader = new StreamReader(decompressStream, Encoding.UTF8))
        {
            string result = reader.ReadToEnd();
            Console.WriteLine(result);
        }
    }
}