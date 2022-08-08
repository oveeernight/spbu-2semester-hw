using System.Collections.Generic;
using NUnit.Framework;
using Routers;

namespace RoutersTest;

public class Tests
{
    [Test]
    public void Test_BigGraph()
    {
        var routersNetwork = new RoutersNetwork("TestBigGraph.txt");
        routersNetwork.BuildNetwork("output.txt");
        var expectedEdges = new List<Edge>
        {
            new Edge(1, 2, 10),
            new Edge(2, 6, 9), 
            new Edge(4, 6, 7), 
            new Edge(4, 5, 20), 
            new Edge(3, 4, 6),
            new Edge(6, 7, 4), 
            new Edge(6, 8, 1),
        };
        var treeEdges = routersNetwork.TreeEdges;
        Assert.AreEqual(expectedEdges.Count, treeEdges.Count);
        for (var i = 0; i < treeEdges.Count; i++)
        {
            
            Assert.AreEqual(expectedEdges[i], treeEdges[i]);
        }
    }
    
    [Test]
    public void Test_FromTask()
    {
        var routersNetwork = new RoutersNetwork("TestFromTask.txt");
        routersNetwork.BuildNetwork("output.txt");
        var expectedEdges = new List<Edge>
        {
            new Edge(1, 2, 10), 
            new Edge(1, 3, 5), 
        };
        var treeEdges = routersNetwork.TreeEdges;
        Assert.AreEqual(expectedEdges.Count, treeEdges.Count);
        for (var i = 0; i < treeEdges.Count; i++)
        {
            
            Assert.AreEqual(expectedEdges[i], treeEdges[i]);
        }
    }
    
    [Test]
    public void Test_UnconnectedNetwork()
    {
        var routersNetwork = new RoutersNetwork("TestUnconnectedNetwork.txt");
        Assert.AreEqual(routersNetwork.BuildNetwork("output.txt"), -1);
    }
    
    [Test]
    public void Test_NetworkMatchesOriginalGraph()
    {
        var routersNetwork = new RoutersNetwork("TestTrivialNetwork1.txt");
        routersNetwork.BuildNetwork("output.txt");
        var expectedEdges = new List<Edge>
        {
            new Edge(1, 2, 1), 
            new Edge(2, 4, 3), 
            new Edge(2, 3, 2),
        };
        var treeEdges = routersNetwork.TreeEdges;
        Assert.AreEqual(expectedEdges.Count, treeEdges.Count);
        for (var i = 0; i < treeEdges.Count; i++)
        {
            
            Assert.AreEqual(expectedEdges[i], treeEdges[i]);
        }
    }
    
    
    [Test]
    public void Test_OneEdgeNetwork()
    {
        var routersNetwork = new RoutersNetwork("TestTrivialNetwork2.txt");
        routersNetwork.BuildNetwork("output.txt");
        var expectedEdges = new List<Edge>
        {
            new Edge(1, 2, 1), 
        };
        var treeEdges = routersNetwork.TreeEdges;
        Assert.AreEqual(expectedEdges[0], treeEdges[0]);
    }
}