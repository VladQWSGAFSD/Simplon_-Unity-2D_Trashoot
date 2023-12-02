using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
public class SpaceshipDeactivationTests
{
    private GameObject testGameObject;
    private SpaceshipController spaceship;
    private IGameOver mockGameStateManager;

    [SetUp]
    public void Setup()
    {
        testGameObject = new GameObject("TestSpaceship");
        spaceship = testGameObject.AddComponent<SpaceshipController>();

        //mockGameStateManager = Substitute.For<IGameOver>();

        //spaceship.Initialize(mockGameStateManager);
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(testGameObject);
    }

    [UnityTest]
    public IEnumerator ShipDeactivatesCorrectly()
    {
        //var mockGameStateManager = Substitute.For<IGameOver>();
       // spaceship.Initialize(mockGameStateManager);

        spaceship.DeactivateObject();

        Assert.IsFalse(testGameObject.activeSelf, "Ship did not deactivate correctly.");
        //mockGameStateManager.Received().TriggerGameOver();

        yield return null;
    }
}
