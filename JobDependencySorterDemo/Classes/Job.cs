using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace JobDependencySorterDemo.Classes
{
    /// <summary>
    /// Job class stores information about job name and its dependency
    /// </summary>
    public class Job
    {
        /// <summary>
        /// Job name
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// name of the dependency job which the current Job object depends on it
        /// </summary>
        public string DependencyChar { get; set; }

        public Job(string job, string dependency)
        {
            JobName = job;
            DependencyChar = dependency;
        }
    }
}
