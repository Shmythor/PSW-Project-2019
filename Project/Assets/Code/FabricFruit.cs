using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricFruit : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GrapePrefab, PumpkinPrefab;
    public GameObject UIController;

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
        Instantiate(GrapePrefab, transform.position, Quaternion.identity);
        Instantiate(PumpkinPrefab, transform.position + new Vector3(5, 5, 0), Quaternion.identity);
    }

    public void consumeCalories(int calories) {
        UIController.SendMessage("updateCalories", calories);
    }

    
}
