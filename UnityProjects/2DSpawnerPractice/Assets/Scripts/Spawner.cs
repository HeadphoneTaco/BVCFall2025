using UnityEngine;
public class Spawner : MonoBehaviour
{
    private float _nextSpawnTime;
    public int count;
    public float updateFrequency;
    public GameObject square;
    public GameObject circle;
    public GameObject triangle;
    private void FixedUpdate()//Only spawns when space is held down
    {
        if (!Input.GetKey(KeyCode.Space) || !(Time.time > _nextSpawnTime)) return;
        count++;
        CheckCount();
        _nextSpawnTime = Time.time + updateFrequency;
    }
    
    private void CheckCount()//Spawns different shapes based on the count
    {
        switch (count / 10)
        {
            case -1: 
                Debug.Log("Bro how the hell did you break this even more?"); 
                break;
            
            case 0:
                Instantiate(square);
                break;

            case 1:
                Instantiate(circle);
                break;

            case 2:
                Instantiate(triangle);
                break;
            
            case 3:
                count = 0;
                break;

            default:
                Debug.Log("How the hell did you get here?");
                break;
        }
    }
}