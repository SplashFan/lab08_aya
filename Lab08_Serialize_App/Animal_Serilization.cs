using System.Xml.Linq;
using Lab08_Library;
using System.Xml.Serialization;
/*
void Serialize(Animal animal, XElement program)
{
    XElement element = new XElement(animal.Name!);
    element.Add(new XElement("HideFromOtherAnimals", animal.HideFromOtherAnimals),
        new XElement("Country", animal.Country),
        new XElement("WhatAnimal", animal.GetClassificationAnimal()));
    program.Add(element);
}


Animal dog = new Animal("Russia", false, "Dog", eClassificationAnimal.Carnivores);
Lion lion = new Lion("Africa");
Cow cow = new Cow("Germany");
Pig pig = new Pig("Russia");

XElement program = new XElement("Animals");
Serialize(dog, program);
Serialize(lion, program);
Serialize(cow, program);
Serialize(pig, program);
XDocument newDoc = new XDocument(program);
newDoc.Save("C:\\Users\\Ruslan\\source\\repos\\Lab 08\\animals.xml");
*/
using (FileStream? fs = new FileStream("C:\\Users\\Ruslan\\source\\repos\\Lab 08\\animals.xml", FileMode.Create))
{
    XmlSerializer xmlElement = new XmlSerializer(typeof(Animal));
    xmlElement.Serialize(fs, new Animal("Russia", false, "Dog", eClassificationAnimal.Carnivores));


}

