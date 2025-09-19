using UnityEngine;
public class Spawner : MonoBehaviour
{
    [SerializeField] int spacebarPressCount;
    public GameObject square;
    public GameObject circle;
    public GameObject triangle;
    void FixedUpdate()
    {
       PressSpace();
       CheckSpacePress();
    }

    private void PressSpace()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                spacebarPressCount++;
            }
        }
    }
    private void CheckSpacePress()
    {
        if (spacebarPressCount <= 10 && spacebarPressCount >= 0)
        {
            Instantiate(square);
        }
        else if (spacebarPressCount <= 20 && spacebarPressCount >= 10)
        {
            Instantiate(circle);
        }
        else if (spacebarPressCount> 30 && spacebarPressCount >= 20)
        {
            Instantiate(triangle);
        }
        else
        {
            spacebarPressCount = 0;
        }
    }
    
}
