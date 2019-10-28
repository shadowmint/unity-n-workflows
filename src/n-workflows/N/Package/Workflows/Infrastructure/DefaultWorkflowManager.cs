namespace N.Package.Workflows.Infrastructure
{
    public class DefaultWorkflowManager : IWorkflowManager
    {
        private static IWorkflowLike _activeWorkflow;

        public void StartWorkflow<T>(Workflow<T> workflow)
        {
            if (_activeWorkflow == null)
            {
                return;
            }

            throw new WorkflowError(WorkflowErrorCode.WorkflowAlreadyActive, $"Workflow {_activeWorkflow.Id} is already active.");
        }

        public void EndWorkflow<T>(Workflow<T> workflow)
        {
            _activeWorkflow = null;
        }
    }
}