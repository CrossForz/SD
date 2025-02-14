using System;
using System.Collections.Generic;
public interface IAlive
{
    int Food { get; }
}
public interface IInventory
{
    int Number { get; }
}

public abstract class Animal : IAlive
{
    public string Name { get; set; }
    public int Food { get; protected set; }
    public bool IsHealthy { get; set; }

    public abstract string GetAnimalType();
}

public class Herbo : Animal
{
    public int KindnessLevel { get; set; }

    public override string GetAnimalType() => "Травоядное";

    public bool CanInteract()
    {
        return KindnessLevel > 5;
    }
}

public class Predator : Animal
{
    public override string GetAnimalType() => "Хищник";
}

public class Monkey : Herbo
{
    public Monkey() { }
    public Monkey(string name, int food, int kindnessLevel)
    {
        Name = name;
        Food = food;
        KindnessLevel = kindnessLevel;
        IsHealthy = true; 
    }
}

public class Rabbit : Herbo
{
    public Rabbit() { }
    public Rabbit(string name, int food, int kindnessLevel)
    {
        Name = name;
        Food = food;
        KindnessLevel = kindnessLevel;
        IsHealthy = true; 
    }
}

public class Tiger : Predator
{
    public Tiger() { } 
    public Tiger(string name, int food)
    {
        Name = name;
        Food = food;
        IsHealthy = true; 
    }
}

public class Wolf : Predator
{
    public Wolf() { }   
    public Wolf(string name, int food)
    {
        Name = name;
        Food = food;
        IsHealthy = true;
    }
}

public abstract class Thing : IInventory
{
    public string Name { get; set; }
    public int Number { get; protected set; }
}

public class Table : Thing
{
    public Table() { }
    public Table(string name, int number)
    {
        Name = name;
        Number = number;
    }
}

public class Computer : Thing
{
    public Computer() { }
    public Computer(string name, int number)
    {
        Name = name;
        Number = number;
    }
}

public class VetClinic
{
    public VetClinic() { }

    public bool AnimalCheck(Animal animal)
    {
        return animal.IsHealthy;
    }
}

public class Zoo
{
    private List<Animal> animals = new List<Animal>();

    public VetClinic vetClinic = new VetClinic();
    public void AddAnimal(Animal animal)
    {
        if (vetClinic.AnimalCheck(animal))
        {
            animals.Add(animal);
            Console.WriteLine($"Животное {animal.Name} добавлено в зоопарк.");
        }
        else
        {
            Console.WriteLine($"Животное {animal.Name} не прошло проверку здоровья.");
        }
    }

    public void ShowFoodReport()
    {
        Console.WriteLine($"Количество животных в зоопарке: {animals.Count}");

        int totalFood = 0;
        foreach (var animal in animals)
        {
            totalFood += animal.Food;
            Console.WriteLine($"{animal.GetAnimalType()} - {animal.Name}, Потребление еды: {animal.Food} кг");
        }

        Console.WriteLine($"Общее потребление еды всеми животными: {totalFood} кг");
    }

    public void ShowContactZooAnimals()
    {
        Console.WriteLine("Животные, которые могут быть помещены в контактный зоопарк:");
        foreach (var animal in animals)
        {
            if (animal is Herbo herbivore && herbivore.CanInteract())
            {
                Console.WriteLine($"{herbivore.Name} (Уровень доброты: {herbivore.KindnessLevel})");
            }
        }
    }
}

class Program
{
    static int GetIntegerFromUser()
    {
        while (true)
        {
            string input = Console.ReadLine();

            if (int.TryParse(input, out int number) && number > 0)
            {
                return number; 
            }
            else
            {
                Console.WriteLine("Ошибка: Введено некорректное значение. Пожалуйста, попробуйте снова. \n");
            }
        }
    }
    static void Main(string[] args)
    {
        Zoo zoo = new Zoo();
        zoo.AddAnimal(new Monkey("Мартышка", 2, 7));
        zoo.AddAnimal(new Tiger("Тигр", 5));
        while (true)
        {
            Console.WriteLine(" \nДобро пожаловать в зоопарк. Выберите пункт меню:");
            Console.WriteLine("1. Добавить животное в зоопарк.");
            Console.WriteLine("2. Вывести информацию о количестве еды, которое нужно всем животным.");
            Console.WriteLine("3. Предоставить информацию о животных, которые могут быть в контактном зоопарке.");
            Console.WriteLine("4. Выход");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите имя животного: ");
                    string name = Console.ReadLine();
                    Console.Write("Введите тип животного (обезьяна/кролик/тигр/волк): ");
                    string type = Console.ReadLine().ToLower();
                    Console.Write("Введите количество еды в кг/день: ");
                    int food = GetIntegerFromUser();
                    Console.Write("Здорово ли животное (да/нет): ");
                    string health = Console.ReadLine().ToLower();

                    Animal newAnimal;

                    if (type == "обезьяна")
                    {
                        Console.Write("Введите уровень доброты: ");
                        int kindnessLevel = GetIntegerFromUser();
                        newAnimal = new Monkey(name, food, kindnessLevel);
                        newAnimal.IsHealthy = health.ToLower() == "да";
                    }
                    else if (type == "кролик")
                    {
                        Console.Write("Введите уровень доброты (0-10): ");
                        int kindnessLevel = GetIntegerFromUser();
                        newAnimal = new Rabbit(name, food, kindnessLevel);
                        newAnimal.IsHealthy = health.ToLower() == "да";
                    }
                    else if (type == "тигр")
                    {
                        newAnimal = new Tiger(name, food);
                        newAnimal.IsHealthy = health.ToLower() == "да";
                    }
                    else if (type == "волк")
                    {
                        newAnimal = new Wolf(name, food);
                        newAnimal.IsHealthy = health.ToLower() == "да";
                    }
                    else
                    {
                        Console.WriteLine("Не опознанный вид. Добавить не удалось.");
                        continue;
                    }

                    zoo.AddAnimal(newAnimal);
                    
                    break;
                case "2":
                    zoo.ShowFoodReport();
                    break;
                case "3":
                    zoo.ShowContactZooAnimals();
                    break;
                case "4": 
                    return;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }
        }

    }
}
