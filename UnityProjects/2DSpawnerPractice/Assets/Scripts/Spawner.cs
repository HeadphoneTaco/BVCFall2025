using UnityEngine;
public class Spawner : MonoBehaviour
{
    [SerializeField] int numberOfTicks;
    [SerializeField] int numberOfObjects;
    [SerializeField] int maxNumberOfObjects;
    [SerializeField] int updateFrequency; // How often the logic should run (in seconds)
    public GameObject square;
    public GameObject circle;
    public GameObject triangle;
    private float _timer;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= updateFrequency)
        {
            // Place your logic here that needs to run at a slower rate
            numberOfTicks++;
            CheckNumberOfObjects();
            _timer = 0f; // Reset the timer
        }
    }

    void CheckNumberOfObjects()
    {
        while (numberOfObjects <= maxNumberOfObjects)
        {
         SpawnThings();
        }       
    }
    
    void SpawnThings()
    {
        switch (numberOfTicks)
        {
            case > 0 and < 10:
                Instantiate(square);
                break;
            case > 10 and < 20:
                Instantiate(circle);
                break;
            case > 20 and < 30:
                Instantiate(triangle);
                break;
            case > 30 :
                numberOfTicks = 0;
                break;
        }
    }
}