using System.Collections.Generic;
using N.Package.Promises;
using UnityEngine;

namespace Demo
{
    public class DemoComponent : MonoBehaviour
    {
        public float speed;

        public List<GameObject> targets;

        void Start()
        {
            new TestWorkflow()
            {
                Affected = gameObject,
                Speed = speed,
                Targets = targets
            }.Run().Dispatch();
        }
    }
}