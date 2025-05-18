using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] platformPrefabs;
    [SerializeField] private GameObject Core;

    [SerializeField] private int platformPoolSize = 20;
    [SerializeField] private float platformGap = 5f;
    
    public List<GameObject> platformPool = new List<GameObject>();
    private List<int> availableIndices = new List<int>();  
    
    private Random random;
    private float positionIncrement = 0f;
    public int totalPlaced = 0;

    private void Start()
    {
        random = new Random();
        for (int i = 0; i < platformPoolSize; i++)
        {
            platformPool.Add(Instantiate(platformPrefabs[random.Next(platformPrefabs.Length)], Core.transform));
            platformPool[i].SetActive(false);
            availableIndices.Add(i);  
        }
        
        // Load initial platforms
        loadPlatformFromPool(20);
    }

    public void loadPlatformFromPool(int platformCount)
    {
        for (int j = 0; j < platformCount; j++)
        {
            if (availableIndices.Count == 0)
            {
                Debug.LogWarning("All platforms in use! Consider increasing pool size.");
                return;
            }
            
            int randomIndex = random.Next(0, availableIndices.Count);
            int platformIndex = availableIndices[randomIndex];
            availableIndices.RemoveAt(randomIndex);
            
            GameObject localPlatform = platformPool[platformIndex];
            localPlatform.SetActive(true);
            
            PlatformData data = localPlatform.GetComponent<PlatformData>();
            if (data == null)
            {
                data = localPlatform.AddComponent<PlatformData>();
            }
            data.poolIndex = platformIndex;
            
            localPlatform.transform.position = new Vector3(0, -1f * positionIncrement, 0);
            localPlatform.transform.Rotate(new Vector3(0, (float)random.NextDouble() * 360f, 0));
            positionIncrement += platformGap;
            totalPlaced++;
        }
    }
    
    public void ReturnToPool(GameObject platform)
    {
        PlatformData data = platform.GetComponent<PlatformData>();
        if (data != null && !availableIndices.Contains(data.poolIndex))
        {
            availableIndices.Add(data.poolIndex);
        }
    }

    public void ResetLevel()
    {
        foreach (GameObject platform in platformPool)
        {
            platform.SetActive(false);
        }
        availableIndices.Clear();
        for (int i = 0; i < platformPool.Count; i++)
        {
            availableIndices.Add(i);
        }
        
        positionIncrement = 0f;
        totalPlaced = 0;
        
        loadPlatformFromPool(20);
    }
}

public class PlatformData : MonoBehaviour
{
    public int poolIndex;
}
