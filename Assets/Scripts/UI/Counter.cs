using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CounterField { 
    XStart,
    XEnd,
    ZStart,
    ZEnd
}

[ExecuteInEditMode]
public class Counter : MonoBehaviour
{
    [SerializeField]
    private Text _counterText;
    [SerializeField]
    private MatrixTriangulator _triangulator;
    [SerializeField]
    private CounterField _countingField;

    public int value;

    private void Awake()
    {
        _counterText.text = value.ToString();
    }

    private void OnValidate()
    {
        _counterText.text = value.ToString();
    }
    public void UpClick()
    {
        value++;
        SwitchValue();
    }

    public void DownClick()
    {
        value--;
        SwitchValue();
    }

    private void SwitchValue()
    {
        _counterText.text = value.ToString();

        switch (_countingField)
        {
            case CounterField.XStart:
                _triangulator.StartX = value;
                break;
            case CounterField.XEnd:
                _triangulator.EndX = value;
                break;
            case CounterField.ZStart:
                _triangulator.StartZ = value;
                break;
            case CounterField.ZEnd:
                _triangulator.EndZ = value;
                break;
        }
        _triangulator.UpdateMesh();
    }
}
