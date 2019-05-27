using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] GameObject[] maps;
    [SerializeField] int activeMap = 0;

    public static MapController instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void setMap(int level) {
        this.activeMap = level - 1; 

        for(int i = 0; i < maps.Length; i++)
        {
            if(i != this.activeMap) maps[i].SetActive(false);
        }

        maps[this.activeMap].SetActive(true);
        
        
    }

    public GameObject getActiveMap() {
        return maps[this.activeMap];
    }

}
