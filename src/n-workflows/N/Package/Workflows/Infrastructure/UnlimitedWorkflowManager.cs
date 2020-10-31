using System.Collections.Generic;

namespace N.Package.Workflows.Infrastructure
{
    public class UnlimitedWorkflowManager : IWorkflowManager
    {
        public IEnumerable<IWorkflowLike> Active => _active.ToArray();
        
        private readonly List<IWorkflowLike> _active = new List<IWorkflowLike>();

        public void StartWorkflow<T>(Workflow<T> workflow)
        {
            _active.Add(workflow);
        }

        public void EndWorkflow<T>(Workflow<T> workflow)
        {
            _active.Remove(workflow);
        }
    }
}