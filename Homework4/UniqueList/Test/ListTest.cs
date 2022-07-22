using System;

using System.Collections.Generic;
using NUnit.Framework;
using List = UniqueList.List;

namespace Test;

public class ListTests
{
    private static IEnumerable<TestCaseData> Lists => new TestCaseData[]
    {
        new(new UniqueList.UniqueList()),
        new(new List())
    };
    
    
    [TestCaseSource(nameof(Lists))]
    public void Test_AddingNumbers_Should_IncreaseCount(List list)
    {
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        Assert.AreEqual(1, list.Count);
        list.Add(2);
        Assert.AreEqual(2, list.Count);
        list.Add(3);
        Assert.AreEqual(3, list.Count);
        list.Remove(3);
        Assert.AreEqual(2, list.Count);
        list.Remove(2);
        Assert.AreEqual(1, list.Count);
        list.Remove(1);
        Assert.AreEqual(0, list.Count);
        
    }
    
    [TestCaseSource(nameof(Lists))]
    public void Test_AddingAndIndexer_WorksCorrectly(List list)
    {
        list.Add(1);
        list.Add(2);
        list.Add(3);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.Throws<IndexOutOfRangeException>(() => list[3] = 5);
    }
    
    [TestCaseSource(nameof(Lists))]
    public void Test_AssigningNonExistentIndex_Should_ThrowException(List list)
    {
        Assert.Throws<IndexOutOfRangeException>(() => list[0] = 1);
        list.Add(1);
        Assert.Throws<IndexOutOfRangeException>(() => list[1] = 1);
    }
}