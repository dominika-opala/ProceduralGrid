using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MyGrid : MonoBehaviour {

    public int Column, Row;

    private Vector3[] vertices;
    private Mesh mesh;

    private void Awake () {
        Generate();
    }


    private void Generate () {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid2";

        vertices = new Vector3[(Column + 1) * (Row + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        for (int i = 0, y = 0; y <= Row; y++) {
            for (int x = 0; x <= Column; x++, i++) {
                vertices[i] = new Vector3(x, y);
                uv[i] = new Vector2((float)x / Column, (float)y / Row);
                //tangents[i] = tangent;
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        //mesh.tangents = tangents;

        int[] triangles = new int[Column * Row * 6];
        for (int ti = 0, vi = 0, y = 0; y < Row; y++, vi++) {
            for (int x = 0; x < Column; x++, ti += 6, vi++) {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + Column + 1;
                triangles[ti + 5] = vi + Column + 2;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
    }
}