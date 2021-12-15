using System;

namespace Task
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
            Console.WriteLine("p: ");
            p.PrintArrNode();
            Graph p1 = new Graph(path2);
            p1.AddNodeInGraph(666);
            p1.AddEdgeInGraph(45, 6);
            Console.WriteLine("p1: ");
            p1.PrintArrNode();

            Console.WriteLine(p.CheckG1InG2(p1));
        }
    }
}
