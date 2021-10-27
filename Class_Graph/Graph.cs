﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
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

        private int FindHalfDegreeExodus(int value)
        {
            if (boolFindNode(value))
            {
                return FindNode(value).CheckList().Count;

            }
            else
            {
                throw new Exception("Такого узла нет в графе");
            }
        }

        public void PrintHalfDegreeForTask2(int value)
        {
            for (int i = 0; i < ArrNode.Count; i++)
            {
                if (FindHalfDegreeExodus(ArrNode[i].ValueNode) > FindHalfDegreeExodus(value))
                {
                    Console.WriteLine(ArrNode[i].ValueNode);
                }
            }
        }

        public void PrintNodeForTask3(int value)
        {
            if (boolFindNode(value))
            {

                for (int i = 0; i < ArrNode.Count; i++)
                {
                    if (FindEdgeInGraph(ArrNode[i], FindNode(value)) == false
                        && (FindEdgeInGraph(FindNode(value), ArrNode[i])) == false
                        && ArrNode[i].ValueNode != value)
                    {
                        Console.WriteLine(ArrNode[i].ValueNode);
                    }

                }
            }
            else Console.WriteLine("такой вершины нет");
        }

        public bool CheckG1InG2(Graph g2)
        {
            //nodes
            for (int i = 0; i < ArrNode.Count; i++)
            {
                if (g2.boolFindNode(ArrNode[i].ValueNode) == false)
                {
                    Console.WriteLine("Вершина " + ArrNode[i].ValueNode + " отсутствует.");
                    return false;
                }
            }

            //edges
            for (int i = 0; i < ArrNode.Count; i++)
            {
                List<int> temp = ArrNode[i].CheckList();
                for (int j = 0; j < temp.Count; j++)
                {
                    if (g2.FindEdgeInGraph(g2.ArrNode[i], g2.FindNode(temp[j])) == false)
                    {
                        Console.WriteLine("Ребро между " + g2.ArrNode[i].ValueNode + " и " + g2.FindNode(temp[j]).ValueNode + " отсутствует.");
                        return false;
                    }
                }
            }

            return true;
        }

        private List<int> arrUsed = new List<int>();

        //проход по компоненте связности
        private void DFS(int v)
        {
            for (int i = 0; i < arrUsed.Count; i++)
            {
                if (arrUsed[i] == v)
                {
                    Console.WriteLine(v);
                    arrUsed.Remove(v);

                    NodeClass n = FindNode(v);
                    for (int j = 0; j < n.AdjacencyList.Count; j++)
                    {
                        DFS(n.AdjacencyList[j]);
                    }
                }
            }
        }

        public void DepthFirstSearch()
        {
            for (int i = 0; i < ArrNode.Count; i++)
            {
                arrUsed.Add(ArrNode[i].ValueNode);
            }

            while (arrUsed.Count != 0)
            {
                DFS(arrUsed[0]);
            }
        }

        public bool isLine = false;
        private void DFSII(int v, int u)
        {
            for (int i = 0; i < arrUsed.Count; i++)
            {
                if (arrUsed[i] == v)
                {
                    Console.WriteLine(v);
                    arrUsed.Remove(v);

                    if (v == u)
                    {
                        isLine = true;
                    }

                    NodeClass n = FindNode(v);
                    for (int j = 0; j < n.AdjacencyList.Count; j++)
                    {
                        DFSII(n.AdjacencyList[j], u);
                    }
                }
            }
        }

        public void TaskDFSII(int v, int u, int k)
        {
            if (boolFindNode(v) == false || boolFindNode(u) == false)
            {
                Console.WriteLine("некорректный ввод вершин");
            }
            else
            {

                for (int j = 0; j < k; j++)
                {
                    Console.WriteLine("Введите вершину1: ");
                    int input1 = System.Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введите вершину2: ");
                    int input2 = System.Convert.ToInt32(Console.ReadLine());
                    DeleteEdgeFromGraph(input1, input2);
                }

                int index = 0;
                for (int i = 0; i < ArrNode.Count; i++)
                {
                    if (ArrNode[i].ValueNode == v)
                    {
                        index = i;
                    }
                    arrUsed.Add(ArrNode[i].ValueNode);
                }

                DFSII(arrUsed[index], u);
                if (isLine == true)
                {
                    Console.WriteLine("Путь существует");
                }
                else
                {
                    Console.WriteLine("Пути нет");
                }
            }
        }

        private Queue<int> qNodes = new Queue<int>();
        private int FindShortLine(int v1, int v2)
        {
            int[] count = new int[100];

            for (int i = 0; i < count.Length; i++)
            {
                count[i] = int.MaxValue;
            }

            count[v1] = 0;
            qNodes.Enqueue(v1);
            while (qNodes.Count != 0)
            {
                int u = qNodes.Dequeue();
                //Console.WriteLine(u);
                for (int i = 0; i < FindNode(u).AdjacencyList.Count; i++)
                {
                    if (count[FindNode(u).AdjacencyList[i]] == int.MaxValue)
                    {
                        count[FindNode(u).AdjacencyList[i]] = count[u] + 1;
                        qNodes.Enqueue(FindNode(u).AdjacencyList[i]);
                    }
                }
            }
            return count[v2];
        }

        public void TaskBFSII(int k)
        {
            for (int i = 0; i < ArrNode.Count; i++)
            {
                bool flag = true;
                for (int j = 0; j < ArrNode.Count; j++)
                {
                    int temp = 0;
                    if (ArrNode[i].ValueNode != ArrNode[j].ValueNode)
                    {
                        temp = FindShortLine(ArrNode[i].ValueNode, ArrNode[j].ValueNode);

                        if (temp > k)
                        {
                            flag = false;
                        }
                    }
                }

                if (flag == true)
                {
                    Console.WriteLine(ArrNode[i].ValueNode);
                }
            }
        }

        public List<EdgeClass> resList = new List<EdgeClass>();
        public void CarcassPrim()
        {
            List<EdgeClass> notUsedE = new List<EdgeClass>();

            for (int i = 0; i < ArrForWeightEdge.Count; i++)
            {
                notUsedE.Add(ArrForWeightEdge[i]);
            }

            List<int> usedV = new List<int>();
            List<int> notUsedV = new List<int>();
            for (int i = 0; i < ArrNode.Count; i++)
            {
                notUsedV.Add(ArrNode[i].ValueNode);
            }

            usedV.Add(ArrNode[0].ValueNode);
            notUsedV.Remove(ArrNode[0].ValueNode);

            while (notUsedV.Count != 0)
            {
                int minE = -1; //номер наименьшего ребра
                               //поиск наименьшего ребра
                for (int i = 0; i < notUsedE.Count; i++)
                {
                    if ((usedV.IndexOf(notUsedE[i].ValFrom.ValueNode) != -1)
                        && (notUsedV.IndexOf(notUsedE[i].ValTo.ValueNode) != -1)
                        || (usedV.IndexOf(notUsedE[i].ValTo.ValueNode) != -1)
                        && (notUsedV.IndexOf(notUsedE[i].ValFrom.ValueNode) != -1))
                    {
                        if (minE != -1)
                        {
                            if (notUsedE[i].Weight < notUsedE[minE].Weight)
                            {
                                minE = i;
                            }
                        }
                        else
                        {
                            minE = i;
                        }
                    }
                }

                if (usedV.IndexOf(notUsedE[minE].ValFrom.ValueNode) != -1)
                {
                    usedV.Add(notUsedE[minE].ValTo.ValueNode);
                    notUsedV.Remove(notUsedE[minE].ValTo.ValueNode);
                }
                else
                {
                    usedV.Add(notUsedE[minE].ValFrom.ValueNode);
                    notUsedV.Remove(notUsedE[minE].ValFrom.ValueNode);
                }
                //заносим новое ребро в дерево и удаляем его из списка неиспользованных
                resList.Add(notUsedE[minE]);
                notUsedE.RemoveAt(minE);
            }
        }

        //   1
        // /  \
        //3  - 2

        public void PrintResList ()
        {
            for (int i = 0; i < resList.Count; i++)
            {
                Console.WriteLine(resList[i].ValFrom.ValueNode + " " + resList[i].ValTo.ValueNode + " " + "weihgt = " + resList[i].Weight);
            }
        }


        private int[] DijkstraD(int node)
        {
            int[] d = new int[ArrNode.Count];
            string[] p = new string[ArrNode.Count];
            bool[] used = new bool[ArrNode.Count];

            for (int i = 0; i < ArrNode.Count; i++)
            {
                if (ArrNode[i].ValueNode == node)
                {
                    d[i] = 0;
                    used[i] = true;
                }
                else
                {
                    d[i] = 1000;
                    used[i] = false;
                }
                p[i] = "";
            }

            for (int j = 0; j < ArrNode.Count; j++)
            {

                for (int i = 0; i < ArrNode.Count; i++)
                {
                    if (!used[i])
                    {
                        if (FindIndexEdgeForWeight(ArrNode[j], ArrNode[i]) > -1)
                        {
                            if (d[i] > d[j] + ArrForWeightEdge[FindIndexEdgeForWeight(ArrNode[j], ArrNode[i])].Weight)
                            {
                                //Console.WriteLine(ArrNode[j].ValueNode + " " + ArrNode[i].ValueNode);
                                d[i] = d[j] + ArrForWeightEdge[FindIndexEdgeForWeight(ArrNode[j], ArrNode[i])].Weight;
                                p[i] = p[j] + " " + ArrNode[i].ValueNode;
                            }
                        }
                    }

                }

                if (!used[j])
                {
                    //пусть это вершина с мин d[j]
                    used[j] = true;
                }
            }

            return d;
        }

        private string[] DijkstraP(int node)
        {
            int[] d = new int[ArrNode.Count];
            string[] p = new string[ArrNode.Count];
            bool[] used = new bool[ArrNode.Count];

            for (int i = 0; i < ArrNode.Count; i++)
            {
                if (ArrNode[i].ValueNode == node)
                {
                    d[i] = 0;
                    used[i] = true;
                }
                else
                {
                    d[i] = 1000;
                    used[i] = false;
                }
                p[i] = "";
            }

            for (int j = 0; j < ArrNode.Count; j++)
            {

                for (int i = 0; i < ArrNode.Count; i++)
                {
                    if (!used[i])
                    {
                        if (FindIndexEdgeForWeight(ArrNode[j], ArrNode[i]) > -1)
                        {
                            if (d[i] > d[j] + ArrForWeightEdge[FindIndexEdgeForWeight(ArrNode[j], ArrNode[i])].Weight)
                            {
                                //Console.WriteLine(ArrNode[j].ValueNode + " " + ArrNode[i].ValueNode);
                                d[i] = d[j] + ArrForWeightEdge[FindIndexEdgeForWeight(ArrNode[j], ArrNode[i])].Weight;
                                p[i] = p[j] + " " + ArrNode[i].ValueNode;
                            }
                        }
                    }

                }

                if (!used[j])
                {
                    //пусть это вершина с мин d[j]
                    used[j] = true;
                }
            }

            for (int i = 0; i < p.Length; i++)
            {
                p[i] = p[i].Insert(0, System.Convert.ToString(node));
                //Console.WriteLine(p[i]);
            }

            return p;
        }

        public void IV_A (int u, int v)
        {
            int[] Arr = DijkstraD(u);

            int indexV = -1;
            for (int i = 0; i < ArrNode.Count; i++)
            {
                if (ArrNode[i].ValueNode == v)
                {
                    indexV = i;
                }
            }

            int val = Arr[indexV];
            Console.WriteLine(u + " " + v + ": " + val);

            for (int i = 0; i < ArrNode.Count; i++)
            {
                for (int j = 0; j < ArrNode.Count; j++)
                {
                    if (ArrNode[i].ValueNode != ArrNode[j].ValueNode)
                    {
                        int[] temp = DijkstraD(ArrNode[i].ValueNode);

                        int indexT = -1;
                        for (int q = 0; q < ArrNode.Count; q++)
                        {
                            if (ArrNode[q].ValueNode == ArrNode[j].ValueNode)
                            {
                                indexT = q;
                            }
                        }

                        int v1 = temp[indexT];

                        //Console.WriteLine(ArrNode[i].ValueNode + " " + ArrNode[j].ValueNode + " " + v1 + " " + val);
                        if (v1 == val)
                        {
                            string[] pT = DijkstraP(ArrNode[i].ValueNode);
                            Console.WriteLine(pT[j]);
                        }

                    }
                } 
            }
        }

        private void Bell_Ford(int s)
        {
            int[] d = new int[ArrNode.Count];

            for (int i = 0; i < ArrNode.Count; i++)
            {
                d[i] = 1000;

                if (ArrNode[i].ValueNode == s)
                {
                    d[i] = 0;
                }
            }

            for (int i = 0; i < ArrNode.Count - 1; i++)
            {
                for (int j = 0; j < ArrForWeightEdge.Count; j++)
                {
                    int ind = FindIndexOfNode(ArrForWeightEdge[j].ValFrom);
                    if (d[ind] < 1000)
                    {
                        if (d[FindIndexOfNode(ArrForWeightEdge[j].ValTo)] > d[ind] + ArrForWeightEdge[j].Weight)
                        {
                            d[FindIndexOfNode(ArrForWeightEdge[j].ValTo)] = d[ind] + ArrForWeightEdge[j].Weight;
                        }
                    }
                }
            }

            for (int i = 0; i < d.Length; i++)
            {
                Console.WriteLine(s + " " + ArrNode[i].ValueNode + " " + d[i]);
            }
        }

        public void IV_B()
        {
            for (int i = 0; i < ArrNode.Count; i++)
            {
                Bell_Ford(ArrNode[i].ValueNode);
            }
        }
    }
}
