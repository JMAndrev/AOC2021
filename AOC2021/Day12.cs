using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AOC2021
{
    class Day12
    {
        private static List<string> fileInputList = File.ReadAllLines(@"Day12.txt").ToList();
        private static Dictionary<string, Node> nodes;
        private static List<string> visitedNodesList;
        private static List<List<string>> allPathsList;
        public static string Run()
        {
            nodes = new Dictionary<string, Node>();
            visitedNodesList = new List<string>();
            allPathsList = new List<List<string>>();
            BuildGraph(fileInputList);
            visitedNodesList.Clear();
            int solution1 = Part1();
            int solution2 = Part2();

            return ("part 1: " + solution1 + " ; " + "part2: " + solution2);
        }
        private static void BuildGraph(List<string> dataList)
        {
            for (int i = 0; i < dataList.Count; i++)
            {
                List<string> tempList = new List<string>(fileInputList[i].Split('-').ToList());
                string nodeName1 = tempList[0];
                string nodeName2 = tempList[1];
                Node n1 = new Node(nodeName1);
                Node n2 = new Node(nodeName2);
                if (!nodes.ContainsKey(nodeName1))
                    nodes.Add(nodeName1, n1);
                if (!nodes.ContainsKey(nodeName2))
                    nodes.Add(nodeName2, n2);
                n1 = nodes[nodeName1];
                n2 = nodes[nodeName2];
                n1.AddEdge(n2);
                n2.AddEdge(n1);
                nodes[nodeName1] = n1;
                nodes[nodeName2] = n2;
            }
        }

        private static int Part1()
        {
            Node startNode = nodes["start"];
            List<string> localPath = new List<string>();
            FindAllPathsPart1(startNode, allPathsList, localPath);
            return allPathsList.Count();
        }

        private static int Part2()
        {
            Node startNode = nodes["start"];
            List<string> localPath = new List<string>();
            FindAllPathsPart2(startNode, allPathsList, localPath, false);
            return allPathsList.Count();
        }
        private static void FindAllPathsPart1(Node cave, List<List<string>> computedPaths, List<string> localPath)
        {
            localPath.Add(cave.GetName());

            if (cave.GetName().Equals("end"))
            {
                computedPaths.Add(localPath);
                localPath.Remove(cave.GetName());
                return;
            }

            if (cave.GetSize() == false) 
                visitedNodesList.Add(cave.GetName());

            foreach (Node n in cave.GetNeighbours())
            {
                if (!visitedNodesList.Contains(n.GetName()) && !n.GetName().Equals("start"))
                {
                    List<string> deepPath = localPath.ToList();
                    FindAllPathsPart1(n, computedPaths, deepPath);
                }
            }
            visitedNodesList.Remove(cave.GetName());
        }

        private static void FindAllPathsPart2(Node mainNode, List<List<string>> computedPaths, List<string> localPath, bool twice)
        {

            localPath.Add(mainNode.GetName());

            if (mainNode.GetName().Equals("end"))
            {
                computedPaths.Add(localPath);
                localPath.Remove(mainNode.GetName());         
                return;
            }

            if (mainNode.GetSize() == false && !mainNode.GetName().Equals("end"))
            {
                mainNode.SetVisited(1);
                if (mainNode.GetVisited() >= 2 && twice == true)
                    return;
                else if (mainNode.GetVisited() == 2 && twice == false)
                    twice = true;
            }

            foreach (Node node in mainNode.GetNeighbours())
            {
                if (!node.GetName().Equals("start") && ((node.GetVisited() == 0 || node.GetSize() == true) ||
                        (node.GetSize() == false && twice == false && node.GetVisited() == 1)))
                {
                    List<string> deepPath = localPath.ToList();

                    FindAllPathsPart2(node, computedPaths, deepPath, twice);
                    if (node.GetSize() == false && !node.GetName().Equals("end"))
                    {
                        node.SetVisited(-1);
                    }
                }
            }
             //visitedNodesList.Remove(mainNode.GetName());
        }

        private static void FindPaths(Node mainNode, List<List<string>> computedPaths, List<string> localPath, bool twice, int part)
        {
            localPath.Add(mainNode.GetName());

            if (mainNode.GetName().Equals("end"))
            {
                computedPaths.Add(localPath);
                localPath.Remove(mainNode.GetName());
                return;
            }

            if (mainNode.GetSize() == false && !mainNode.GetName().Equals("end"))
            {
                if (part == 1)
                    visitedNodesList.Add(mainNode.GetName());
                else
                {
                    mainNode.SetVisited(1);
                    if (mainNode.GetVisited() >= 2 && twice == true)
                        return;
                    else if (mainNode.GetVisited() == 2 && twice == false)
                        twice = true;
                }
            }

            foreach (Node node in mainNode.GetNeighbours())
            {
                if (!visitedNodesList.Contains(node.GetName()) && !node.GetName().Equals("start"))
                {
                    List<string> deepPath = new List<string>();
                    deepPath.AddRange(localPath);
                    FindAllPathsPart1(node, computedPaths, deepPath);
                }
            }

            foreach (Node node in mainNode.GetNeighbours())
            {
                if (part == 1 && !node.GetName().Equals("start") && !visitedNodesList.Contains(node.GetName()))
                {
                    List<String> deepPath = new List<string>();
                    deepPath.AddRange(localPath);
                    FindPaths(node, computedPaths, deepPath, twice, part);
                }
                if (part == 2 && !node.GetName().Equals("start") &&
                        ((node.GetVisited() == 0 || node.GetSize() == true) ||
                        (node.GetSize() == false && twice == false && node.GetVisited() == 1)))
                {
                    List<string> deepPath = new List<string>();
                    deepPath.AddRange(localPath);
                    FindPaths(node, computedPaths, deepPath, twice, part);

                    if (node.GetSize() == false && !node.GetName().Equals("end") && part == 2)
                    {
                        node.SetVisited(-1);
                    }
                }
            }
            visitedNodesList.Remove(mainNode.GetName());
        }

        class Node
        {
            string name;
            int visited;
            bool isBig;
            List<Node> neighbours;
            public Node(string name)
            {
                this.name = name;
                this.neighbours = new List<Node>();
                this.visited = 0;
                SetSize(this.name);
            }
            public void AddEdge(Node neighbour) {    neighbours.Add(neighbour); }
            public void SetVisited(int visit) {  visited += visit; }
            public void SetSize(string called)
            {
                if (called.ToLower().Equals(called))
                    isBig = false;
                else
                    isBig = true;
            }
            public bool GetSize() { return isBig; }
            public string GetName() { return name; }
            public List<Node> GetNeighbours() { return neighbours; }
            public int GetVisited() { return visited; }
        }
    }
}