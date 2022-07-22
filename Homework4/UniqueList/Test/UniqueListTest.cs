using System;
using UniqueList;
using NUnit.Framework;

namespace UniqueListTest;

public class UniqueListTests
{
    private UniqueList.UniqueList list = new ();
    
    [SetUp]
    public void Setup()
    {
        list = new UniqueList.UniqueList();
    }
    
    [Test]
    public void Test_AddingExistingNumber_Should_ThrowException()
    {
        list.Add(1);
        Assert.Throws<AddingExistingNumberException>(() => list.Add(1));
    }
    
    [Test]
    public void Test_RemovingNonExistingNumber_Should_ThrowException()
    {
        Assert.Throws<RemovingNonExistingNumberException>(() => list.Remove(1));
    }
    
    [Test]
    public void Test_RemoveExistingNumber_ShouldNot_ThrowException()
    {
        list.Add(1);
        Assert.DoesNotThrow(() => list.Remove(1));
    }
    
    [Test]
    public void Test_AddingNonExistingNumber_ShouldNot_ThrowException()
    {
        Assert.DoesNotThrow(() => list.Add(1));
    }
    
    [Test]
    public void Test_AssigningExistingValue_Should_ThrowException()
    {
        list.Add(1);
        list.Add(2);
        Assert.Throws<AddingExistingNumberException>(() => list[1] = 1);
    }
    
    [Test]
    public void Test_Reassigning_ShouldNot_ThrowException()
    {
        list.Add(1);
        Assert.DoesNotThrow(() => list[0] = 1);
    }
    
    [Test]
    public void Test_Zero_ShouldNotBeContainedAfterRemove()
    {
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Remove(1);
        Assert.DoesNotThrow((() => list[1] = 0));
    }

    [Test]
    public void Test_RemovingItemAndAddAgain_ShouldNot_ThrowException()
    {
        list.Add(1);
        list.Remove(1);
        Assert.DoesNotThrow(() => list.Add(1));
    }
}