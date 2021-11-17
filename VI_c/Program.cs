using System;

namespace Task
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string path8 = @"C:\graph\IV_3.txt";
            string path1 = @"C:\graph\IV_4.txt";

            //Graph g = new Graph(false); //пустой граф

            Graph p = new Graph(path1); //из файлика

            p.AddWeightInEdge(0, 1, 1);
            p.AddWeightInEdge(0, 2, -6);
            p.AddWeightInEdge(1, 2, 4);
            p.AddWeightInEdge(1, 3, 1);
            p.AddWeightInEdge(3, 2, -1);
            p.AddWeightInEdge(3, 4, 5);
            p.AddWeightInEdge(2, 4, -2);

            p.PrintArrNode();

            p.PrintEdgeWeights();

            p.IV_B();
        }
    }
}
