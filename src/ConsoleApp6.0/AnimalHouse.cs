using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class AnimalHouse<T> : IAnimalHouse<T> where T : Animal, new()
    {
        private List<T> animals = new List<T>();

        public void AddAnimal(T a) { 
            animals.Add(a);
        }

        public bool RemoveAnimal(T a)
        {
            return animals.Remove(a);
        }
    }
}
