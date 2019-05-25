using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searching : AStateEnemy
{

    private float radiusOfSearching = 6f;
    private float acceptableDistanceToPoint = 2f;
    private float timeToNextLocation = 2f;
    private float timer;
    private float timerWalking;
    private float timeMaximumWalking = 3f;
    private bool hasReached = false;
    private bool waitForNextPoint = true;
    

    public Searching(Rigidbody2D rb2d, float defSpeed) : base(rb2d, defSpeed){
        actualSpeed = defSpeed * 0.6f;
        timer = timeToNextLocation;
        character = null;
        randomMovePosition();
    }

    public override void movement()
    {
        hasReached = isItReached();
        if (hasReached == true)
        {
            timeWaitOnPoint();
            return;
        }
        else
        {
            timeWalking();
        }
        base.movement();
    }

    private void timeWaitOnPoint()
    {
        timer -= Time.fixedDeltaTime;
        if (timer < 0)
        {
            randomMovePosition();
            timer = timeToNextLocation;
        }
    }

    private void timeWalking()
    {
        timer -= Time.fixedDeltaTime;
        if (timer < 0)
        {
            randomMovePosition();
            timer = timeMaximumWalking;
        }
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
