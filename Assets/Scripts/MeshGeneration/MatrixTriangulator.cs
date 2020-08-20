using UnityEngine;

[ExecuteInEditMode]
public class MatrixTriangulator : MonoBehaviour
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
    private float _t;
    [SerializeField]
    private float _u;


    public GameObject marker;

    public int StartX { get => _startX; set => _startX = value; }
    public int EndX { get => _endX; set => _endX = value; }
    public int StartZ { get => _startZ; set => _startZ = value; }
    public int EndZ { get => _endZ; set => _endZ = value; }
    public float T { get => _t; set => _t = value; }
    public float U { get => _u; set => _u = value; }

    private void Start()
    {
        UpdateMesh();
    }

    private void OnValidate()
    {
        UpdateMesh();
    }

    public void UpdateMesh()
    {
        Generate(new Paraboloid(_t, _u));
    }

    private void Generate(Function func)
    {
        Mesh mesh;
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        var xSize = Mathf.Abs(_endX - _startX) + 1;
        var zSize = Mathf.Abs(_endZ - _startZ) + 1;

        // Инициализируется двумерный массив вершин
        Vector3[,] vertexMatrix = new Vector3[xSize, zSize];

        var i = 0;
        var j = 0;

        // Определение множества вершин
        for (int x = _startX; x <= _endX; x++)
        {
            j = 0;
            for (int z = _startZ; z <= _endZ; z++)
            {
                // Координата y вычисляет через указанную функцию
                float y = func.GetFunctionValue(x, z);
                // Веришина сохраняется в двумерный массив
                vertexMatrix[i, j] = new Vector3(x, y, z);
                j++;
            }
            i++;
        }

        // Вычисление размера массива треугольников
        var combineSize = (xSize - 1) * 2 * (zSize - 1);
        // Инициализация массива, хранящего меши треугольников
        var combine = new CombineInstance[combineSize];

        // Поиск треугольников на заданном множестве вершин
        var count = 0;
        for (int ii = 0; ii < xSize - 1; ii++)
        {
            for (int jj = 0; jj < zSize - 1; jj++)
            {
                var vert1 = vertexMatrix[ii, jj];
                var vert2 = vertexMatrix[ii, jj + 1];
                var vert3 = vertexMatrix[ii + 1, jj];
                combine[count].mesh = GetTriangleMesh(vert1, vert2, vert3);

                vert1 = vertexMatrix[ii + 1, jj];
                vert2 = vertexMatrix[ii, jj + 1];
                vert3 = vertexMatrix[ii + 1, jj + 1];
                combine[count + 1].mesh = GetTriangleMesh(vert1, vert2, vert3);
                count += 2;
            }
        }

        // Объединение найденных треугольников в один меш
        mesh.CombineMeshes(combine, true, false);
        mesh.RecalculateNormals();
    }

    public Mesh GetTriangleMesh(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3)
    {
        var mesh = new Mesh();
        mesh.vertices = new[] { vertex1, vertex2, vertex3 };
        mesh.triangles = new[] { 0, 1, 2 };
        return mesh;
    }

}
