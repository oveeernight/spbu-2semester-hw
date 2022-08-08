using NUnit.Framework;

namespace ParseTreeTest;

public class Tests
{
    private ParseTree.ParseTree _tree;

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_TwoTreeOperands_GivesCorrectAnswer()
    {
        _tree = new ParseTree.ParseTree("(* (+ 5 7) (/ 4 2))");
        Assert.AreEqual(24, _tree.Result);
        _tree = new ParseTree.ParseTree("(* (+ 5 7) (/ 5 2))");
        Assert.AreEqual(24, _tree.Result);
        _tree = new ParseTree.ParseTree("(+ (* 1 0) (/ 0 2))");
        Assert.AreEqual(0, _tree.Result);
    }

    [Test]
    public void Test_OneTreeOperator_IsCalculatedCorrectly()
    {
        _tree = new ParseTree.ParseTree("(- (+ 1 2) 3)");
        Assert.AreEqual(0, _tree.Result);
        _tree = new ParseTree.ParseTree("(/ 3 (+ 1 2)");
        Assert.AreEqual(1, _tree.Result);
    }

    [Test]
    public void Test_PlusMultiply_AreCommutative()
    {
        var tree1 = new ParseTree.ParseTree("(* (+ 1 2) (* 5 6))");
        var tree2 = new ParseTree.ParseTree("(* (* 6 5) (+ 2 1))");
        Assert.AreEqual(tree1.Result, tree2.Result);
    }

    [Test]
    public void Test_LongExpression_IsCalculatedCorrectly()
    {
        _tree = new ParseTree.ParseTree("(* (+ (- 1 2) (/ 6 2)) (/ (+ 30 (- 5 3)) (* (+ 0 4) 4)))");
        Assert.AreEqual(4, _tree.Result);
    }

    [Test]
    public void Test_ExpressionWithoutTrees_IsCalculatedCorrectly()
    {
        _tree = new ParseTree.ParseTree("(+ 1 2)");
        Assert.AreEqual(3, _tree.Result);
        _tree = new ParseTree.ParseTree("(* 1 2)");
        Assert.AreEqual(2, _tree.Result);
        _tree = new ParseTree.ParseTree("(- 1 2)");
        Assert.AreEqual(-1, _tree.Result);
        _tree = new ParseTree.ParseTree("(/ 1 2)");
        Assert.AreEqual(0, _tree.Result);
        _tree = new ParseTree.ParseTree("(/ 6 2)");
        Assert.AreEqual(3, _tree.Result);
    }
}