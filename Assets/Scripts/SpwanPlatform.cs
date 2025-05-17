using UnityEngine;

public class SpwanPlatform : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject platformPrefab;

    public int platformCount = 20;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPosition = new Vector3();

        for (int i = 0; i < platformCount; i++)
        {
            spawnPosition.y += Random.Range(.5f, 2f);
            spawnPosition.x = Random.Range(-2.3f, 2.3f);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }

        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
