using UnityEngine;

public class DotProductVisualiser : MonoBehaviour
{
    [SerializeField]
    private float _visionAngle = 45f;
    private Transform _player;

    private void Start()
    {
        // Find Player (tag)
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            _player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player object not found! Make sure the Player has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (_player == null)
        {
            return;
        }

        // 2D forward = transform.right (x-axis direction)
        // Vector2 forward = transform.right;
        
        // The character is looking to the left
        Vector2 forward = -transform.right;
        Vector2 toTarget = (_player.position - transform.position).normalized;

        float dot = Vector2.Dot(forward, toTarget);

        // Change color based on dot
        Color lineColor;
        if (dot > 0.7f)
        {
            lineColor = Color.green; // in front
        }
        else if (dot > -0.7f)
        {
            lineColor = Color.yellow; // side
        }
        else
        {
            lineColor = Color.red; // behind
        }

        // Draw 2D vision cone
        DrawVisionCone2D(_visionAngle, lineColor);

        Debug.Log($"Dot Product: {dot:F2}");
    }

    private void DrawVisionCone2D(float angle, Color color)
    {
        // Vector2 forward = transform.right;
        // The character is looking to the left
        Vector2 forward = -transform.right;

        // Rotate around Z axis for 2D
        Vector2 leftBoundary = RotateVector(forward, -angle);
        Vector2 rightBoundary = RotateVector(forward, angle);

        Debug.DrawRay(transform.position, new Vector3(leftBoundary.x, leftBoundary.y) * 5f, color);
        Debug.DrawRay(transform.position, new Vector3(rightBoundary.x, rightBoundary.y) * 5f, color);
    }

    private Vector2 RotateVector(Vector2 v, float degrees)
    {
        return Quaternion.Euler(0, 0, degrees) * v;
    }
}