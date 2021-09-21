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
        public Graph()
        {

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
            else
            {
                throw new Exception("Такой узел уже есть!");
            }
        }

        public void AddEdgeInGraph(int valNode1, int valNode2)
        {

            if (boolFindNode(valNode1))
            {
                if (boolFindNode(valNode2))
                {
                    FindNode(valNode1).AddNodeInList(FindNode(valNode2));
                }
                else
                {
                    NodeClass n = new NodeClass(valNode2);
                    FindNode(valNode1).AddNodeInList(n);
                    ArrNode.Add(n);
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

        public void DeleteEdgeFromGraph(int val1, int val2)
        {
            if (boolFindNode(val1) && (boolFindNode(val2)))
            {
                if (FindNode(val1).FindEdge(FindNode(val2)))
                {
                    FindNode(val1).DeleteEdge(val2);
                }
                else
                {
                    throw new Exception("Дуга не существовала..");
                }
            }
        }

        public void PrintArrNode()
        {
            Console.WriteLine();
            for (int i = 0; i < ArrNode.Count; i++)
            {
                ArrNode[i].PrintList();
            }
        }

        public void WriteArrNode(string path)
        {

            using (StreamWriter sw = new StreamWriter(path, false)) { }

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
    }
}
