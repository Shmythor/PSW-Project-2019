using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerTest
    {      
        
        [Test]
        public void player_test_initial_variables()
        {
            GameObject playerGameObject = new GameObject("player");
            Player player = playerGameObject.AddComponent<Player>();
            Assert.AreEqual(3, player.getHearts());
            Assert.AreEqual(4f, player.getSpeed());
        }

        [Test]
        public void player_movement_test()
        {
            Rigidbody2D rb2d = new GameObject("Test movement").AddComponent<Rigidbody2D>();
            rb2d.gravityScale = 0f;
            MovementComponent movement = new MovementComponent(rb2d, 4);
            movement.movement(1, 0, 1);
            if (rb2d.velocity != new Vector2(4, 0))
                Assert.Fail();
        }

        [Test]
        public void player_damage_test()
        {
            Player playerTest = new GameObject("Damage test").AddComponent<Player>();
            HealthComponent healths = new HealthComponent(playerTest);
            healths.reciveDamage(100);
            Assert.AreEqual(2, healths.getHearts());
        }

       [Test]
        public void enemyNumber_spawn_by_level_test()
        {

            GameController gc = new GameController();
            EnemyFabric enemyFabric = new GameObject("Enemy Spawn test").AddComponent<EnemyFabric>();
            
            List<IEnemy> enemiesList = new List<IEnemy>();
            enemiesList = enemyFabric.spawnEnemies(1);         
            Assert.AreEqual(2,enemiesList.Count);

            enemiesList.Clear();
            enemiesList = enemyFabric.spawnEnemies(4);
            Assert.AreEqual(4, enemiesList.Count);

            enemiesList.Clear();
            enemiesList = enemyFabric.spawnEnemies(6);
            Assert.AreEqual(6, enemiesList.Count);
        }
    }
}
