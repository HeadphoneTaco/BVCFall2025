using UnityEngine;

public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(float deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;
 
    private float oldPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    oldPosition = transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {
    if (transform.position.x != oldPosition)
    {
        if (onCameraTranslate != null)
        {
            float delta = oldPosition - transform.position.x;
            onCameraTranslate(delta);
        }
 
        oldPosition = transform.position.x;
        
    }
    
    }
}
