using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
1. Построить три класса (базовый и 2 потомка), описывающих двух работников: с почасовой оплатой (один из потомков) и фиксированной оплатой (второй потомок).
а) Описать в базовом классе абстрактный метод для расчета среднемесячной заработной платы.
Для «повременщиков» формула для расчета такова: «среднемесячная заработная плата = 20.8 * 8 * почасовая ставка».
Для работников с фиксированной оплатой: «среднемесячная заработная плата = фиксированная месячная оплата».
б) Создать на базе абстрактного класса массив сотрудников и заполнить его.
в) *Реализовать интерфейсы для возможности сортировки массива, используя Array.Sort().
г) *Создать класс, содержащий массив сотрудников, и реализовать возможность вывода данных с использованием foreach. 
*/

namespace SharpLesson2
{
    class Program
    {
        private static int EMP_ARRAY_SIZE = 20; //размер массива с сотрудниками

        private static int EMP_FIX_UP_SALARY = 100_000; //верхняя граница фиксированных зарплат
        private static int EMP_FIX_LOW_SALARY = 50_000; //нижняя граница фиксированных зарплат

        private static int EMP_HOUR_UP_SALARY = 800; //верхняя граница почасовых зарплат
        private static int EMP_HOUR_LOW_SALARY = 250; //нижняя граница почасовых зарплат

        private static List<BaseEmployee> empList;

        static void Main(string[] args)
        {
            //б) Создать на базе абстрактного класса массив сотрудников и заполнить его.
            InitArray();
            Console.WriteLine("Начальный массив:");
            PrintArray();

            //в) *Реализовать интерфейсы для возможности сортировки массива, используя Array.Sort().
            empList.Sort();
            Console.WriteLine("\nОтсортированный массив:");
            PrintArray();

            //г) *Создать класс, содержащий массив сотрудников, и реализовать возможность вывода данных с использованием foreach.
            Console.WriteLine("\nКласс с массивом сотрудников:");
            EmployeeList employeeList = new EmployeeList();
            //заполняем массив
            foreach (var item in empList)
            {
                employeeList.Add(item);
            }

            Console.WriteLine("Сотрудник\tЗарплата");
            int index = 1;
            foreach (var item in employeeList)
            {
                Console.WriteLine($"{index}\t\t{(item as BaseEmployee).CountSalary()}");
                index++;
            }

            Console.ReadKey();
        }

        static void InitArray()
        {
            empList = new List<BaseEmployee>();
            Random rnd = new Random();

            for (int i = 0; i < EMP_ARRAY_SIZE / 2; i++)
            {
                empList.Add(new FixEmployee(rnd.Next(EMP_FIX_LOW_SALARY, EMP_FIX_UP_SALARY + 1)));
            }
            for (int i = 0; i < EMP_ARRAY_SIZE / 2; i++)
            {
                empList.Add(new HourEmployee(rnd.Next(EMP_HOUR_LOW_SALARY, EMP_HOUR_UP_SALARY + 1)));
            }
        }

        static void PrintArray()
        {
            Console.WriteLine("Сотрудник\tЗарплата");
            for (int i = 0; i < empList.Count; i++)
            {
                Console.WriteLine($"{i + 1}\t\t{empList[i].CountSalary()}");
            }
        }
    }
}
