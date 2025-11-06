using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


namespace Player
{


    public class PlayerGroundController : MonoBehaviour
    {
        [SerializeField] public float speed;
        public TextMeshProUGUI countText;

        private Rigidbody _rb;
        private int _count;

        private float _movementX;
        private float _movementY;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Vector3 movement = new Vector3(_movementX, 0.0f, _movementY);
            _rb.AddForce(movement * speed);
        }

        void OnMove(InputValue movementValue)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();
            _movementX = movementVector.x;
            _movementY = movementVector.y;
        }
        

    }
}