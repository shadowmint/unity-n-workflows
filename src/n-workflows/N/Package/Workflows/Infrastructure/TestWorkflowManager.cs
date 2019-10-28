using System;

namespace N.Package.Workflows.Infrastructure
{
    public class TestWorkflowManager : IWorkflowManager
    {
        private IWorkflowLike _workflow;

        public void StartWorkflow<T>(Workflow<T> workflow)
        {
            if (_workflow != null)
            {
                throw new NotSupportedException();
            }

            _workflow = workflow;
        }

        public void EndWorkflow<T>(Workflow<T> workflow)
        {
            _workflow = null;
        }
    }
}