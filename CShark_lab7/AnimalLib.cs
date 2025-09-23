namespace LibAnimal
{
    public class AnimalAttribute : Attribute
    {
        public AnimalAttribute(string str) => Comment = str;
        public string Comment { get; set; }

    }
    public enum eClassificationAnimal
    {
        Herbivores,
        Carnivores,
        Omnivores
    }
    public enum eFavouriteFood
    {
        Meat,
        Plants,
        Everything
    }

    [AnimalAttribute("This is an Animal")]
    public abstract class Animal
    {
        public string Country { get; set; }
        public string Name { get; set; }
        public string HideFromOtherAnimals { get; set; }
        public string WhatAnimal { get; set; }
        public Animal(string country, string name, string hidefromother, string whatanimal) 
        {
            (Country, Name, HideFromOtherAnimals, WhatAnimal) = (country, name, hidefromother, whatanimal);
        }
        public void Deconstruct(out string country, out string name, out string hidefromother, out string whatanimal)
        { 
            (country, name, hidefromother, whatanimal) = (Country, Name, HideFromOtherAnimals, WhatAnimal); 
        }
        public abstract eClassificationAnimal GetClassificationAnimal();
        public abstract eFavouriteFood GetFavouriteFood();
        public abstract void SayHello();
    }

    [AnimalAttribute("This is a Cow")]
    public class Cow : Animal
    {
        public Cow(string country, string name, string hidefromother, string whatanimal) : base(country, name, hidefromother, whatanimal) { }
        public override eClassificationAnimal GetClassificationAnimal() => eClassificationAnimal.Herbivores;
        public override eFavouriteFood GetFavouriteFood() => eFavouriteFood.Plants;
        public override void SayHello() => Console.WriteLine("Salam ot the Cow");
    }

    [AnimalAttribute("This is a Lion")]
    public class Lion : Animal
    {
        public Lion(string country, string name, string hidefromother, string whatanimal) : base(country, name, hidefromother, whatanimal) { }
        public override eClassificationAnimal GetClassificationAnimal() => eClassificationAnimal.Carnivores;
        public override eFavouriteFood GetFavouriteFood() => eFavouriteFood.Meat;
        public override void SayHello() => Console.WriteLine("Salam ot the Lion");
    }

    [AnimalAttribute("This is a Pig")]
    public class Pig : Animal
    {
        public Pig(string country, string name, string hidefromother, string whatanimal) : base(country, name, hidefromother, whatanimal) { }
        public override eClassificationAnimal GetClassificationAnimal() => eClassificationAnimal.Omnivores;
        public override eFavouriteFood GetFavouriteFood() => eFavouriteFood.Everything;
        public override void SayHello() => Console.WriteLine("Salam ot the Pig");
    }
}
