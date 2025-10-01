using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 100f;
    private Camera _camera;
    
    private void Start()
    {
        _camera = Camera.main;
        Cursor.visible = false;
    }

    void Update()
    {
        // Movement Input
        var horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        var verticalInput = Input.GetAxis("Vertical");   // W/S or Up/Down Arrow
        var upInput = 0f;

        if (Input.GetKey(KeyCode.Space))
        {
            upInput = 1f; // Move up
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            upInput = -1f; // Move down
        }

        // Calculate Movement Direction
        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput + Vector3.up * upInput;

        // Apply Movement
        transform.position += moveDirection * (moveSpeed * Time.deltaTime);

        // Rotation Input (Mouse Look)
        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");

        // Apply Rotation
        transform.Rotate(Vector3.up * (mouseX * rotationSpeed * Time.deltaTime));

        if (_camera) _camera.transform.Rotate(Vector3.left * (mouseY * rotationSpeed * Time.deltaTime));
    }
}