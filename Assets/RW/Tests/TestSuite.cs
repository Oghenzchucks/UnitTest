using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestSuite
    {
        [SetUp]
        public void Setup()
        {
            GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
            game = gameGameObject.GetComponent<Game>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(game.gameObject);
        }
        
        private Game game;

        [UnityTest]
        public IEnumerator TestSuiteWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            GameObject asteriod = game.GetSpawner().SpawnAsteroid();

            //4
            float initialYPos = asteriod.transform.position.y;

            //5
            yield return new WaitForSeconds(0.1f);

            //6
            Assert.Less(asteriod.transform.position.y, initialYPos);
            
            //7
            
        }

        [UnityTest]
        public IEnumerator GameOverOccursOnAsteroidCollision()
        {
            GameObject asteriod = game.GetSpawner().SpawnAsteroid();

            //2
            asteriod.transform.position = game.GetShip().transform.position;

            //3
            yield return new WaitForSeconds(0.1f);

            //4
            Assert.True(game.isGameOver);
        }

        [UnityTest]
        public IEnumerator NewGameRestartsGame()
        {
            game.isGameOver = true;
            game.NewGame();

            //1
            Assert.False(game.isGameOver);
            yield return null;
        }

        [UnityTest]
        public IEnumerator LaserMovesUp()
        {
            GameObject laser = game.GetShip().SpawnLaser();

            float initialYpos = laser.transform.position.y;
            yield return new WaitForSeconds(0.1f);

            Assert.Greater(laser.transform.position.y, initialYpos);
        }

        [UnityTest]
        public IEnumerator LaserDestroyAsteriod()
        {
            //1
            GameObject asteriod = game.GetSpawner().SpawnAsteroid();
            asteriod.transform.position = Vector3.zero;
            GameObject laser = game.GetShip().SpawnLaser();
            laser.transform.position = Vector3.zero;
            yield return new WaitForSeconds(0.1f);

            //2
            UnityEngine.Assertions.Assert.IsNull(asteriod);
        }

        [UnityTest]
        public IEnumerator DestroyedAsteroidRaisesScore()
        {
            // 1
            GameObject asteroid = game.GetSpawner().SpawnAsteroid();
            asteroid.transform.position = Vector3.zero;
            GameObject laser = game.GetShip().SpawnLaser();
            laser.transform.position = Vector3.zero;
            yield return new WaitForSeconds(0.1f);
            // 2
            Assert.AreEqual(game.score, 1);
        }

    }
}