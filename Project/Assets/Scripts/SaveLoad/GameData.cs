using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameData {
    /* Greedy Stats */
    public float[] greedyPosition;
    public int hearts, calories, fruitsEaten, fruitsToEat;
    public float damage;
    /* Game Stats */
    public int level;
    public float time;
    public string date;
    /* Consumables statas */
    public float[][] grapePositions, pumpkinPositions, heartPositions, energyPositions;
    /* Enemies stats */
    public float[][] impPositions, bunnyPositions, holePostions, firePositions;


    public GameData(GameObject[] consumables, GameObject[] enemies, GameObject greedy, int level, int hearts, int calories, int fruitsEaten, int fruitsToEat, float damage, float time)
    {        
        this.hearts = hearts;
        this.fruitsEaten = fruitsEaten;
        this.fruitsToEat = fruitsToEat;
        this.calories = calories;
        this.damage = damage;
        this.level = level;
        this.time = time;

        getConsumablePositions(consumables);
        getEnemyPositions(enemies);
        getGreedyPosition(greedy);

        this.date = DateTime.Now.ToString("dd/MM/yyyy");
    }

    private void getConsumablePositions(GameObject[] consumables) {
        Vector3 position;
        List<float[]> grapes = new List<float[]>(), pumpkins = new List<float[]>(), 
                    energies = new List<float[]>(), hearts = new List<float[]>();

        foreach(GameObject consumable in consumables) {
            if(consumable.GetComponent<Grape>() != null) {            
                position = consumable.transform.position;
                grapes.Add(new float[] {position.x, position.y, position.z});
            }
            if(consumable.GetComponent<Pumpkin>() != null) {
                position = consumable.transform.position;
                pumpkins.Add(new float[] {position.x, position.y, position.z});
            }
            if(consumable.GetComponent<Energy>() != null) {
                position = consumable.transform.position;
                energies.Add(new float[] {position.x, position.y, position.z});
            }
            if(consumable.GetComponent<Heart>() != null) {
                position = consumable.transform.position;
                hearts.Add(new float[] {position.x, position.y, position.z});
            }
        }

        this.grapePositions = grapes.ToArray();
        this.pumpkinPositions = pumpkins.ToArray();
        this.energyPositions = energies.ToArray();
        this.heartPositions = hearts.ToArray();
    }

    private void getEnemyPositions(GameObject[] enemies) {
        Vector3 position;
        List<float[]> imps = new List<float[]>(), bunnies = new List<float[]>(); 

        foreach(GameObject enemy in enemies) {
            if(enemy.GetComponent<Imp>() != null) {            
                position = enemy.transform.position;
                imps.Add(new float[] {position.x, position.y, position.z});
            }
            if(enemy.GetComponent<Bunny>() != null) {
                position = enemy.transform.position;
                bunnies.Add(new float[] {position.x, position.y, position.z});
            }
        }

        this.impPositions = imps.ToArray();
        this.bunnyPositions = bunnies.ToArray();
    }

    private void getGreedyPosition(GameObject greedy) {
        Vector3 position = greedy.transform.position;
        greedyPosition = new float[] {position.x, position.y, position.z};
    }
}


