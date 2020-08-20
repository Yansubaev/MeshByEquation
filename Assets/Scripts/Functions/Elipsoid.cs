using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elipsoid : Function
{
    public override float GetFunctionValue(float x, float y)
    {
        return Mathf.Sqrt(1 - x*x - y*y);
    }
}
