using System;
using Microsoft.Build.Framework;
using System.Management;
using Microsoft.Build.Utilities;
using System.Management.Automation;
using System.IO;
using System.Collections;
using System.Collections.ObjectModel;
using SmartTesterTask.Properties;
using System.Collections.Generic;
using TestMapper;

namespace SmartTesterTask
{
    public class SmartTestingTask : Task
    {
        [Required]
        public string RepositoryOwner { get; set; }
        [Required]
        public string RepositoryName { get; set; }
        [Required]
        public string PullRequestNumber { get; set; }
        [Output]
        public string[] TestAssemblies { get; set; }
        public override bool Execute()
        {
            string mapFilePath = @"c:\users\t-netron\documents\visual studio 2017\Projects\SmartTesterTask\SmartTesterTask\map.json";
           // Call the PowerShell.Create() method to create an 
           // empty pipeline.
            PowerShell powerShell = PowerShell.Create();

            powerShell.AddScript(Resources.GetFilesScript);
            powerShell.AddScript($"Get-PullRequestFileChanges -RepositoryOwner {RepositoryOwner} " +
                                         $"-RepositoryName {RepositoryName} -PullRequestNumber {PullRequestNumber}");

            // invoke execution on the pipeline (collecting output)
            Collection<PSObject> psOutput = powerShell.Invoke();
            List<string> filesChanged = new List<string>();

            if (psOutput == null)
            {
                return false;
            }

            foreach (var element in psOutput)
            {
                filesChanged.Add(element.ToString());
            }

            TestAssemblies = new List<string>(TestSetGenerator.GetTests(filesChanged, mapFilePath)).ToArray();

            return true;
        }
    }
}
