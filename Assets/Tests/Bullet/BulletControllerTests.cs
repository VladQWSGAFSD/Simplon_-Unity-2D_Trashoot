using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BulletControllerTests
{
    private GameObject testBulletObject;
    private BulletController bulletController;
    private bool deactivateCalled;
    private void HandleOnDeactivate()
    {
        deactivateCalled = false;
    }


    [SetUp]
    public void Setup()
    {
        testBulletObject = new GameObject("TestBullet");
        testBulletObject.AddComponent<Rigidbody2D>();
        bulletController = testBulletObject.AddComponent<BulletController>();
        bulletController.Initialize(Vector2.up);


        //bulletController.OnDeactivate += HandleOnDeactivate;
        deactivateCalled = false;
        //use lambda instead of creating a method that changes deactivateCalled
        bulletController.OnDeactivate += () => deactivateCalled = true;
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(testBulletObject);
    }

    [UnityTest]
    public IEnumerator DeactivateObjectIsCalledAfterLifetime()
    {
        // Act
        yield return new WaitForSeconds(bulletController.lifeTime);

        // Assert
        Assert.IsTrue(deactivateCalled, "DeactivateObject was not called after bullet lifetime.");
    }
}
