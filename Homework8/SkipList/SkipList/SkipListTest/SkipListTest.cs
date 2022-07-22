using System;
using NUnit.Framework;
using SkipList;

namespace SkipListTest;

public class Tests
{
    private SkipList<int> _list = new ();
    
    [SetUp]
    public void Setup()
    {
        _list = new SkipList<int>();
        _list.Add(9);
        _list.Add(1);
        _list.Add(15);
        _list.Add(30);
    }

    [Test]
    public void Test_Add_Should_AddItemToSkipList()
    {
        Assert.IsTrue(_list.Contains(9));
        Assert.IsTrue(_list.Contains(1));
        Assert.IsTrue(_list.Contains(15));
        Assert.IsTrue(_list.Contains(30));
    }
    
    [Test]
    public void Test_NonExistentItem_ShouldNot_BeContained()
    {
        Assert.False(_list.Contains(2));
        Assert.False(_list.Contains(3));
        Assert.False(_list.Contains(4));
        Assert.False(_list.Contains(5));
    }
    
    [Test]
    public void Test_Indexer_ShouldNot_DisturbOrder()
    {
        Assert.AreEqual(1, _list[0]);
        Assert.AreEqual(9, _list[1]);
        Assert.AreEqual(15, _list[2]);
        Assert.AreEqual(30, _list[3]);
    }

    [Test]
    public void Test_IndexSetter_Should_ThrowException()
    {
        Assert.Throws<NotSupportedException>(() => _list[0] = 100);
    }

    [Test]
    public void Test_Foreach_WorksCorrectly()
    {
        var expected = new [] { 1, 9, 15, 30 };
        var i = 0;
        Assert.AreEqual(expected.Length, _list.Count);
        Assert.IsTrue(_list.GetEnumerator().MoveNext());
        foreach (var item in _list)
        {
            Assert.AreEqual(expected[i], item);
            i++;
        }
    }
    
    [Test]
    public void Test_Remove_Should_DeleteItem()
    {
        _list.Remove(9);
        Assert.False(_list.Contains(9));
        _list.Remove(1);
        Assert.False(_list.Contains(1));
        _list.Remove(15);
        Assert.False(_list.Contains(15));
        _list.Remove(30);
        Assert.False(_list.Contains(30));
    }
    
    [Test]
    public void Test_IndexOf_ReturnsCorrectValue()
    {
        Assert.AreEqual(0, _list.IndexOf(1));
        Assert.AreEqual(1, _list.IndexOf(9));
        Assert.AreEqual(2, _list.IndexOf(15));
        Assert.AreEqual(3, _list.IndexOf(30));
        Assert.AreEqual(-1, _list.IndexOf(0));
    }
    
    [Test]
    public void Test_RemoveAt_Should_RemoveItem()
    {
        _list.RemoveAt(0);
        Assert.False(_list.Contains(1));
        Assert.AreEqual(3, _list.Count);
        _list.RemoveAt(0);
        Assert.False(_list.Contains(9));
        Assert.AreEqual(2, _list.Count);
        _list.RemoveAt(0);
        Assert.False(_list.Contains(15));
        Assert.AreEqual(1, _list.Count);
        _list.RemoveAt(0);
        Assert.False(_list.Contains(30));
        Assert.AreEqual(0, _list.Count);
        Assert.Throws<IndexOutOfRangeException>(() => _list.RemoveAt(1));
    }

    [Test]
    public void Test_CopyTo_Should_CopyItems()
    {
        var array = new int[4];
        _list.CopyTo(array, 0);
        var expected = new [] { 1, 9, 15, 30 };
        for (var i = 0; i < 4; i++)
        {
            Assert.AreEqual(expected[i], array[i]);
        }
    }
    
    [Test]
    public void Test_CopyToInvalidCount_Should_ThrowException()
    {
        var array = new int[4];
        Assert.Throws<IndexOutOfRangeException>((() => _list.CopyTo(array, 1)));
    }
}