namespace N.Package.Workflows
{
    /// <summary>
    /// Looks after workflow interactions
    /// </summary>
    public interface IWorkflowManager
    {
        void StartWorkflow<T>(Workflow<T> workflow);
        void EndWorkflow<T>(Workflow<T> workflow);
    }
}