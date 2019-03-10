using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChange : TemporalController
{
    public Material material;

    public override void Determine(DeterministicObject obj, float time)
    {

    }

    public override void Initialize(DeterministicObject obj)
    {
        UniversalCube cube = (UniversalCube)obj;
        cube.rendy.material = material;
    }
}
