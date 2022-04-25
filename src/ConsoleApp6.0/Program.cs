// See https://aka.ms/new-console-template for more information
using ConsoleApp2;

#region 逆变和协变
//协变
/**
IAnimalHouse<Dog> dogHouse = new AnimalHouse<Dog>();
IAnimalHouse<Animal> anaimalHouse = dogHouse;
**/

//逆变
/**
IAnimalHouse<Animal> animalHouse = new AnimalHouse<Animal>();
IAnimalHouse<Pig> pigHouse = animalHouse;
**/

/**
AnimalHouse<Dog> dogHouse = new AnimalHouse<Dog>();
TestIn(dogHouse);

AnimalHouse<Animal> animalHouse2 = new AnimalHouse<Animal>();
TestIn(animalHouse2);
**/
#endregion

#region 斐波那契数列
/**
foreach (var item in Fibonacci.GenerateFibonacci(10))
{
    Console.WriteLine(item);
}
foreach (var item in Fibonacci.Fibs())
{
    Console.WriteLine(item);
}

foreach (var item in Fibonacci.GenerateFibonacci2(10))
{
    Console.WriteLine(item);
}**/
#endregion

#region foreach && ref
int[] arr = { 1, 2, 3, 4, 5, 6 };
Console.WriteLine(String.Join(',', arr));
foreach (ref int i in arr.AsSpan())
{
    i++;
}
Console.WriteLine(String.Join(',', arr));
#endregion.

Console.WriteLine();

static void TestIn(IAnimalHouse<Dog> dog)
{
    Dog d = new Dog();
    dog.AddAnimal(d);
}