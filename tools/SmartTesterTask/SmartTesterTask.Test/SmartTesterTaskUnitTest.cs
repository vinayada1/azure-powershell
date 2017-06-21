using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SmartTesterTask;

namespace SmartTesterTask.Test
{
    public class SmartTesterTaskUnitTest
    {
        [Fact]
        public void Execute_PullRequestWithFilesChangedThatMatchPathsInMap_ShouldReturnOnlyMatchingTests()
        {
            //arrange
            SmartTestingTask task = new SmartTestingTask();
            task.RepositoryName = "azure-powershell";
            task.RepositoryOwner = "Azure";
            task.PullRequestNumber = "4170";
            List<string> expected = new List<string>()
            {
                ".\\src\\ServiceManagement\\Common\\Commands.Common.Test\\bin\\Debug\\Microsoft.WindowsAzure.Commands.Common.Test.dll",
                ".\\src\\ServiceManagement\\Services\\Commands.Test\\bin\\Debug\\Microsoft.WindowsAzure.Commands.Test.dll",
                ".\\src\\ServiceManagement\\StorSimple\\Commands.StorSimple.Test\\bin\\Debug\\Microsoft.WindowsAzure.Commands.StorSimple.Test.dll",
                ".\\src\\ServiceManagement\\RemoteApp\\Commands.RemoteApp.Test\\bin\\Debug\\Microsoft.Azure.Commands.RemoteApp.Tests.dll",
                ".\\src\\ServiceManagement\\Common\\Commands.ScenarioTest\\bin\\Debug\\Microsoft.WindowsAzure.Commands.ScenarioTest.dll",
                ".\\src\\ServiceManagement\\RecoveryServices\\Commands.RecoveryServices.Test\\bin\\Debug\\Microsoft.Azure.Commands.RecoveryServices.Test.dll",
                ".\\src\\ServiceManagement\\Network\\Commands.Network.Test\\bin\\Debug\\Microsoft.WindowsAzure.Commands.ServiceManagement.Network.Test.dll",
                ".\\src\\ResourceManager\\UsageAggregates\\Commands.UsageAggregates.Test\\bin\\Debug\\Microsoft.Azure.Commands.UsageAggregates.Test.dll",
                ".\\src\\ResourceManager\\LogicApp\\Commands.LogicApp.Test\\bin\\Debug\\Microsoft.Azure.Commands.LogicApp.Test.dll"
            };
           

            //act
            task.Execute();
            var actual = new List<string>(task.TestAssemblies);
            actual.Sort();
            expected.Sort();

            //assert            
            Assert.True(actual.SequenceEqual(expected));

        }
        [Fact]
        public void Execute_PullRequestWithFilesChangedWithNonMatchingPathsInMap_ShouldReturnAllTests()
        {
            //arrange
            SmartTestingTask task = new SmartTestingTask();
            task.RepositoryName = "azure-powershell";
            task.RepositoryOwner = "Azure";
            task.PullRequestNumber = "4171";
            int expectedNumberTests = 50;
            int actualNumberTests;

            //act
            task.Execute();
            actualNumberTests = task.TestAssemblies.Count();

            //assert            
            Assert.True(actualNumberTests.Equals(expectedNumberTests));

        }
    }
}
