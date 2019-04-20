using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject fruitPrefab;

    // Start is called before the first frame update
    void Start()
    {
        createFruit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void createFruit() {
        Instantiate(fruitPrefab, transform.position, Quaternion.identity);
    }

    
}
