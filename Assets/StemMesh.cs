using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class StemMeshGrowth : MonoBehaviour
{
    public float growthSpeed = 0.5f; // Speed of growth
    public float growthAngle = 45f; // Angle in degrees for the stem to grow
    public float growthLength = 5f; // Length for the stem to grow
    public float startWidth = 0.2f; // Width at the base of the stem

    private List<Vector3> spinePoints = new List<Vector3>();
    private Vector3 startPoint;
    private Vector3 secondPoint;
    private Vector3 controlPoint;
    private Vector3 endPoint;

    private MeshFilter meshFilter;
    private Mesh stemMesh;

    void Start()
    {
        // Get MeshFilter and initialize the Mesh
        meshFilter = GetComponent<MeshFilter>();
        stemMesh = new Mesh();
        meshFilter.mesh = stemMesh;

        // Initialize the spine with two vertical points, using the object's local position as the start point
        startPoint = Vector3.zero; // Local position of the start point
        secondPoint = startPoint + new Vector3(0, 1, 0); // The second point is directly above the start point to create an initial vertical line

        // Add the initial two points to the spine
        spinePoints.Add(startPoint);
        spinePoints.Add(secondPoint);

        // Convert angle to radians and calculate direction
        float angleInRadians = growthAngle * Mathf.Deg2Rad;
        Vector3 direction = new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0).normalized;

        // Define the control point and initial end point for the curve
        endPoint = secondPoint + direction * growthLength; // Extend the endpoint based on length and direction

        // Control point for curvature after vertical growth
        controlPoint = secondPoint + (direction * growthLength * 0.5f) + new Vector3(0, growthLength * 0.25f, 0);

        // Start growing the stem
        StartCoroutine(GrowStem());
    }

    IEnumerator GrowStem()
    {
        float t = 0f; // Parameter for the Bezier curve (ranges from 0 to 1)

        while (t <= 1f)
        {
            // Calculate the next point on the quadratic Bezier curve in local space
            Vector3 nextPoint = Mathf.Pow(1 - t, 2) * secondPoint +
                                2 * (1 - t) * t * controlPoint +
                                Mathf.Pow(t, 2) * endPoint;

            // Add the new point to the spine
            spinePoints.Add(nextPoint);

            // Update the mesh with the new spine points
            UpdateMesh();

            // Increment t based on growth speed
            t += Time.deltaTime * growthSpeed;

            yield return null;
        }

        // Add Rigidbody and MeshCollider after growth is complete
        AddPhysics();
    }

    void AddPhysics()
    {
        // Add a Rigidbody2D component to the GameObject
        Rigidbody2D rb2D = gameObject.AddComponent<Rigidbody2D>();
        rb2D.isKinematic = true; // Make the Rigidbody2D kinematic

        // Add an EdgeCollider2D to follow the shape of the stem
        EdgeCollider2D edgeCollider = gameObject.AddComponent<EdgeCollider2D>();

        // Convert the spine points to a 2D vector array
        Vector2[] edgePoints = new Vector2[spinePoints.Count];
        for (int i = 0; i < spinePoints.Count; i++)
        {
            edgePoints[i] = new Vector2(spinePoints[i].x, spinePoints[i].y);
        }

        // Assign the points to the EdgeCollider2D
        edgeCollider.points = edgePoints;
    }


    void UpdateMesh()
    {
        // Generate the vertices for the mesh based on the spine points
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < spinePoints.Count - 1; i++)
        {
            Vector3 currentPoint = spinePoints[i];
            Vector3 nextPoint = spinePoints[i + 1];

            // Calculate the width at this segment
            float t = (float)i / (spinePoints.Count - 1);
            float width = Mathf.Lerp(startWidth, 0, t);

            // Calculate direction for generating the width offsets
            Vector3 direction = (nextPoint - currentPoint).normalized;
            Vector3 perpendicular = Vector3.Cross(direction, Vector3.forward).normalized;

            // Create vertices for this segment in local space
            Vector3 left = currentPoint - perpendicular * width * 0.5f;
            Vector3 right = currentPoint + perpendicular * width * 0.5f;
            Vector3 nextLeft = nextPoint - perpendicular * width * 0.5f;
            Vector3 nextRight = nextPoint + perpendicular * width * 0.5f;

            vertices.Add(left);
            vertices.Add(right);
            vertices.Add(nextLeft);
            vertices.Add(nextRight);

            // Add triangles for the quad formed by the current and next points
            int baseIndex = i * 4;
            triangles.Add(baseIndex);
            triangles.Add(baseIndex + 2);
            triangles.Add(baseIndex + 1);
            triangles.Add(baseIndex + 1);
            triangles.Add(baseIndex + 2);
            triangles.Add(baseIndex + 3);

            // Add UV coordinates for this segment
            uvs.Add(new Vector2(0, t));
            uvs.Add(new Vector2(1, t));
            uvs.Add(new Vector2(0, t + (1f / (spinePoints.Count - 1))));
            uvs.Add(new Vector2(1, t + (1f / (spinePoints.Count - 1))));
        }

        // Assign generated vertices, triangles, and UVs to the mesh
        stemMesh.Clear();
        stemMesh.vertices = vertices.ToArray();
        stemMesh.triangles = triangles.ToArray();
        stemMesh.uv = uvs.ToArray();
        stemMesh.RecalculateNormals();
    }
}

