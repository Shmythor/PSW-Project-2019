using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerTest
    {
        Player player = new GameObject("player").AddComponent<Player>();

        

        [UnityTest]
        public IEnumerator PlayerTestWithEnumeratorPasses()
        {
            
            yield return null;
        }

        [Test]
        public void player_Test_Hearts()
        {
            HealthComponent healths = new HealthComponent(player);
            Assert.AreEqual(3, player.getHearts());
            player.setHearts(2);
            Assert.AreEqual(2, player.getHearts());
        }

        [Test]
        public void player_test_Movement()
        {

            Rigidbody2D rb2d = new GameObject("Test movement").AddComponent<Rigidbody2D>();
            rb2d.gravityScale = 0f;
            MovementComponent movement = new MovementComponent(rb2d, 4);
            movement.movement(1,0,1);
            if (rb2d.velocity != new Vector2(4, 0))
                Assert.Fail();
        }
    }
}
