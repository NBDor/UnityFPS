using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    #region Singleton
    public GameObject player;
    public static PlayerManager instance;

    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    #endregion
    
}
