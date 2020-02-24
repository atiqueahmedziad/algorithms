using System;

namespace FCFS
{
    struct Process {
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

        public void setWaitTime(double wt) {
            waitTime = wt;
        }

        public void setTurnAroundTime(double tt) {
            turnAroundTime = tt;
        }
    };

    class MainClass {

        public static double avgWaitTime(Process[] process) {
            double sumWaitTime = 0;
            for (int i = 0; i < process.Length; i++)
            {
                sumWaitTime += process[i].waitTime;
            }
            return sumWaitTime / process.Length;
        }

        public static double avgTurnAroundTime(Process[] process) {
            double sumTurnAroundTime = 0;
            for (int i = 0; i < process.Length; i++)
            {
                sumTurnAroundTime += process[i].turnAroundTime;
            }
            return sumTurnAroundTime / process.Length;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine($"How many process do you want to enter: ");
            int n = Convert.ToInt32(Console.ReadLine());

            Process[] process = new Process[n];

            for(int i=0; i<process.Length; i++) {
                Console.WriteLine($"Enter arrival time for process {i}: ");
                double arrivalTime = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine($"Enter cpu time time for process {i}: ");
                double cpuTime = Convert.ToDouble(Console.ReadLine());

                process[i] = new Process();

                process[i].setValues(i+1, arrivalTime, cpuTime);
            }

            // Sort the processes in assending order according to their arrival time
            Array.Sort<Process>(process, (x, y) => x.arrivalTime.CompareTo(y.arrivalTime));

            // wait time of 1st process is always 0
            process[0].setWaitTime(0);


            // As suggested in Classroom's manual (algorithm)
            //for (int i = 1; i < process.Length; i++) {
            //    double processWaitTime = process[i-1].cpuTime + process[i-1].waitTime - process[i].arrivalTime;
            //    process[i].setWaitTime(processWaitTime);
            //}


            double sumCpuTime = 0;
            // set wait time of each process
            for (int i = 1; i<process.Length; i++) {
                sumCpuTime += process[i-1].cpuTime;
                double processWaitTime = sumCpuTime - process[i].arrivalTime;
                process[i].setWaitTime(processWaitTime);
            }

            // set turn around time of each process
            for(int i = 0; i<process.Length; i++) {
                double turnAroundTime = process[i].cpuTime + process[i].waitTime;
                process[i].setTurnAroundTime(turnAroundTime);
            }

            double avgWT = avgWaitTime(process);
            double avgTT = avgTurnAroundTime(process);

            for(int i=0; i<process.Length; i++) {
                Console.WriteLine($"Process {process[i].processId}: Waiting time: {process[i].waitTime}  Turnarround Time: {process[i].turnAroundTime}");
            }

            Console.WriteLine($"Average waiting time: {avgWT}");
            Console.WriteLine($"Average turn around time: {avgTT}");

            Console.ReadLine();
        }
    }
}
