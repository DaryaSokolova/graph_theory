using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Graph
    {
        private List<NodeClass> ArrNode = new List<NodeClass>();

        private List<EdgeClass> ArrForWeightEdge = new List<EdgeClass>();
        public bool IsOrgraph
        {
            get;

            private set;
        }
        public Graph(bool isOrgraph)
        {
            IsOrgraph = isOrgraph;
        }

        public Graph(string path)
        {
            ReadArrNode(path);
        }

        public Graph(Graph g)
        {
            for (int i = 0; i < g.ArrNode.Count; i++)
            {
                AddNodeInGraph(g.ArrNode[i].ValueNode);
                List<int> tempList = new List<int>();

                for (int j = 0; j < g.ArrNode[i].CheckList().Count; j++)
                {
                    tempList.Add(g.ArrNode[i].CheckList()[j]);
                }

                for (int j = 0; j < tempList.Count; j++)
                {
                    AddEdgeInGraph(FindNode(g.ArrNode[i].ValueNode).ValueNode, tempList[j]);
                }
            }
        }

        private NodeClass FindNode(int value)
        {
            for (int i = 0; i < ArrNode.Count; i++)
            {
                if (ArrNode[i].ValueNode == value)
                {
                    return ArrNode[i];
                }
            }

            return null;
        }

        private bool boolFindNode(int value)
        {
            if (FindNode(value) != null) return true;

            return false;
        }

        public void AddNodeInGraph(int value)
        {
            if (boolFindNode(value) == false)
            {
                NodeClass n = new NodeClass(value);
                ArrNode.Add(n);
            }
        }

        public void AddEdgeInGraph(int valNode1, int valNode2)
        {

            if (boolFindNode(valNode1))
            {
                if (boolFindNode(valNode2))
                {
                    if (FindNode(valNode1).FindEdge(FindNode(valNode2)) == false)
                    {
                        FindNode(valNode1).AddNodeInList(FindNode(valNode2));
                        EdgeClass edge = new EdgeClass(FindNode(valNode1), FindNode(valNode2), 0); //WEIGHT ПО ДЕФОЛТУ
                        ArrForWeightEdge.Add(edge);

                        if (IsOrgraph == false && valNode1 != valNode2)
                        {
                            FindNode(valNode2).AddNodeInList(FindNode(valNode1));
                        }
                    }
                }
                else
                {
                    NodeClass n = new NodeClass(valNode2);
                    FindNode(valNode1).AddNodeInList(n);
                    ArrNode.Add(n);

                    if (IsOrgraph == false)
                    {
                        FindNode(valNode2).AddNodeInList(FindNode(valNode1));
                    }

                    EdgeClass edge = new EdgeClass(FindNode(valNode1), FindNode(valNode2), 0); //WEIGHT ПО ДЕФОЛТУ
                    ArrForWeightEdge.Add(edge);
                }
            }
            else
            {
                throw new Exception("Первого узла вообще не существует.");
            }

        }

        //ПЕРЕГРУЗКА ДЛЯ МЕТОДА С УКАЗАНИЕМ ВЕСА
        public void AddEdgeInGraph(int valNode1, int valNode2, int valWeight)
        {

            if (boolFindNode(valNode1))
            {
                if (boolFindNode(valNode2))
                {
                    FindNode(valNode1).AddNodeInList(FindNode(valNode2));
                    EdgeClass edge = new EdgeClass(FindNode(valNode1), FindNode(valNode2), valWeight); //WEIGHT
                    ArrForWeightEdge.Add(edge);

                    if (IsOrgraph == false)
                    {
                        FindNode(valNode2).AddNodeInList(FindNode(valNode1));
                    }
                }
                else
                {
                    NodeClass n = new NodeClass(valNode2);
                    FindNode(valNode1).AddNodeInList(n);
                    ArrNode.Add(n);

                    if (IsOrgraph == false)
                    {
                        FindNode(valNode2).AddNodeInList(FindNode(valNode1));
                    }

                    EdgeClass edge = new EdgeClass(FindNode(valNode1), FindNode(valNode2), valWeight); //WEIGHT
                    ArrForWeightEdge.Add(edge);
                }
            }
            else
            {
                throw new Exception("Первого узла вообще не существует.");
            }

        }

        private bool FindEdgeInGraph(NodeClass n1, NodeClass n2)
        {
            if (n1.FindEdge(n2)) return true;

            return false;
        }

        private int FindIndexOfNode(NodeClass n)
        {
            for (int i = 0; i < ArrNode.Count; i++)
            {
                if (ArrNode[i] == n) return i;
            }

            return -1;
        }

        public void DeleteNodeFromGraph(int value)
        {
            if (boolFindNode(value))
            {
                for (int i = 0; i < ArrNode.Count; i++)
                {
                    if (ArrNode[i].FindEdge((FindNode(value))))
                    {
                        ArrNode[i].DeleteEdge(value);
                    }
                }

                ArrNode.RemoveAt(FindIndexOfNode(FindNode(value)));
            }
            else
            {
                throw new Exception("А что, собственно, удалять? Узла нет.");
            }
        }

        public int FindIndexEdgeForWeight(NodeClass valFrom, NodeClass valTo)
        {
            for (int i = 0; i < ArrForWeightEdge.Count; i++)
            {
                if (ArrForWeightEdge[i].ValFrom == valFrom && ArrForWeightEdge[i].ValTo == valTo)
                {
                    return i;
                }
            }

            return -1;
        }

        public void DeleteEdgeFromGraph(int val1, int val2)
        {
            if (boolFindNode(val1) && (boolFindNode(val2)))
            {
                if (FindNode(val1).FindEdge(FindNode(val2)))
                {
                    FindNode(val1).DeleteEdge(val2);

                    if (IsOrgraph == false)
                    {
                        FindNode(val2).DeleteEdge(val1);
                    }

                    ArrForWeightEdge.RemoveAt(FindIndexEdgeForWeight(FindNode(val1), FindNode(val2)));

                }
                else
                {
                    throw new Exception("Дуга не существовала..");
                }
            }
        }

        public void PrintArrNode()
        {
            if (IsOrgraph == true)
            {
                Console.WriteLine("ORGRAPH");
            }
            else Console.WriteLine("NOT ORGRAPH");
            Console.WriteLine();
            for (int i = 0; i < ArrNode.Count; i++)
            {
                ArrNode[i].PrintList();
            }
        }

        public void WriteArrNode(string path)
        {

            using (StreamWriter sw = new StreamWriter(path, false)) { }

            if (IsOrgraph == false)
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine("false");
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine("true");
                }
            }

            for (int i = 0; i < ArrNode.Count; i++)
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(ArrNode[i].WriteList());
                }
            }
        }

        private int ConvertNode(string tempNode)
        {
            int temp = Int32.Parse(tempNode);
            AddNodeInGraph(temp);
            return temp;
        }

        private void Convert(string str)
        {
            if (str == "true" || str == "false")
            {
                if (str == "true")
                    IsOrgraph = true;
                else IsOrgraph = false;
            }
            else
            {
                string tempNode = str.Substring(0, str.IndexOf('|'));
                int node = ConvertNode(tempNode);
                string tempStr = str.Substring(str.IndexOf('|') + 1);

                string[] nodesInList = tempStr.Split(' ');

                for (int i = 0; i < nodesInList.Length; i++)
                {
                    if (nodesInList[i] != "")
                    {
                        int temp = Int32.Parse(nodesInList[i]);
                        AddEdgeInGraph(node, temp);

                    }
                }
            }
        }

        private void ReadArrNode(string path)
        {

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Convert(line);
                }
            }
        }

        public void PrintEdgeWeights()
        {
            for (int i = 0; i < ArrForWeightEdge.Count; i++)
            {
                Console.WriteLine("From: " + ArrForWeightEdge[i].ValFrom.ValueNode + " "
                                 + "To: " + ArrForWeightEdge[i].ValTo.ValueNode + " "
                                 + "Weight: " + ArrForWeightEdge[i].Weight);
            }

            Console.WriteLine();
        }

        public void AddWeightInEdge(int valFrom, int valTo, int value)
        {
            if (FindIndexEdgeForWeight(FindNode(valFrom), FindNode(valTo)) != -1)
            {
                ArrForWeightEdge[FindIndexEdgeForWeight(FindNode(valFrom), FindNode(valTo))].Weight = value;
            }
            else
            {
                throw new Exception("Дуги не существует");
            }
        }
    }
}
