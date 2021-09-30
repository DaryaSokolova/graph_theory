using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public class NodeClass
    {
        public int ValueNode
        {
            get;
        }

        private List<int> AdjacencyList = new List<int>();

        public bool isLabel;

        public NodeClass(int valueNode)
        {
            ValueNode = valueNode;
        }

        public List<int> CheckList()
        {
            return AdjacencyList;
        }

        public void AddNodeInList(NodeClass n)
        {

            if (FindEdge(n) == false)
            {
                AdjacencyList.Add(n.ValueNode);
            }
            else
            {
                throw new Exception("Такой узел уже есть в списке смежности.");
            }

        }

        public bool FindEdge(NodeClass nodeInList)
        {
            for (int i = 0; i < AdjacencyList.Count; i++)
            {
                if (AdjacencyList[i] == nodeInList.ValueNode)
                {
                    return true;
                }
            }

            return false;
        }

        public void DeleteEdge(int value)
        {
            for (int i = 0; i < AdjacencyList.Count; i++)
            {
                if (AdjacencyList[i] == value)
                {
                    AdjacencyList.RemoveAt(i);
                }
            }
        }

        public void PrintList()
        {
            Console.Write("[" + ValueNode + "]" + ":");
            for (int i = 0; i < AdjacencyList.Count; i++)
            {
                Console.Write(AdjacencyList[i] + " ");
            }
            Console.WriteLine();
        }

        public string WriteList()
        {
            string tempStr = ValueNode + "|";
            for (int i = 0; i < AdjacencyList.Count; i++)
            {
                tempStr += AdjacencyList[i] + " ";
            }

            return tempStr;
        }
    }
}
