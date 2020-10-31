namespace N.Package.Workflows
{
    public interface IWorkflowLike
    {
        string Id { get; }
        
        bool Aborted { get; }
        
        bool Failed { get; }

        void Abort();
    }
}