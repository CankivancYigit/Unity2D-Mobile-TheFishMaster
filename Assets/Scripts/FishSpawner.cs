using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] Fish fishPrefab;
    [SerializeField] Fish.FishType[] fishTypes;


    void Awake()
    {
        for (int i = 0; i < fishTypes.Length; i++)
        {
            int fishNum = 0;
            while (fishNum < fishTypes[i].fishAmount)
            {
                Fish fish = Instantiate<Fish>(fishPrefab);
                fish.Type = fishTypes[i];
                fish.PlaceFish();
                fishNum++;
            }
            
        }
    }

    
    void Update()
    {
        
    }
}
