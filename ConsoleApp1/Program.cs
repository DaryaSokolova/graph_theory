using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string path = @"C:\graph\GraphWriting.txt";

            Graph g = new Graph();
            Graph p = new Graph(path);
            Graph q = new Graph(p);

            p.PrintArrNode();
            q.PrintArrNode();
        }
    }
}
