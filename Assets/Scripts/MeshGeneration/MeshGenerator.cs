using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MeshGenerator : MonoBehaviour
{
    [SerializeField]
    private int _startX = -20;
    [SerializeField]
    private int _endX = 20;
    [SerializeField]
    private int _startZ = -20;
    [SerializeField]
    private int _endZ = 20;
    [SerializeField]
    private Material _material;
    [SerializeField]
    private float _t;
    [SerializeField]
    private float _u;


    private void Start()
    {
        Generate();
    }

    private void OnValidate()
    {
        Generate();
    }

    private void Generate()
    {
        Mesh mesh;
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        var xSize = _endX - _startX;
        var ySize = _endZ - _startZ;

        var calc = new Calculator(_startX, _endX, _startZ, _endZ);
        var vertices = calc.CalculateValues(new Paraboloid(_t, _u));
        mesh.vertices = vertices;
        int[] triangles = new int[xSize * ySize * 6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }
        mesh.SetTriangles(triangles, 0, true);
        mesh.RecalculateNormals();
        GetComponent<MeshRenderer>().material = _material;
    }
}
