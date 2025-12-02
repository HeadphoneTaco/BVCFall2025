using UnityEngine;

/// <summary>
/// This script forms the basis of a weapon system that uses Raycasting to detect collisions in a 2D environment
/// </summary>
public class WeaponRaycast : MonoBehaviour
{
    // The maximum distance the Raycast will reach.
    [SerializeField] private float _weaponRange = 15f;
    // The LayerMask specifies which layers the Raycast should collide with.
    [SerializeField] private LayerMask _enemyLayers;
    
    private void Update()
    {
        // Detects the left mouse button click
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
        // Set the fire direction as the object's right
        Vector2 fireDirection = transform.right; 
        // Visualise aim: Draws the Raycast direction and range in the Scene view as a yellow line for developer debugging.
        Debug.DrawRay(transform.position, fireDirection * _weaponRange, Color.yellow);
    }
    
    private void Shoot()
    {
        // Set the firing direction vector.
        Vector2 fireDirection = transform.right;
        
        // Execute the 2D Raycast: 
        // 1. transform.position: Starts at the current object's position.
        // 2. fireDirection: Fires in the set direction.
        // 3. _weaponRange: Travels up to the max range.
        // 4. _enemyLayers: Only detects objects on the specified layers.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, fireDirection, _weaponRange, _enemyLayers);

        // Checks if the Raycast successfully hit something
        if (hit.collider != null)
        {
            // Log the name and distance of the collided object to the console.
            Debug.Log($"Hit {hit.collider.name} at distance {hit.distance}");
        }
    }
}
