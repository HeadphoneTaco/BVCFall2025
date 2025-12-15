using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactor;
 
    public void Move(float delta)

    {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta * parallaxFactor;
 
        transform.localPosition = newPos;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
