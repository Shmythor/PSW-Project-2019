using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevelPrefabs : MonoBehaviour
{
    public static NewLevelPrefabs instance = null;
    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            //There can only ever be one instance of this object!!
            Destroy(gameObject);  
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);        
    }
}

