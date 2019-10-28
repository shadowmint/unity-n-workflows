using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo;
using N.Package.Workflows;
using UnityEngine;

public class TestWorkflow : Workflow
{
    public override string Id => "Animation test";

    public GameObject Affected { get; set; }

    public float Speed { get; set; }

    public List<GameObject> Targets { get; set; }

    protected override async Task<bool> Execute()
    {
        foreach (var target in Targets)
        {
            await new ProcAnimWorkflow()
            {
                Affected = Affected,
                Speed = Speed,
                Destination = target
            }.Run(this);
        }

        return true;
    }

    protected override void Validate()
    {
    }
}