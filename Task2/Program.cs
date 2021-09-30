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

            Graph p = new Graph(path2); //из файлика

            p.printHalfDegreeForTask2(6);
        }
    }
}
