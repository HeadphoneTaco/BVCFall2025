using UnityEngine;
public class Spawner : MonoBehaviour
{
    private float _nextSpawnTime;
    public int count;
    public float updateFrequency;
    public GameObject square;
    public GameObject circle;
    public GameObject triangle;
    private void FixedUpdate()
    {
        if (!Input.GetKey(KeyCode.Space) || !(Time.time > _nextSpawnTime)) return;
        count++;
        CheckCount();
        _nextSpawnTime = Time.time + updateFrequency;
    }
    
    private void CheckCount()
    {
        switch (count / 10)
        {
            case 0:
                Instantiate(square);
                break;

            case 1:
                Instantiate(circle);
                break;

            case 2:
                Instantiate(triangle);
                break;

            default:
                count = 0;
                break;
        }
    }
}