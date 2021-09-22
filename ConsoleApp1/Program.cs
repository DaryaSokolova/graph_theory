using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string path1 = @"C:\graph\GraphWriting.txt";
            string path2 = @"C:\graph\test4.txt";

            Graph g = new Graph(); //пустой граф
            Graph p = new Graph(path2); //из файлика
            Graph q = new Graph(p); //скопировать существующий граф


            Console.WriteLine("Для вашего удобства по умолчанию считывается из файла");
            Console.WriteLine("для смены способа создания измените на соответсвующий объект графа");
            try
            {
                Console.WriteLine("Текущий граф:");
                p.PrintArrNode();

                Console.WriteLine("1 - добавить узел");
                Console.WriteLine("2 - добавить ребро");
                Console.WriteLine("3 - удалить узел");
                Console.WriteLine("4 - удалить ребро");
                Console.WriteLine("5 - вывести в файл");
                int selection = Convert.ToInt32(Console.ReadLine());
                switch (selection)
                {
                    case 1:

                        Console.WriteLine("При добавлении существующего узла " +
                            "его дубликат не добавится.");

                        Console.Write("int узел = ");
                        int val = Convert.ToInt32(Console.ReadLine());
                        p.AddNodeInGraph(val);
                        p.PrintArrNode();

                        break;
                    case 2:

                        Console.WriteLine("При добавлении дуги к несуществующему узлу " +
                            "будет создан новый узел.");

                        Console.Write("int узела1 = ");
                        int val1 = Convert.ToInt32(Console.ReadLine());

                        Console.Write("int узела2 = ");
                        int val2 = Convert.ToInt32(Console.ReadLine());

                        p.AddEdgeInGraph(val1, val2);
                        p.PrintArrNode();

                        break;

                    case 3:

                        Console.Write("int узела = ");
                        int valВDel = Convert.ToInt32(Console.ReadLine());

                        p.DeleteNodeFromGraph(valВDel);
                        p.PrintArrNode();
                        
                        break;


                    case 4:

                        Console.WriteLine("При удалении дуги к несуществующему узлу " +
                            "- ничего не изменится");

                        Console.Write("int узела1 = ");
                        int valВDel1 = Convert.ToInt32(Console.ReadLine());

                        Console.Write("int узела2 = ");
                        int valВDel2 = Convert.ToInt32(Console.ReadLine());

                        p.DeleteEdgeFromGraph(valВDel1, valВDel2);
                        p.PrintArrNode();

                        break;

                    case 5:

                        p.WriteArrNode(path1);

                        break;

                    default:
                        Console.WriteLine("Вы нажали неизвестную команду");
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
