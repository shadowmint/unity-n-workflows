using System;
using System.Collections;
using System.Threading.Tasks;
using N.Package.Promises;

namespace N.Package.Workflows
{
    /// <summary>
    /// A workflow with a built-in IEnumerator for unity async operations.
    /// </summary>
    public abstract class UnityWorkflow : Workflow
    {
        private TaskCompletionSource<bool> _taskCompletionSource;

        private bool _resolved;

        protected sealed override Task<bool> Execute()
        {
            _taskCompletionSource = new TaskCompletionSource<bool>();
            AsyncWorker.Run(ExecuteInternal);
            return _taskCompletionSource.Task;
        }

        private IEnumerator ExecuteInternal()
        {
            var enumerator = ExecuteAsync();
            while (true)
            {
                if (_resolved)
                {
                    break;
                }

                object item = null;

                try
                {
                    if (enumerator.MoveNext())
                    {
                        item = enumerator.Current;
                    }
                    else
                    {
                        break;
                    }
                }
                catch (Exception error)
                {
                    _taskCompletionSource.SetException(error);
                    break;
                }

                yield return item;
            }
        }

        protected abstract IEnumerator ExecuteAsync();

        protected void Resolve()
        {
            _resolved = true;
            _taskCompletionSource.SetResult(true);
        }

        protected void Reject(Exception error)
        {
            _resolved = true;
            _taskCompletionSource.SetException(error);
        }
    }
}