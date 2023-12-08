using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    // Tudo isso foi inteiramente ROUBADO daqui:
    // https://www.youtube.com/watch?v=CSeUMTaNFYk
    [SerializeField] int rayCount = 2;
    [SerializeField] float fov = 90f;
    [SerializeField] float viewDistance = 50f;
    void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        float angle = 0f;
        
        Vector3 origin = Vector3.zero;

        float angleIncrease = fov / rayCount;
        


        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for(int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            vertices[vertexIndex] = vertex;
            if(i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }
            vertexIndex += 1;

            angle -= angleIncrease;

        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

    }
    public static Vector3 GetVectorFromAngle(float angle)
    {
        //angle = 0 -> 360
        float angleRad = angle * (Mathf.PI/180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}
