namespace Lab08_Library;

[MyComment("����������� ������")]
public class Cow : Animal
{
    public Cow(string? country)
    {
        Country = country;
        HideFromOtherAnimals = true;
        Name = "Cow";
        WhatAnimal = eClassificationAnimal.Herbivores;
    }
}