using UnityEngine;

class Paraboloid : Function
{
    private float _t;
    private float _u;

    public Paraboloid(float t, float u)
    {
        this._t = t;
        this._u = u;
    }
    public override float GetFunctionValue(float x, float y)
    {
        return _t * Mathf.Pow(x, 2) + _u * Mathf.Pow(y, 2);
    }
}
