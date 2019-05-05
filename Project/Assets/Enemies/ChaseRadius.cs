using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseRadius : MonoBehaviour
{
    private AEnemy enemyController;




    // It's not in the main GameObject of the enemy because we will might use OnTriggerEnter/OnTriggerExit in Enemy for other purposes
    private void Awake()
    {
        enemyController = GetComponentInParent<AEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Default behaviour is that the enemy is going to chase only the player
        IPlayer player = collision.GetComponent<IPlayer>();
        if (player != null)
        {
            enemyController.changeState(player, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IPlayer player = collision.GetComponent<IPlayer>();
        if (player != null)
        {
            enemyController.changeState(player, false);
        }
    }
}
