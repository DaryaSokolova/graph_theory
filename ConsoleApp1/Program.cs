using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string path = @"C:\graph\GraphWriting.txt";

            try
            {

                Graph g = new Graph();
                Graph p = new Graph(path);
                Graph q = new Graph(p);

                q.AddNodeInGraph(7474);
                q.AddEdgeInGraph(40, 7474, 666);

                q.PrintEdgeWeights();
                q.PrintArrNode();
            }
            catch (Exception ex)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine(ex.Message);
            }

        }
    }
}
