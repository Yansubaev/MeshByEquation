using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator
{
    private int _startX;
    private int _endX;
    private int _startY;
    private int _endY;

    private List<int> indices = new List<int>();

    public Calculator(int startX, int endX, int startY, int endY)
    {
        this._startX = startX;
        this._endX = endX;
        this._startY = startY;
        this._endY = endY;
    }

    public Vector3[] CalculateValues(Function func)
    {
        var xSize = _endX - _startX;
        var ySize = _endY - _startY;

        var values = new Vector3[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                float z = func.GetFunctionValue(x + _startX, y + _startY);
                if (float.IsNaN(z))
                {
                    continue;
                }
                values[i] = new Vector3(x, y, z);
                indices.Add(i);
            }
        }
        return values;
    }
    public int[] GetIndecies()
    {
        return indices.ToArray();
    }
}
