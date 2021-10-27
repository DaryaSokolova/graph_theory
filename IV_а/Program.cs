using System;

namespace Task
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string path2 = @"C:\graph\GraphReadingNotOrgraph.txt";
            string path3 = @"C:\graph\GraphWriting.txt";
            string path5 = @"C:\graph\GraphReadingNotOrgraph2.txt";
            string path6 = @"C:\graph\ExII.txt";
            string path7 = @"C:\graph\ExCarcass.txt";

            //Graph g = new Graph(false); //пустой граф

            Graph p = new Graph(path7); //из файлика

            p.AddWeightInEdge(1, 3, 12);
            p.AddWeightInEdge(1, 2, 3);
            p.AddWeightInEdge(3, 6, 10);
            p.AddWeightInEdge(2, 4, 5);
            p.AddWeightInEdge(2, 3, 7);
            p.AddWeightInEdge(4, 7, 5);
            p.AddWeightInEdge(6, 7, 25);

            p.PrintArrNode();

            p.PrintEdgeWeights();

            p.IV_A(1, 3);
        }
    }
}
