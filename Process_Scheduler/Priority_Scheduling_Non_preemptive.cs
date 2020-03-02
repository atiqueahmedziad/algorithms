using System;
using System.Collections.Generic;
using System.Linq;

/* 

Based on Non Preemtive Algorithm

*/

namespace PS
{
    class Process
    {
        public double processId;
        public double arrivalTime;
        public double cpuTime;
        public double waitTime;
        public double turnAroundTime;
        public double priority;

        public Process(double processId, double arrivalTime, double cpuTime, double priority)
        {
            this.arrivalTime = arrivalTime;
            this.cpuTime = cpuTime;
            this.processId = processId;
            this.priority = priority;
        }
    };

    class MainClass
    {

        public static List<Process> sortedList(List<Process> processes)
        {
            double sumOfCpuTime = 0;
            int itemCount = 0;

            // Since we are already sending 1st process in the sortedProcess List
            int itemInQueue = processes.Count - 1;

            // Sort the processes in assending order according to their arrival time
            processes = processes.OrderBy(process => process.arrivalTime).ToList();

            List<Process> sortedProcess = new List<Process>();

            sortedProcess.Add(processes[0]);

            for (int i = 1; i < processes.Count; i++)
            {
                List<Process> temp = new List<Process>();

                sumOfCpuTime += processes[i - 1].cpuTime;

                for (int j = itemCount + 1; j < processes.Count; j++)
                {
                    if (processes[j].arrivalTime <= sumOfCpuTime)
                    {
                        temp.Add(processes[j]);
                        itemCount++;
                    }
                }
                if (temp.Count != 0 && itemCount != itemInQueue)
                {
                    temp = temp.OrderBy(process => process.priority).ToList();
                    sortedProcess.Add(temp[0]);
                    itemCount = itemCount - temp.Count + 1;
                }

                else if (temp.Count != 0 && itemCount == itemInQueue)
                {
                    temp = temp.OrderBy(process => process.priority).ToList();
                    sortedProcess.AddRange(temp);
                }

                if (itemCount == itemInQueue)
                {
                    break;
                }
            }

            return sortedProcess;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine($"How many process do you want to enter: ");
            int n = Convert.ToInt32(Console.ReadLine());

            List<Process> processes = new List<Process>();

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Enter arrival time for process {i + 1}: ");
                double arrivalTime = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine($"Enter cpu time for process {i + 1}: ");
                double cpuTime = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine($"Enter priority for process {i + 1}: ");
                double priority = Convert.ToDouble(Console.ReadLine());

                Process process = new Process(i + 1, arrivalTime, cpuTime, priority);

                processes.Add(process);
            }

            processes = sortedList(processes);

            // If you want to see the gantt chart, uncomment this
            //for (int i = 0; i < processes.Count; i++)
            //{
            //    Console.WriteLine($"Process {processes[i].processId}");
            //}

            double totalWaitTime = 0, totalTurnAroundTime = 0;

            double sumCpuTime = 0;

            // set wait time of each process
            for (int i = 1; i < processes.Count; i++)
            {
                sumCpuTime += processes[i - 1].cpuTime;
                double processWaitTime = sumCpuTime - processes[i].arrivalTime;
                processes[i].waitTime = processWaitTime;
                totalWaitTime += processWaitTime;
            }

            // set turnaround time of each process
            for (int i = 0; i < processes.Count; i++)
            {
                double turnAroundTime = processes[i].cpuTime + processes[i].waitTime;
                processes[i].turnAroundTime = turnAroundTime;
                totalTurnAroundTime += turnAroundTime;
            }

            processes = processes.OrderBy(process => process.processId).ToList();

            for (int i = 0; i < processes.Count; i++)
            {
                Console.WriteLine($"Process {processes[i].processId}: Waiting time: {processes[i].waitTime}  Turnarround Time: {processes[i].turnAroundTime}");
            }

            Console.WriteLine($"Average waiting time: {totalWaitTime / processes.Count}");
            Console.WriteLine($"Average turn around time: {totalTurnAroundTime / processes.Count}");

            Console.ReadLine();
        }
    }
}
