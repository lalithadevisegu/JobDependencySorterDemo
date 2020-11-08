using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobDependencySorterDemo.Classes;

namespace JobDependencyCharSorterDemo
{ 
    /// <summary>
    /// Job Sorter class, sorts a sequence of jobs based on their DependencyChar
    /// </summary>
    public class JobSort
    {
    /// <summary>
    /// Sorts jobs based on their DependencyChar
    /// </summary>
    /// <param JobName="jobs">array of strings of jobs to be sorted, ex: "a => " or "a => b"</param>
    /// <returns>string of sorted jobs</returns>
    public string ProcessJobs(string[] jobs)
    {
        var jobList = new List<Job>();
        try
        {
            foreach (var jobString in jobs)
            {
                jobList.Add(ParseJobString(jobString));
            }
            var sortedJobs = SortJobs(jobList);
            var output = PrintJobs(sortedJobs);
            return output;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    /// <summary>
    /// Parses a single job string into Job object
    /// </summary>
    /// <param JobName="job">job string to be parse</param>
    /// <returns>Job object</returns>
    private Job ParseJobString(string job)
    {
        if (string.IsNullOrWhiteSpace(job))
        {
            return new Job(string.Empty, null);
        } 
            var splittedJob = job.Split(new string[] { "=>"}, StringSplitOptions.RemoveEmptyEntries);
            // trim spaces at the beginning/end of each job. "a " will be "a"
            for (int i = 0; i < splittedJob.Length; i++)
        {
            splittedJob[i] = splittedJob[i].Trim();
        }
        if (splittedJob.Length == 1) // job has no DependencyChar
        {
            return new Job(splittedJob[0], null);
        }
        if (splittedJob[0] == splittedJob[1])
        {
            throw new Exception($"Job {splittedJob[0]} can not have DependencyChar on itself");
        }
        return new Job(splittedJob[0], splittedJob[1]);
    }

    /// <summary>
    /// Takes a list of unsorted Jobs and sort them based on their DependencyChar. 
    /// </summary>
    /// <param JobName="jobs">List of Jobs</param>
    /// <returns>List of Jobs sorted</returns>
    private List<Job> SortJobs(List<Job> jobs)
    {
        var sortedJobs = new List<Job>();
        var visitedJobs = new List<Job>();
        foreach (var job in jobs)
        {
            VisitJob(job, sortedJobs, visitedJobs, jobs);
        }
        return sortedJobs;
    }

    /// <summary>
    /// Goes through every job DependencyChar and add it to the sorted list and mark this job as visited
    /// </summary>
    /// <param JobName="job">the job object to get its DependencyChar</param>
    /// <param JobName="sortedJobs">list of sorted jobs</param>
    /// <param JobName="visitedJobs">list of visited jobs</param>
    /// <param JobName="unsortedJobs">list of unsorted jobs(the original ones)</param>
    private void VisitJob(Job job, List<Job> sortedJobs, List<Job> visitedJobs, List<Job> unsortedJobs)
    {
        // job already visited before, return unless there is circular dependencies
        if (visitedJobs.Any(x => x.JobName.Equals(job.JobName)))
        {
            // if not exist in the sorted list, then circular dependencies found
            if (!sortedJobs.Any(x => x.JobName.Equals(job.JobName)))
            {
                throw new Exception("Jobs can’t have circular dependencies");
            }
            return;
        }
        visitedJobs.Add(job);

        // visit the job's DependencyChar (in case it has) and add it to the sorted list first
        if (!string.IsNullOrWhiteSpace(job.DependencyChar))
        {
            var DependencyCharJob = unsortedJobs.Where(x => x.JobName == job.DependencyChar).FirstOrDefault();
            if (DependencyCharJob != null)
            {
                // recursive loop for all related dependencies
                VisitJob(DependencyCharJob, sortedJobs, visitedJobs, unsortedJobs);
            }
        }
        // add the current job to the sorted list after looping through its dependencies
        sortedJobs.Add(job);
    }

    /// <summary>
    /// Takes a list of Jobs and prints their JobNames in one string
    /// </summary>
    /// <param JobName="jobs">job list to be printed</param>
    /// <returns>job JobNames in string</returns>
    private string PrintJobs(List<Job> jobs)
    {
        var output = new StringBuilder();
        foreach (var job in jobs)
        {
            output.Append(job.JobName);
        }
        return output.ToString();
    }
}
}
