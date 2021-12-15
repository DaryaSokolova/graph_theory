using System;

namespace Task
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string path7 = @"C:\graph\IV_2.txt";
            string path8 = @"C:\graph\IV_3.txt";

            //Graph g = new Graph(false); //пустой граф

            Graph p = new Graph(path7); //из файлика

            p.AddWeightInEdge(0, 2, 4);
            p.AddWeightInEdge(0, 1, 5);
            p.AddWeightInEdge(1, 2, 4);
            p.AddWeightInEdge(2, 3, 2);
            p.AddWeightInEdge(3, 1, 17);
            p.AddWeightInEdge(2, 4, -4);
            p.AddWeightInEdge(4, 5, 3);
            p.AddWeightInEdge(5, 2, -2);

            //p.AddWeightInEdge(0, 2, 10);
            //p.AddWeightInEdge(0, 3, 3);
            //p.AddWeightInEdge(1, 0, 8);
            //p.AddWeightInEdge(3, 1, -4);
            //p.AddWeightInEdge(3, 2, 6);
            //p.AddWeightInEdge(2, 3, -2);
            //p.AddWeightInEdge(4, 2, 9);

            p.PrintArrNode();

            p.PrintEdgeWeights();

            p.IV_C(0, 1, 2);
        }
    }
}
