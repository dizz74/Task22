using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Task22
{
    class Program
    {

        static int[] array;

        static void Main(string[] args)
        {

            Console.WriteLine("Введите размер массива:");
            int n = Convert.ToInt32(Console.ReadLine());

            if (n > 0 && n <= 532650000)
            {  // MAX OK  536 870 897 x64 2Gb
               // MAX OK  532 650 000 x86 ?
                Task taskFill = new Task(() => FillArray(n));
                Func<Task, object> calc = new Func<Task, object>(CalcMaxAndSumm);
                Task<object> taskSumm = taskFill.ContinueWith(calc);
                taskFill.Start();

                (long Summ, int Max) res = ((long Summ, int Max))taskSumm.Result;
                Console.WriteLine("Сумма:{0} Max:{1}", res.Summ,res.Max);

            }

            Console.WriteLine("MainExit");
            Console.ReadKey();
        }

        static void FillArray(int n)
        {
            Console.WriteLine($"Заполняем {n} значений");
            array = new int[n];
            Random random = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(5000);
            }
            Console.WriteLine("Массив заполнен");

        }


        static object CalcMaxAndSumm(Task unused)
        {
            Console.WriteLine("Считаем сумму и макс");
            long summ = 0;
            int max = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > max) max = array[i];
                summ += array[i];
            }
             
            return (Summ: summ, Max: max);
        }
    }
}
