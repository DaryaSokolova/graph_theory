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
            string path6 = @"C:\graph\ExII.txt";
            string path7 = @"C:\graph\ExIIorgr.txt";

            //Graph g = new Graph(false); //пустой граф

            Graph p = new Graph(path6); //из файлика
            p.DeleteNodeFromGraph(20); //при наличии изолированных
                                       //вершин ничего не выведет

            p.PrintArrNode();

            p.TaskBFSII(3);
        }
    }
}
