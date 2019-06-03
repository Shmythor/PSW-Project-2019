using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerTest
    {
        static GameObject playerGameObject = new GameObject("player");
        Player player = playerGameObject.AddComponent<Player>();
        
        

        [UnityTest]
        public IEnumerator PlayerTestWithEnumeratorPasses()
        {

            yield return null;
        }

        [Test]
        public void player_test_initial_variables()
        {
            HealthComponent healths = new HealthComponent(player);
            Assert.AreEqual(3, player.getHearts());
            player.setHearts(2);
            Assert.AreEqual(2, player.getHearts());
            Assert.AreEqual(4f, player.getSpeed());
            player.setSpeed(3f);
            Assert.AreEqual(3f, player.getSpeed());
        }

        [Test]
        public void player_test_movement()
        {
            Rigidbody2D rb2d = new GameObject("Test movement").AddComponent<Rigidbody2D>();
            rb2d.gravityScale = 0f;
            MovementComponent movement = new MovementComponent(rb2d, 4);
            movement.movement(1, 0, 1);
            if (rb2d.velocity != new Vector2(4, 0))
                Assert.Fail();
        }

        [Test]
        public void player_test_damage()
        {
            ICharacter character = new Bunny();
            HealthComponent healths = new HealthComponent(character);
            healths.reciveDamage(100);
            Assert.AreEqual(2, healths.getHearts());
        }

        

    }
}
