using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLocation : MonoBehaviour
{

    public GameObject RiflePrefab;
    public Vector3[] positions;
    // public Vector3[] positions = new Vector3[] { 
    //     new Vector3(9.98,0.68,-7),
    //     new Vector3(24,3.23,-7),
    //     new Vector3(14.32,0.32,12)
    //     // new Vector3(14.326,0.32,12.08)

    // };

    // Start is called before the first frame update
    void Start()
    {
        int random = Random.Range(0,positions.Length);
        SpawnRifle(positions[random]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRifle(Vector3 position)
    {
        Debug.Log(" SpawnRifle called with position: " + position);
        // Instantiate(RiflePrefab,position,Quaternion.identity);
        transform.position = position;
    }
}
