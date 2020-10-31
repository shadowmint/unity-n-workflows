using UnityEngine;

namespace N.Package.Workflows
{
    public class WorkflowLogger
    {
        private string _id;

        public void SetContext(string id)
        {
            _id = id;
        }

        public virtual void Info(string message)
        {
            Debug.Log($"Workflow {_id}: {message}");
        }

        public virtual void Warn(string message)
        {
            Debug.LogWarning($"Workflow {_id}: {message}");
        }
    }
}