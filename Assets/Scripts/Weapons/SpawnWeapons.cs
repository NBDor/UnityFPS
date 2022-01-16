using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnWeapons : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject RiflePrefab;
    public Transform RilfeSpawnPositions;
    public GameObject LaserPrefab;
    public Transform LaserSpawnPositions;
    public GameObject GrenadePrefab;
    public Transform GrenadeSpawnPositions;

    [Range (0f, 100f)]
    public float SpawnPercentage = 55f;
    
    void Start()
    {
        Spawn(GrenadePrefab, GrenadeSpawnPositions);
        Spawn(RiflePrefab, RilfeSpawnPositions);
        Spawn(LaserPrefab, LaserSpawnPositions);
    }
    
    public void Spawn(GameObject weaponPrefab, Transform positions)
    {
        foreach (Transform currentPosition in positions)
        {
            if (SpawnPercentage >= Random.Range (0f, 100f))
            {
                GameObject instance = Instantiate(weaponPrefab, currentPosition.position, Quaternion.identity);
                instance.transform.rotation = weaponPrefab.transform.rotation;
            }
            
        }
    }
}
