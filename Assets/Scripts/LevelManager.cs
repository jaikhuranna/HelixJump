using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] platformPrefabs;
    [SerializeField] private GameObject Core;
    [SerializeField] private BoxCollider loadingTrigger;

    [SerializeField] private int platformPoolSize = 20;
    [SerializeField] private int platformGap = 5;
    public List<GameObject> platformPool = new List<GameObject>();

    private Random random; 
    private int positionIncrement = 5;
    private void Start()
    {
        Random random = new Random();
        for (int i = 0; i < platformPoolSize; i++)
        {
            platformPool.Add(Instantiate(platformPrefabs[random.Next(6)], Core.transform));
            platformPool[i].SetActive(false);
        }
        
        // Loads inital 20 platfroms form the pool
        loadPlatformFromPool(20);
    }

    private void loadPlatformFromPool(int platformCount)
    {
        Random random = new Random();
        for (int j = 0; j < platformCount; j++)
        {
            GameObject localPlatform = platformPool[random.Next(platformPool.Count)];
            localPlatform.SetActive(true);
            localPlatform.transform.position = new Vector3(
                0, 
                0 - positionIncrement, 
                0);
            // localPlatform.transform.Rotate(new Vector3(0, 1, 0));
            positionIncrement += platformGap;
            print("positionIncrement:" +  positionIncrement);
        }
    }

}
