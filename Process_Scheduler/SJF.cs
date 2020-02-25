using System;
using System.Collections.Generic;
using System.Linq;

namespace SJF
{
    class Process
    {
        public double processId;
        public double arrivalTime;
        public double cpuTime;
        public double waitTime;
        public double turnAroundTime;

        public void setValues(double pid, double at, double ct)
        {
            arrivalTime = at;
            cpuTime = ct;
            processId = pid;
        }

        public void setWaitTime(double wt)
        {
            waitTime = wt;
        }

        public void setTurnAroundTime(double tt)
        {
            turnAroundTime = tt;
        }
    };

    class MainClass
    {

        public static List<Process> sortedList(List<Process> processes)
        {
            double sumOfCpuTime = 0;
            int itemCount = 0;

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

                if (temp.Count != 0)
                {
                    temp = temp.OrderBy(process => process.cpuTime).ToList();
                    sortedProcess.AddRange(temp);
                }

                if (temp.Count == processes.Count - 1)
                {
                    break;
                }
            }

            return sortedProcess;
        }

        public static double avgWaitTime(List<Process> processes)
        {
            double sumWaitTime = 0;
            for (int i = 0; i < processes.Count; i++)
            {
                sumWaitTime += processes[i].waitTime;
            }
            return sumWaitTime / processes.Count;
        }

        public static double avgTurnAroundTime(List<Process> processes)
        {
            double sumTurnAroundTime = 0;
            for (int i = 0; i < processes.Count; i++)
            {
                sumTurnAroundTime += processes[i].turnAroundTime;
            }
            return sumTurnAroundTime / processes.Count;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine($"How many process do you want to enter: ");
            int n = Convert.ToInt32(Console.ReadLine());

            List<Process> processes = new List<Process>();

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Enter arrival time for process {i+1}: ");
                double arrivalTime = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine($"Enter cpu time time for process {i+1}: ");
                double cpuTime = Convert.ToDouble(Console.ReadLine());

                Process process = new Process();

                process.setValues(i + 1, arrivalTime, cpuTime);
                processes.Add(process);
            }

            processes = sortedList(processes);

            // wait time of 1st process is always 0
            processes[0].setWaitTime(0);

            double sumCpuTime = 0;

            // set wait time of each process
            for (int i = 1; i < processes.Count; i++)
            {
                sumCpuTime += processes[i - 1].cpuTime;
                double processWaitTime = sumCpuTime - processes[i].arrivalTime;
                processes[i].setWaitTime(processWaitTime);
            }

            // set turn around time of each process
            for (int i = 0; i < processes.Count; i++)
            {
                double turnAroundTime = processes[i].cpuTime + processes[i].waitTime;
                processes[i].setTurnAroundTime(turnAroundTime);
            }

            double avgWT = avgWaitTime(processes);
            double avgTT = avgTurnAroundTime(processes);

            for (int i = 0; i < processes.Count; i++)
            {
                Console.WriteLine($"Process {processes[i].processId}: arrival time: {processes[i].waitTime}  cpu Time: {processes[i].turnAroundTime}");

            }

            Console.WriteLine($"Average waiting time: {avgWT}");
            Console.WriteLine($"Average turn around time: {avgTT}");

            Console.ReadLine();
        }
    }
}
