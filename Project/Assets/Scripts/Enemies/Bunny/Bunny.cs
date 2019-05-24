using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : AEnemy
{
    [Header("Dust particles")]
    [SerializeField] private float minSpeedForDustParticles = 1.5f;
    [SerializeField] private GameObject dustParticles;
    private IEnemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<IEnemy>();
    }


    private void Update()
    { 
        if (enemy.getVelocity().SqrMagnitude() > minSpeedForDustParticles)
            dustParticles.SetActive(true);
        else
            dustParticles.SetActive(false);
    }
}
