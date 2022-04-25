using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal interface IAnimalHouse<in T> where T : Animal, new()
    {
        void AddAnimal(T animal);

        bool RemoveAnimal(T animal);

    }

 
}
