using System.Collections.Generic;
using NUnit.Framework;
using Routers;

namespace RoutersTest;

public class Tests
{
    [Test]
    public void Test_BigGraphWIthEqualEdges()
    {
        var rn = new RoutersNetwork("TestBigGraphWithEqualEdges.txt");
        rn.BuildNetwork("output.txt");
        var expectedEdges = new Dictionary<Edge, int>
        {
            { new Edge(1, 2, 10), 10 },
            { new Edge(2, 6, 9), 9 },
            { new Edge(4, 6, 7), 7 }, // тут возможно ребро (5,6) 7 , но программа построит дерево с ребром (4, 6) 7
            { new Edge(4, 5, 20), 20 },
            { new Edge(3, 4, 6), 6 },
            { new Edge(6, 7, 4), 4 },
            { new Edge(6, 8, 1), 1 }
        };
        Assert.AreEqual(expectedEdges.Count, rn.TreeEdges.Count);
        foreach (var (key, _) in expectedEdges)
        {
            Assert.AreEqual(expectedEdges[key], rn.TreeEdges[key]);
        }
    }
    
    [Test]
    public void Test_FromTask()
    {
        var rn = new RoutersNetwork("TestFromTask.txt");
        rn.BuildNetwork("output.txt");
        var expectedEdges = new Dictionary<Edge, int>
        {
            { new Edge(1, 2, 10), 10 },
            { new Edge(1, 3, 5), 5 },
        };
        Assert.AreEqual(expectedEdges.Count, rn.TreeEdges.Count);
        foreach (var (key, _) in expectedEdges)
        {
            Assert.AreEqual(expectedEdges[key], rn.TreeEdges[key]);
        }
    }
    
    [Test]
    public void Test_UnconnectedNetwork()
    {
        var rn = new RoutersNetwork("TestUnconnectedNetwork.txt");
        Assert.AreEqual(rn.BuildNetwork("output.txt"), -1);
    }
    
    [Test]
    public void Test_NetworkMatchesOriginalGraph()
    {
        var rn = new RoutersNetwork("TestTrivialNetwork1.txt");
        rn.BuildNetwork("output.txt");
        var expectedEdges = new Dictionary<Edge, int>
        {
            { new Edge(1, 2, 1), 1 },
            { new Edge(2, 3, 2), 2 },
            { new Edge(2, 4, 3), 3 }
        };
        Assert.AreEqual(expectedEdges.Count, rn.TreeEdges.Count);
        foreach (var (key, _) in expectedEdges)
        {
            Assert.AreEqual(expectedEdges[key], rn.TreeEdges[key]);
        }
    }
    
    [Test]
    public void Test_OneEdgeNetwork()
    {
        var rn = new RoutersNetwork("TestTrivialNetwork2.txt");
        rn.BuildNetwork("output.txt");
        var expectedEdges = new Dictionary<Edge, int>
        {
            { new Edge(1, 2, 1), 1 },
        };
        Assert.AreEqual(expectedEdges.Count, rn.TreeEdges.Count);
        foreach (var (key, _) in expectedEdges)
        {
            Assert.AreEqual(expectedEdges[key], rn.TreeEdges[key]);
        }
    }
}