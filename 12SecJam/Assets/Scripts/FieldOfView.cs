using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    // Tudo isso foi inteiramente ROUBADO daqui:
    // https://www.youtube.com/watch?v=CSeUMTaNFYk
    Mesh mesh;
    Vector3 origin;
    float startingAngle = 180;

    [SerializeField] int rayCount = 2;
    [SerializeField] float fov = 90f;
    [SerializeField] float viewDistance = 50f;

    [SerializeField] LayerMask layerMask;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }
    private void LateUpdate()
    {
     
        float angle = startingAngle;

        //origin = Vector3.zero;

        float angleIncrease = fov / rayCount;
        
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];
        
        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for(int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            if(raycastHit2D.collider == null)
            {
                
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = raycastHit2D.point;
            }
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

        //SetAimDirection(aimDir);
        //

    }
    public static Vector3 GetVectorFromAngle(float angle)
    {
        //angle = 0 -> 360
        float angleRad = angle * (Mathf.PI/180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        // Ensure aimDirection is normalized
        aimDirection = aimDirection.normalized;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // Adjust the angle to match your desired orientation
        startingAngle = angle - fov / 2f + 90f;
    }
}
