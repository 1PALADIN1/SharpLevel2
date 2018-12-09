using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
3. *Дан фрагмент программы:
а) Свернуть обращение к OrderBy с использованием лямбда-выражения $.
б) *Развернуть обращение к OrderBy с использованием делегата Predicate<T>.
*/
namespace SharpLesson4
{
    partial class Program
    {
        public static void Task3()
        {
            CodeVersion0();
            CodeVersion1();
        }

        /// <summary>
        /// Исходная версия кода из методички
        /// </summary>
        private static void CodeVersion0()
        {
            Console.WriteLine("Исходная версия:");
            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                { "four",4 },
                { "two",2 },
                { "one",1 },
                { "three",3 },
            };
            var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });

            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// С помощью лямбда выражения
        /// </summary>
        private static void CodeVersion1()
        {
            Console.WriteLine("С помощью лямбда выражения:");
            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                { "four",4 },
                { "two",2 },
                { "one",1 },
                { "three",3 },
            };
            //var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });
            var d = dict.OrderBy(pair => pair.Value);

            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
        }

        private static Func<KeyValuePair<string, int>> orderByFun;

        /// <summary>
        /// С помощью делегата Predicate<T>
        /// </summary>
        //private static void CodeVersion2()
        //{
        //    Console.WriteLine("С помощью предиката:");
        //    Dictionary<string, int> dict = new Dictionary<string, int>()
        //    {
        //        { "four",4 },
        //        { "two",2 },
        //        { "one",1 },
        //        { "three",3 },
        //    };
        //    //var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });
        //    orderByFun = (c) => c = ;
        //    var d = dict.OrderBy(delegate(KeyValuePair<string, int> pair) { return pair.Value; });

        //    foreach (var pair in d)
        //    {
        //        Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
        //    }
        //}
    }
}
