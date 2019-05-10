using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AComponent : IComponent
{
    protected bool enabled = true;
   
    public void enable() { enabled = true; }
    public void disable() { enabled = false; }

}
