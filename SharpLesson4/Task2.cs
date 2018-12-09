using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
2. Дана коллекция List<T>, требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции:
а) для целых чисел;
б) *для обобщенной коллекции;
в) *используя Linq.
*/
namespace SharpLesson4
{
    partial class Program
    {
        public class Car
        {
            string brand;
            string model;

            public string Brand
            {
                get => brand;
            }

            public string Model
            {
                get => model;
            }

            public Car(string brand, string model)
            {
                this.brand = brand;
                this.model = model;
            }

            public override string ToString()
            {
                return $"{brand} {model}";
            }
        }

        static void Task2()
        {
            //а) для целых чисел;
            Console.WriteLine("Для целых чисел:");
            List<int> intList = new List<int>();
            Dictionary<int, int> intDictionary = new Dictionary<int, int>();
            Random rnd = new Random();
            int intListSize = 50;

            //заполняем массив
            for (int i = 0; i < intListSize; i++)
            {
                intList.Add(rnd.Next(0, 21));
                if (i == intListSize - 1) Console.Write(intList[i]);
                else
                    Console.Write(intList[i] + ", ");
            }
            Console.WriteLine();

            //считаем количество символов
            foreach (var item in intList)
            {
                if (intDictionary.ContainsKey(item)) intDictionary[item]++;
                else
                    intDictionary.Add(item, 1);
            }

            Console.WriteLine("Число\t\tКоличество");
            foreach (var item in intDictionary)
            {
                Console.WriteLine($"{item.Key}\t\t{item.Value}");
            }


            //б) *для обобщенной коллекции;


            //в) *используя Linq.
            Console.WriteLine("Используя Linq:");
            List<Car> carList = new List<Car>()
            {
                new Car("Tesla", "Model X"),
                new Car("Audi", "Q7"),
                new Car("BMW", "X7"),
                new Car("Audi", "Q7"),
                new Car("Tesla", "Model X"),
                new Car("Toyota", "Land Cruiser"),
                new Car("Toyota", "Camry"),
                new Car("BMW", "X7"),
                new Car("Audi", "A7"),
                new Car("Mercedes", "E-class"),
                new Car("Audi", "Q7")
            };
            var selectCar = carList.GroupBy(c => $"{c.Brand} {c.Model}");
            foreach (var item in selectCar)
            {
                Console.WriteLine($"{item.Key} : {item.Count()}");
            }
        }
    }
}
