namespace Lab08_Library;

[MyComment("����������� ���")]
public class Lion : Animal
{
    public Lion(string? country)
    {
        Country = country;
        HideFromOtherAnimals = false;
        Name = "Lion";
        WhatAnimal = eClassificationAnimal.Carnivores;
    }
}