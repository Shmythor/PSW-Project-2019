using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] GameObject[] maps;
    [SerializeField] int activeMap;

    public static MapController instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void setMap(int level) {
        maps[activeMap].SetActive(false);
        activeMap = level - 1;
        maps[activeMap].SetActive(true);
    }

    public GameObject getActiveMap() {
        return maps[activeMap];
    }

}
