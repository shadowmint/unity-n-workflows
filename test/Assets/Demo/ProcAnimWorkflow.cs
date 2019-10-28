using System.Collections;
using N.Package.Workflows;
using UnityEngine;
using UnityEngine.Assertions;

namespace Demo
{
    public class ProcAnimWorkflow : UnityWorkflow
    {
        public override string Id => "AnimateToPoint";
        
        public GameObject Destination { get; set; }
        
        public GameObject Affected { get; set; }

        public float Speed { get; set; }

        protected override void Validate()
        {
            Assert.IsTrue(Destination != null);
            Assert.IsTrue(Affected != null);
        }

        protected override IEnumerator ExecuteAsync()
        {
            do
            {
                var delta = Vector3.Distance(Affected.transform.position, Destination.transform.position);
                if (delta <= 0.1f)
                {
                    Affected.transform.position = Destination.transform.position;
                    break;
                }

                var position = Affected.transform.position;
                var step = (Time.deltaTime) * Speed * (Destination.transform.position - position).normalized;
                position += step;
                Affected.transform.position = position;
                yield return new WaitForEndOfFrame();
            } while (true);
            Resolve();
        }
    }
}