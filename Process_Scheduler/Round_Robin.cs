using System;
using System.Collections.Generic;
using System.Linq;

namespace RoundRobin
{
    class Process
    {
        public double processId;
        public double arrivalTime;
        public double cpuTime;
        public double remainTime;
        public double waitTime;
        public double completionTime;
        public double turnAroundTime;
        public double lastIndex;

        public Process(double processId, double arrivalTime, double cpuTime, double remainTime)
        {
            this.arrivalTime = arrivalTime;
            this.processId = processId;
            this.cpuTime = cpuTime;
            this.remainTime = remainTime;
            // Initially lastIndex should be arrival time
            lastIndex = arrivalTime;
        }
    }

    class MainClass
    {

        public static double turnAroundTime(Process process)
        {
            return process.cpuTime + process.waitTime;
        }

        public static void Main(string[] args)
        {
            Queue<Process> queue = new Queue<Process>();

            List<Process> processes = new List<Process>();

            Console.WriteLine("How many process do you want to enter ?");
            int n = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Enter Quantum Time: ");
            double timeQuantum = Convert.ToDouble(Console.ReadLine());

            for (int i=0; i<n; i++)
            {
                Console.WriteLine($"Enter Arrival time for proceess {i+1}: ");
                double arrivalTime = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine($"Enter CPU time for proceess {i + 1}: ");
                double cpuTime = Convert.ToDouble(Console.ReadLine());

                double remainTime = cpuTime;

                Process process = new Process(i + 1, arrivalTime, cpuTime, remainTime);

                processes.Add(process);
            }

            // Process list is arranged in ascending order according to their arrival time initally 
            processes = processes.OrderBy(process => process.arrivalTime).ToList();

            // This queue initally holds all the prosesses.
            Queue<Process> tempQueue = new Queue<Process>();

            foreach (var process in processes)
            {
                tempQueue.Enqueue(process);
            }

            // sending first process from tempQueue to the queue
            queue.Enqueue(tempQueue.Dequeue());

            double timeCount = 0;
            
            while (queue.Count != 0)
            {
                Process process = queue.Dequeue();

                // lastIndex is the arrival time initally
                process.waitTime += timeCount - process.lastIndex;
               
                if (process.remainTime >= timeQuantum)
                {
                    process.remainTime -= timeQuantum;
                    timeCount += timeQuantum;
                } else
                {
                    timeCount += process.remainTime;
                    // When remaining time is less than Time Quantum
                    // the process finishes, so remaining time becomes 0.
                    process.remainTime = 0;
                }

                process.completionTime = timeCount;

                if (tempQueue.Count != 0) {
                    // Count is used to keep track of how many process has been added
                    // so that we can dequeue those from tempQueue.
                    int count = 0;
                    foreach(var eachProcess in tempQueue)
                    {
                        if (eachProcess.arrivalTime <= timeCount)
                        {
                            count++;
                            queue.Enqueue(eachProcess);
                        }
                    }
                    for(int i=0; i<count; i++)
                    {
                        // We will be dequeuing as much process as included above in line no 103-106
                        tempQueue.Dequeue();
                    }
                    
                }

                if (process.remainTime != 0)
                {
                    queue.Enqueue(process);
                    // Saves the previous completion time of a process
                    process.lastIndex = timeCount;
                }

            }

            // Arrange the process list according to id for output.
            processes = processes.OrderBy(process => process.processId).ToList();

            double totalWaitTime = 0, totalTurnAroundTime = 0;

            foreach (var process in processes)
            {
                process.turnAroundTime = turnAroundTime(process);

                totalWaitTime += process.waitTime;

                totalTurnAroundTime += process.turnAroundTime;

                Console.WriteLine($"process {process.processId} wait time is {process.waitTime} completion time is {process.completionTime} turn around time is {process.turnAroundTime}");
            }

            Console.WriteLine($"Average Waiting Time {totalWaitTime / processes.Count}");
            Console.WriteLine($"Average Turnaround Time {totalTurnAroundTime / processes.Count}");

            Console.ReadLine();
        }
    }
}
