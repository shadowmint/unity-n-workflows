using System;
using System.Threading.Tasks;
using N.Package.Promises;
using N.Package.Workflows.Infrastructure;

namespace N.Package.Workflows
{
    public abstract class Workflow<T> : IWorkflowLike, IWorkflowManager
    {
        public abstract string Id { get; }

        protected WorkflowLogger Logger { get; set; } = new WorkflowLogger();

        protected WorkflowState State;

        public bool Aborted => State == WorkflowState.Aborted;

        public bool Failed => State == WorkflowState.Failed;

        public bool Resolved => State == WorkflowState.Resolved;

        public async Task<T> Run(IWorkflowManager manager = null)
        {
            if (manager == null)
            {
                manager = new DefaultWorkflowManager();
            }

            Logger.SetContext(Id);
            manager.StartWorkflow(this);
            Logger.Info("Start workflow");

            Logger.Info("Validate");
            Validate();

            Logger.Info("Workflow pending");
            try
            {
                var result = await Execute();
                if (Aborted)
                {
                    manager.EndWorkflow(this);
                    Logger.Warn("Workflow aborted");
                    return result;
                }

                State = WorkflowState.Resolved;
                manager.EndWorkflow(this);
                Logger.Info("Workflow completed");
                return result;
            }
            catch (Exception error)
            {
                State = WorkflowState.Failed;
                manager.EndWorkflow(this);
                Logger.Warn($"Workflow failed: {error}");
                throw;
            }
        }

        /// <summary>
        /// Execute this workflow async
        /// </summary>
        protected abstract Task<T> Execute();

        /// <summary>
        /// Validate the workflow
        /// </summary>
        protected abstract void Validate();

        /// <summary>
        /// Abort this task now
        /// </summary>
        public void Abort()
        {
            State = WorkflowState.Aborted;
        }

        /// <summary>
        /// Every workflow is the manager for it's child workflows.
        /// </summary>
        public void StartWorkflow<TOut>(Workflow<TOut> workflow)
        {
            if (_activeWorkflow == null)
            {
                return;
            }

            throw new WorkflowError(WorkflowErrorCode.WorkflowAlreadyActive, $"Workflow {_activeWorkflow.Id} is already active.");
        }

        /// <summary>
        /// Every workflow is the manager for it's child workflows.
        /// </summary>
        public void EndWorkflow<TOut>(Workflow<TOut> workflow)
        {
            _activeWorkflow = null;
        }

        private IWorkflowLike _activeWorkflow;
    }

    public abstract class Workflow : Workflow<bool>
    {
    }
}