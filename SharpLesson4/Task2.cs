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

            /// <summary>
            /// Инициализация объекта машины
            /// </summary>
            /// <param name="brand">Бренд</param>
            /// <param name="model">Модель</param>
            public Car(string brand, string model)
            {
                this.brand = brand;
                this.model = model;
            }

            /// <summary>
            /// Перегрузка метода ToString()
            /// </summary>
            /// <returns>Строка brand + model</returns>
            public override string ToString()
            {
                return $"{brand} {model}";
            }

            /// <summary>
            /// Проверка на равенство текущего объекта с другим объектом
            /// </summary>
            /// <param name="obj">Объект типа Car, с которым сравниваем</param>
            /// <returns>Истину в случае успешного сравнения, в противном случае - ложь</returns>
            public override bool Equals(object obj)
            {
                Car other = obj as Car;
                if (other.Brand == Brand && other.Model == Model)
                    return true;
                return false;
            }

            /// <summary>
            /// Получение результата работы хеш-функции для объекта
            /// </summary>
            /// <returns>Результат (целое число) работы хеш-функции</returns>
            public override int GetHashCode()
            {
                //return base.GetHashCode();
                return Brand.GetHashCode() + Model.GetHashCode();
            }
        }

        class MyList<T>
        {
            private List<T> list;

            public T this[int index]
            {
                get => list[index];
                set => list[index] = value;
            }

            public int Count
            {
                get => list.Count;
            }

            /// <summary>
            /// Инициализация списка
            /// </summary>
            public MyList()
            {
                list = new List<T>();
            }

            /// <summary>
            /// Добавление элемента
            /// </summary>
            /// <param name="element">Добавляемый элемент</param>
            public void Add(T element)
            {
                list.Add(element);
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
            Console.WriteLine("\nДля обобщённой коллекции:");
            MyList<Car> list = new MyList<Car>();
            Dictionary<Car, int> carDictionary = new Dictionary<Car, int>();

            list.Add(new Car("Lada", "Granta"));
            list.Add(new Car("Ford", "Focus"));
            list.Add(new Car("Lada", "Granta"));
            list.Add(new Car("Skoda", "Rapid"));
            list.Add(new Car("Chevrolet", "Cruze"));
            list.Add(new Car("Skoda", "Rapid"));
            list.Add(new Car("Lada", "Granta"));

            for (int i = 0; i < list.Count; i++)
            {
                if (carDictionary.ContainsKey(list[i])) carDictionary[list[i]]++;
                else
                    carDictionary.Add(list[i], 1);
            }

            foreach (var item in carDictionary)
            {
                Console.WriteLine($"{item.Key} {item.Value}");
            }

            //в) *используя Linq.
            Console.WriteLine("\nИспользуя Linq:");
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
