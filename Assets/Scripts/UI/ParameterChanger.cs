using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Parameter
{
    T,
    U
}
public class ParameterChanger : MonoBehaviour
{
    [SerializeField]
    private MatrixTriangulator _triangulator;
    [SerializeField]
    private Parameter _parameter;

    private float _value;

    private void Awake()
    {
        switch (_parameter)
        {
            case Parameter.T:
                _value = _triangulator.T;
                break;
            case Parameter.U:
                _value = _triangulator.U;
                break;
        }
    }

    public void OnValueChanged(string value)
    {
        try
        {
            float val;
            float.TryParse(value, out val);
            _value = val;
            switch (_parameter)
            {
                case Parameter.T:
                    _triangulator.T = _value;
                    break;
                case Parameter.U:
                    _triangulator.U = _value;
                    break;
            }
            _triangulator.UpdateMesh();
        }
        catch (Exception)
        {
            Debug.Log("Incorrect value");
        }
    }
}
