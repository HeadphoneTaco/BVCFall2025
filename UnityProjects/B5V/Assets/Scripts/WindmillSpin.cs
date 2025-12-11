using UnityEngine;

public class WindmillSpin : MonoBehaviour
{
    public float rotationSpeed = 50f; // Degrees per second

    void Update()
    {
        // Rotate around the Y-axis (up)
        transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
        
    }
}