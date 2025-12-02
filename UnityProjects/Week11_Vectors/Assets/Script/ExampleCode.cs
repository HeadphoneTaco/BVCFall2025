using UnityEngine;

public class ExampleCode : MonoBehaviour
{
    private GameObject _playerGameObject;
    private float _speed = 0.5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Vector2
        var vectorUp = Vector3.up;          // (0, 1)
        var vectorDown = Vector2.down;      // (0, -1)
        var vectorRight = Vector2.right;    // (1, 0)
        var vectorLeft = Vector2.left;      // (-1, 0)
        
        Debug.Log(vectorUp);
        Debug.Log(vectorDown);
        Debug.Log(vectorRight);
        Debug.Log(vectorLeft);
        
        // Vector3
        var vectorForward = Vector3.forward;    // (0, 0, 1)
        var vectorBack = Vector3.back;          // (0, 0, -1)
        
        _playerGameObject.transform.position += vectorForward  * _speed * Time.deltaTime;
        
        /*  Calculates distance between two points
            Use this when you only need to know the distance (e.g., proximity check) */
        float distance = Vector3.Distance(_playerGameObject.transform.position, transform.position);
        
        /*  When both direction and distance are needed */
        Vector3 dir = _playerGameObject.transform.position - transform.position;
        float magnitude = dir.magnitude;
        
        // Normalize the direction
        Vector3 normalizedDir = dir.normalized;

        // Move the character
        transform.position += normalizedDir * _speed * Time.deltaTime;
        
        const float visibilityRange = 50f;
        var distanceFromPlayer = Vector3.Distance(_playerGameObject.transform.position, transform.position);
        if (distanceFromPlayer <= visibilityRange)
        {
            // Yes, I see you
        }
    }
}
