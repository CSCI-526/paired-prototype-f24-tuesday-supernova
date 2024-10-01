using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StemGrowth : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float growthSpeed = 0.5f; // Speed of growth
    public float growthAngle = 45f; // Angle in degrees for the stem to grow
    public float growthLength = 5f; // Length for the stem to grow
    public float startWidth = 0.2f; // Width at the base of the stem
    public float widthGrowthFactor = 0.3f; // Initial factor for width growth

    private List<Vector3> spinePoints = new List<Vector3>();
    private Vector3 startPoint;
    private Vector3 secondPoint;
    private Vector3 controlPoint;
    private Vector3 endPoint;
    private EdgeCollider2D edgeCollider;
    private Coroutine growthCoroutine;

    void Start()
    {
        InitializeStem();
        // Start growing the stem
        //growthCoroutine = StartCoroutine(GrowStem());
    }

    void InitializeStem()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();

            // If LineRenderer is still null, add one dynamically
            if (lineRenderer == null)
            {
                lineRenderer = gameObject.AddComponent<LineRenderer>();
            }
        }

        lineRenderer.useWorldSpace = true;

        // Clear previous spine points
        spinePoints.Clear();

        // Initialize the spine with two vertical points, using the object's position as the start point
        startPoint = transform.position;
        Vector3 start2Point = startPoint + new Vector3(0, 0.5f, 0);
        secondPoint = startPoint + new Vector3(0, 1, 0); // The second point is directly above the start point to create an initial vertical line

        // Add the initial two points to the spine
        spinePoints.Add(startPoint);
        spinePoints.Add(start2Point);
        spinePoints.Add(secondPoint);

        // Set up the LineRenderer with the initial points
        lineRenderer.positionCount = spinePoints.Count;
        lineRenderer.SetPositions(spinePoints.ToArray());

<<<<<<< HEAD
        // Update the width growth factor (from 0.3 to 1)
        float currentWidthFactor = Mathf.Lerp(0.3f, 1f, 0);

        // Set the width curve to transition from full width to zero, scaled by currentWidthFactor
        AnimationCurve widthCurve = new AnimationCurve();
        widthCurve.AddKey(0f, startWidth * currentWidthFactor);  // Full width at the bottom (0% along the length)
        widthCurve.AddKey(1f, 0f);                               // Zero width at the top (100% along the length)
        lineRenderer.widthCurve = widthCurve;

=======
>>>>>>> f386d93dd70ec1224a115aa2b15e2f128f0cf917
        // Convert angle to radians and calculate direction
        float angleInRadians = growthAngle * Mathf.Deg2Rad;
        Vector3 direction = new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0).normalized;

        // Define the control point and initial end point for the curve
        endPoint = secondPoint + direction * growthLength; // Extend the endpoint based on length and direction

        // Control point for curvature after vertical growth
        controlPoint = secondPoint + (direction * growthLength * 0.5f) + new Vector3(0, growthLength * 0.25f, 0);

        // Set the starting width of the line
        lineRenderer.widthMultiplier = 1f;

        // Add EdgeCollider2D component if not already present
        if (edgeCollider == null)
        {
            edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
        }

        // Update the EdgeCollider2D with the initial spine points
        UpdateEdgeCollider();
    }

    IEnumerator GrowStem()
    {
        float t = 0f; // Parameter for the Bezier curve (ranges from 0 to 1)

        while (t <= 1f)
        {
            // Calculate the next point on the quadratic Bezier curve
            Vector3 nextPoint = Mathf.Pow(1 - t, 2) * secondPoint +
                                2 * (1 - t) * t * controlPoint +
                                Mathf.Pow(t, 2) * endPoint;

            // Add the new point to the spine
            spinePoints.Add(nextPoint);

            // Update the LineRenderer with the new spine points
            lineRenderer.positionCount = spinePoints.Count;
            lineRenderer.SetPositions(spinePoints.ToArray());

            // Update the width growth factor (from 0.3 to 1)
            float currentWidthFactor = Mathf.Lerp(0.3f, 1f, t);

            // Set the width curve to transition from full width to zero, scaled by currentWidthFactor
            AnimationCurve widthCurve = new AnimationCurve();
            widthCurve.AddKey(0f, startWidth * currentWidthFactor);  // Full width at the bottom (0% along the length)
            widthCurve.AddKey(1f, 0f);                               // Zero width at the top (100% along the length)
            lineRenderer.widthCurve = widthCurve;

            // Update the EdgeCollider2D with the new spine points
            UpdateEdgeCollider();

            // Increment t based on growth speed
            t += Time.deltaTime * growthSpeed;

            yield return null;
        }
    }

    void UpdateEdgeCollider()
    {
        if (edgeCollider != null && spinePoints.Count > 1)
        {
            // Convert spine points to 2D points for EdgeCollider2D in local space
            Vector2[] colliderPoints = new Vector2[spinePoints.Count];
            for (int i = 0; i < spinePoints.Count; i++)
            {
                Vector3 localPoint = transform.InverseTransformPoint(spinePoints[i]); // Convert world space to local space
                colliderPoints[i] = new Vector2(localPoint.x, localPoint.y);
            }

            // Update the EdgeCollider2D points
            edgeCollider.points = colliderPoints;
        }
    }

    public void ResetStem()
    {
        // Stop the current growth coroutine if it's running
        if (growthCoroutine != null)
        {
            StopCoroutine(growthCoroutine);
        }

        // Re-initialize the stem to the initial state
        InitializeStem();
    }

    public void ExtendStem() {
        // Start growing the stem again
        growthCoroutine = StartCoroutine(GrowStem());
    }

<<<<<<< HEAD

    public void PositionDidChange() {
        InitializeStem();
    }


=======
>>>>>>> f386d93dd70ec1224a115aa2b15e2f128f0cf917
}


