using System;
using Task1;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string path1 = @"C:\graph\GraphReadingOrgraph.txt";
            string path2 = @"C:\graph\GraphReadingNotOrgraph.txt";
            string path3 = @"C:\graph\GraphWriting.txt";
            string path4 = @"C:\graph\GraphReadingOrgraph2.txt";
            string path5 = @"C:\graph\GraphReadingNotOrgraph2.txt";

            //Graph g = new Graph(false); //пустой граф

            Graph p = new Graph(path1); //из файлика

            p.PrintArrNode();
            try
            {

                Console.WriteLine("Введите вершину: ");
                int input = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("*****************");
                p.printHalfDegreeForTask2(input);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
