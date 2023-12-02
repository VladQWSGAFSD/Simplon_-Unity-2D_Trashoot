using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SpaceshipMovementTests
{
    private GameObject testGameObject;
    private SpaceshipController spaceship;

    [SetUp]
    public void Setup()
    {
        testGameObject = new GameObject("TestSpaceship");
        spaceship = testGameObject.AddComponent<SpaceshipController>();
        //arrange zero cause moveing up will if rotation.z= 90 position.x will be 0
        testGameObject.transform.position = Vector3.zero;
        //arrange initialPosition so that it doesnt depend on vector3.zero and then Assert.AreEqual(initialPosition, testGameObject.transform.position)
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(testGameObject);
    }

    [UnityTest]
    public IEnumerator ShipMovesUp()
    {
        // Act
        spaceship.MoveUp();

        yield return null;

        // Assert
        Assert.Greater(testGameObject.transform.position.y, 0, "Ship did not move up.");
    }

    [UnityTest]
    public IEnumerator ShipRotatesLeft()
    {
        // Act
        spaceship.RotateLeft();

        yield return null;

        // Assert
        Assert.Greater(testGameObject.transform.rotation.z, 0, "Ship did not move left.");
    }

    [UnityTest]
    public IEnumerator ShipRotatesRight()
    {
        // Act
        spaceship.RotateRight();

        yield return null;

        // Assert
        Assert.Less(testGameObject.transform.rotation.z, 0, "Ship did not move right.");
    }

    [UnityTest]
    public IEnumerator ShipDoesNotMoveDown()
    {
        // Arrange
        Vector3 initialPosition = testGameObject.transform.position;

        // Act
        spaceship.MoveDown();

        yield return null;

        // Assert
        //same things these two
       // Assert.AreEqual(initialPosition, testGameObject.transform.position, "Ship moved down, but it should not.");
        Assert.That(testGameObject.transform.position, Is.EqualTo(initialPosition));
    }
}
