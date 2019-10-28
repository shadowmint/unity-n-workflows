using System;

namespace N.Package.Workflows
{
    public class WorkflowError : Exception
    {
        public WorkflowError(WorkflowErrorCode code, string message) : base($"{code}: {message}")
        {
        }
    }
}