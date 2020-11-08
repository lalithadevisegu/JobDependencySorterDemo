using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobDependencyCharSorterDemo;

namespace JobDependencySorterDemo
{
    class Program
    {
        private const string EXIT_STRING = "EXIT";

        static void Main(string[] args)
        {
            Console.WriteLine($"Input your jobs here line by line in this formt \"a => b\" " +
                $"(type {EXIT_STRING} to terminate & porcess the jobs): ");
            var jobs = new List<string>();
            // reading job list entries
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                if (line.ToUpper() == EXIT_STRING)
                {
                    Console.WriteLine("Processing jobs, and the output is:");
                    break;
                }
                jobs.Add(line);
            }
            var jobSorter = new JobSort();
            Console.WriteLine(jobSorter.ProcessJobs(jobs.ToArray()));
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }  
}
