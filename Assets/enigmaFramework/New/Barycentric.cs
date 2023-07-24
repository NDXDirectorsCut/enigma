using UnityEngine;

public class Barycentric : MonoBehaviour
{
    public Transform capsule;
    public bool interpolateNormals;
    public float lerp;
    // Attach this script to a camera and it will
    // draw a debug line pointing outward from the normal
    void LateUpdate()
    {
        // Only if we hit something, do we continue
        RaycastHit hit;


        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            return;
        }

        // Just in case, also make sure the collider also has a renderer
        // material and texture
        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (meshCollider == null || meshCollider.sharedMesh == null)
        {
            return;
        }

        Mesh mesh = meshCollider.sharedMesh;
        Vector3[] normals = mesh.normals;
        int[] triangles = mesh.triangles;

        // Extract local space normals of the triangle we hit
        Vector3 n0 = normals[triangles[hit.triangleIndex * 3 + 0]];
        Vector3 n1 = normals[triangles[hit.triangleIndex * 3 + 1]];
        Vector3 n2 = normals[triangles[hit.triangleIndex * 3 + 2]];

        // interpolate using the barycentric coordinate of the hitpoint
        Vector3 baryCenter = hit.barycentricCoordinate;

        // Use barycentric coordinate to interpolate normal
        Vector3 interpolatedNormal = n0 * baryCenter.x + n1 * baryCenter.y + n2 * baryCenter.z;
        // normalize the interpolated normal
        interpolatedNormal = interpolatedNormal.normalized;

        // Transform local space normals to world space
        Transform hitTransform = hit.collider.transform;
        Debug.DrawRay(hit.point,interpolatedNormal,Color.green);

        interpolatedNormal = hitTransform.TransformDirection(interpolatedNormal);

        // Display with Debug.DrawLine
        Debug.DrawRay(hit.point, interpolatedNormal,Color.red);
        Debug.DrawRay(hit.point, hit.normal,Color.blue);

        capsule.position = Vector3.Lerp(capsule.position,hit.point,1);
        capsule.up = interpolateNormals ? Vector3.Slerp(capsule.up,interpolatedNormal,lerp) : Vector3.Slerp(capsule.up,hit.normal,lerp);

    }
}
