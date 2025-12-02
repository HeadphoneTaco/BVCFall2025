using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    public float MoveSpeed = 1f;
    public bool Normalized;

    private Transform _player;

    private void Start()
    {
        // Find Player (Objects with the Player tag)
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

        // 1. Direction Vector Calculation (Player - Enemy)
        Vector3 direction = _player.position - transform.position;

        if (Normalized == false)
        {
            /******************************
             *        No Normalize
             ******************************/
            
            // The faster you go, the greater the distance!
            transform.position += direction * MoveSpeed * Time.deltaTime;
        }
        else
        {
            /******************************
             *         Normalize
             ******************************/

            // 2. Direction Vector Normalization (Set length to 1!)
            Vector3 normalizedDir = direction.normalized;

            // 3. Moving at a constant speed
            transform.position += normalizedDir * MoveSpeed * Time.deltaTime;
        }
    }
}
