using System;

namespace Task1
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

            Console.WriteLine("Для вашего удобства по умолчанию считывается из файла");
            Console.WriteLine("для смены способа создания измените на соответсвующий объект графа");
            try
            {
                Console.WriteLine("Текущий граф:");
                p.PrintArrNode();

                while (true)
                {

                    Console.WriteLine("1 - добавить узел");
                    Console.WriteLine("2 - добавить ребро");
                    Console.WriteLine("3 - удалить узел");
                    Console.WriteLine("4 - удалить ребро");
                    Console.WriteLine("5 - добавить ребру вес");
                    Console.WriteLine("6 - вывести в файл");
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

                            Console.WriteLine("1 - Без веса, вес будет равен 0");
                            Console.WriteLine("2 - ребро с весом");
                            Console.WriteLine("Иное = выход");
                            int selection2 = Convert.ToInt32(Console.ReadLine());
                            switch (selection2)
                            {
                                case 1:
                                    Console.Write("int узела1 = ");
                                    int val1 = Convert.ToInt32(Console.ReadLine());

                                    Console.Write("int узела2 = ");
                                    int val2 = Convert.ToInt32(Console.ReadLine());

                                    p.AddEdgeInGraph(val1, val2);
                                    break;

                                case 2:
                                    Console.Write("int узела1 = ");
                                    int valWithWeight1 = Convert.ToInt32(Console.ReadLine());

                                    Console.Write("int узела2 = ");
                                    int valWithWeight2 = Convert.ToInt32(Console.ReadLine());

                                    Console.Write("int вес = ");
                                    int valWeight2 = Convert.ToInt32(Console.ReadLine());
                                    p.AddEdgeInGraph(valWithWeight1, valWithWeight2, valWeight2);

                                    Console.WriteLine("Текущие веса:");
                                    p.PrintEdgeWeights();
                                    break;

                                default: break;
                            }
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

                            Console.Write("int узела1 = ");
                            int valInsert1 = Convert.ToInt32(Console.ReadLine());

                            Console.Write("int узела2 = ");
                            int valInsert2 = Convert.ToInt32(Console.ReadLine());

                            Console.Write("int вес = ");
                            int valWeight = Convert.ToInt32(Console.ReadLine());

                            p.AddWeightInEdge(valInsert1, valInsert2, valWeight);
                            p.PrintEdgeWeights();

                            break;

                        case 6:

                            p.WriteArrNode(path3);

                            break;

                        default:
                            Console.WriteLine("Вы нажали неизвестную команду");
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
