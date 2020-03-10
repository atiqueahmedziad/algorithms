using System;
using System.Collections.Generic;
using System.Linq;

namespace DeadLock {
    class Node {
        public char name;
        public List<char> adjNode = new List<char>();
        // All nodes are unvisited initially
        public bool visited = false;

        public Node(char name) {
            this.name = name;
        }
    }

    class MainClass {
        // nodeList contains all the input nodes
        public static List<Node> nodeList = new List<Node>();
        // deadLockList contains all the deadlock cycles (each deadlock cycle as an item in the list)
        public static List<string> deadLockList = new List<string>();
        // It contains the nodes of deadlock cycle
        public static string deadLocks;

        public static void addEdge(char nodeName, char adjNode) {
            Node node = nodeList.Find(eachNode => eachNode.name.Equals(nodeName));
            node.adjNode.Add(adjNode);
        }

        // Returning the unvisited nodes list
        public static List<Node> filterNodeList() {
            return nodeList.FindAll(node => !node.visited);
        }

        // Returning Node object with a given node name
        public static Node findNodeWithChar(char nodeName) {
            return nodeList.Find(node => node.name.Equals(nodeName));
        }

        public static void DFS(Node node) {
            node.visited = true;
            deadLocks = deadLocks.Insert(deadLocks.Length, node.name.ToString());

            // Determining the adjacent nodes of given Node.
            foreach(var adjNode in node.adjNode) {
                Node newNode = findNodeWithChar(adjNode);
                // If deadlocks string already contains the adjacent node name
                // it is a deadLock.
                if (deadLocks.Contains(newNode.name)) {
                    int index = deadLocks.IndexOf(newNode.name);
                    string deadLockCycle = deadLocks.Substring(index);
                    deadLockList.Add(deadLockCycle);
                }
                else if(newNode!= null && !newNode.visited) {
                    DFS(newNode);
                }
            }
            if(filterNodeList().Count != 0) {
                // DFSInitilize() method is calling with remaining unvisited nodes
                // filterNodeList() method returning the list of unvisited nodes
                DFSInitialize(filterNodeList());
            }
        }

        public static void DFSInitilize(List<Node> nodeList) {
            deadLocks = "";
            // Initializing DFS with first node of given nodeList
            DFS(nodeList[0]);
        }

        public static void Main(string[] args) {
            Console.WriteLine("Enter node names:");
            string nodeName = Console.ReadLine();

            Console.WriteLine("How many edges: ");
            int numOfEdges = Convert.ToInt32(Console.ReadLine());

            foreach(var eachNode in nodeName) {
                Node node = new Node(eachNode);
                nodeList.Add(node);
            }

            Console.WriteLine("Enter Edges: ");
            // Example
            // AB
            // BC
            for (int i=0; i<numOfEdges; i++) {
                string edge = Console.ReadLine();
                edge.Trim();
                addEdge(edge.First(), edge.Last());
            }

            DFSInitialize(nodeList);

            foreach(var dLock in deadLockList) {
                Console.WriteLine($"DeadLock cycle is: {dLock}");
            }

            /*
            --- DeadLock Recovery ---
            From each deadLock cycle we took the first and last node.
            The last node must have first node as its adjacent node.
            We delete the first node from last node's adjacent node list.
            */
            foreach(var dLockCycle in deadLockList) {
                char firstNodeVal = dLockCycle.First();
                char lastNodeVal = dLockCycle.Last();

                Node lastNode = findNodeWithChar(lastNodeVal);
                lastNode.adjNode.Remove(firstNodeVal);
            }

            // Clearing the existing deadLock list
            // since we will be running DFS again to check for deadLock cycle
            deadLockList.Clear();

            Console.WriteLine($"Run DFS after fixing deadlock cycles");
            DFSInitialize(nodeList);

            foreach (var dLock in deadLockList) {
                Console.WriteLine($"DEADLOCK IS HERE: {dLock}");
            }

            // If deadLockList is empty, there is no deadlock in the system
            if (deadLockList.Count == 0) {
                Console.WriteLine($"NO DEAD LOCK FOUND :)");
            }

            Console.ReadLine();
        }
    }
}
