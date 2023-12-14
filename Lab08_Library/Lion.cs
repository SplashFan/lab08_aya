namespace Lab08_Library;

[MyComment("Наследуемый Лев")]
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