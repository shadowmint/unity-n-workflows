using System.Threading.Tasks;
using N.Package.Workflows;
using N.Package.Workflows.Infrastructure;
using NUnit.Framework;

namespace Editor.Tests
{
    public class SourceTests
    {
        [Test]
        public void TestNormalWorkflow()
        {
            var manager = new TestWorkflowManager();
            var result = new TestWorkflow().Run(manager).Result;
            Assert.IsTrue(result);
        }
    }

    public class TestWorkflow : Workflow
    {
        public override string Id => "Test123";

        protected override Task<bool> Execute()
        {
            return Task.FromResult(true);
        }

        protected override void Validate()
        {
        }
    }
}