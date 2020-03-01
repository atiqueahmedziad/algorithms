using System;
using System.Collections.Generic;
using System.Linq;

/* 

Based on Non Preemptive Algorithm

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
            this.processId = processId;
            this.cpuTime = cpuTime;
            this.priority = priority;
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            List<Process> processes = new List<Process>();

            Console.WriteLine("How many process do you want to enter ?");
            int n = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Enter CPU time for proceess {i + 1}: ");
                double cpuTime = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine($"Enter priority for proceess {i + 1}: ");
                double priority = Convert.ToDouble(Console.ReadLine());

                // Arrival time is assigned 0 for all process
                Process process = new Process(i + 1, 0, cpuTime, priority);

                processes.Add(process);
            }

            processes = processes.OrderBy(process => process.priority).ToList();

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
