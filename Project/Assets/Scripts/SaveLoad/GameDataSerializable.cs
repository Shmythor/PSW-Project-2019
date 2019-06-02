using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] 
public class GameDataSerializable {
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
    public float[][] impPositions, bunnyPositions;// holePostions, firePositions;

    public GameDataSerializable(GameData data)
    {
        this.greedyPosition = data.greedyPosition;
        this.hearts = data.hearts;
        this.calories = data.calories;
        this.fruitsEaten = data.fruitsEaten;
        this.fruitsToEat = data.fruitsToEat;
        this.damage = data.damage;
        this.level = data.level;
        this.time = data.time;
        this.date = data.date;
        this.grapePositions = data.grapePositions;
        this.pumpkinPositions = data.pumpkinPositions;
        this.heartPositions = data.heartPositions;
        this.energyPositions = data.energyPositions;
        this.impPositions = data.impPositions;
        this.bunnyPositions = data.bunnyPositions;
        
        //this.holePostions = data.holePostions;
        //this.firePositions = data.firePositions;
    }
}