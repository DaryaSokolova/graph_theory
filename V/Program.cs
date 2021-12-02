using System;

namespace Task
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string path7 = @"C:\graph\V.txt";
            string path1 = @"C:\graph\V_1.txt";


            //Graph g = new Graph(path7);

            //g.AddFlowInEdge(0, 1, 0, 1);
            //g.AddFlowInEdge(0, 2, 0, 4);
            //g.AddFlowInEdge(0, 3, 0, 5);
            //g.AddFlowInEdge(0, 5, 0, 2);
            //g.AddFlowInEdge(1, 3, 0, 10);
            //g.AddFlowInEdge(2, 4, 0, 2);
            //g.AddFlowInEdge(3, 4, 0, 8);
            //g.AddFlowInEdge(4, 5, 0, 9);

            Graph g = new Graph(path1);
            g.PrintArrNode();

            g.AddFlowInEdge(0, 1, 0, 12);

            g.AddFlowInEdge(0, 2, 0, 18);

            g.AddFlowInEdge(0, 5, 0, 14);

            g.AddFlowInEdge(1, 3, 0, 10);

            g.AddFlowInEdge(1, 5, 0, 8);

            g.AddFlowInEdge(2, 4, 0, 9);

            g.AddFlowInEdge(2, 6, 0, 12);

            g.AddFlowInEdge(3, 7, 0, 11);

            g.AddFlowInEdge(4, 7, 0, 17);

            g.AddFlowInEdge(5, 3, 0, 22);

            g.AddFlowInEdge(5, 6, 0, 12);

            g.AddFlowInEdge(6, 4, 0, 12);

            g.AddFlowInEdge(6, 7, 0, 10);

            Console.WriteLine(g.Ford_fulkerson(0, 7));
        }
    }
}
