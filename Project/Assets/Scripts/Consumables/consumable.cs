﻿

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : MonoBehaviour
{
    public abstract int calories { get; set; }
    public abstract int chanceOfSpawn { get; }
    public abstract string typeOfConsumable { get; }
}
