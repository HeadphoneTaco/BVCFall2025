using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public InputActionReference moveAction;

    private Vector2 _moveDirection;
    private Rigidbody _rigidbody;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get rigidbody component if not assigned
        if (_rigidbody == null)
        {
            _rigidbody = gameObject.GetComponent<Rigidbody>();
        }
        //Freeze rotation on y-axis
        _rigidbody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        //get the direction from the input action
        _moveDirection = moveAction.action.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        // Calculate movement in the x-z plane only (wasd) 
        Vector3 movement = new Vector3(_moveDirection.x, 0, _moveDirection.y).normalized * moveSpeed;
        //Apply movement to the current position, keeping y-axis dependent on gravity
        _rigidbody.MovePosition(transform.position + movement * Time.fixedDeltaTime);
    }
}
