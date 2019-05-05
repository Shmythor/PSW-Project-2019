using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searching : AStateEnemy
{

    private float radiusOfSearching = 10f;
    private float acceptableDistanceToPoint = 2f;
    private float timeToNextLocation = 2f;
    private float timer;
    private bool hasReached = false;
    private bool waitForNextPoint = true;
    

    public Searching(Rigidbody2D rb2d, float defSpeed) : base(rb2d, defSpeed){ actualSpeed = defSpeed * 0.6f; timer = timeToNextLocation; character = null; }

    public override void movement()
    {
        hasReached = isItReached();
        if (hasReached == true)
        {
            timer -= Time.fixedDeltaTime;
            if(timer < 0)
            {
                randomMovePosition();
                timer = timeToNextLocation;  
            }
            return;
        }
        base.movement();
    }


    private bool isItReached()
    {
        return Vector2.Distance(rb2d.GetComponent<Transform>().position, movePosition) > acceptableDistanceToPoint ? false : true;
    }

    private void randomMovePosition()
    {
        Vector2 actualPosition = rb2d.GetComponent<Transform>().position;
        movePosition = new Vector2(Random.Range(actualPosition.x - radiusOfSearching, actualPosition.x + radiusOfSearching),
                                   Random.Range(actualPosition.y - radiusOfSearching, actualPosition.y + radiusOfSearching));
    }
    

}
