using UnityEngine;

public class SpwanPlatform : MonoBehaviour
{
    public GameObject platformPrefab;

     int platformCount = 10;

    void Start()
    {
        Vector3 spawnPosition = new Vector3();
        float minYSpacing = 2.5f; 
        float maxYSpacing = 3.5f; 
        float minX = 3.1f;        
        float maxX = 14.1f;

        for (int i = 0; i < platformCount; i++)
        {
            spawnPosition.y += Random.Range(minYSpacing, maxYSpacing);
            spawnPosition.x = Random.Range(minX, maxX);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }


    }

}
