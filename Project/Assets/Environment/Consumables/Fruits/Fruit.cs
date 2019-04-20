using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    // References
    Rigidbody2D rb2d;
    BoxCollider2D bc2d;

    private float deltaTime;
    private float verInput;
    private float horInput;


    public void Init()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag.Equals("fruit")) {
            Destroy(other.gameObject);
            Debug.Log("entered");
        }
        Debug.Log("32e");
    }

}