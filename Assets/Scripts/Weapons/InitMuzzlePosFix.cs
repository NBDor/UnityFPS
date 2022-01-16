using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMuzzlePosFix : MonoBehaviour
{

    // for particle effects that have longer lifetime
    public GameObject pos;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        transform.position = pos.transform.position;
    }
}
