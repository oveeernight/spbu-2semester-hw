using NUnit.Framework;

namespace TrieTest;

public class Tests
{
    private Trie.Trie trie = new();

    [SetUp]
    public void Setup()
    {
        trie = new Trie.Trie();
    }

    [Test]
    public void AddingStoredStringShallDoNothing()
    {
        trie.Add("string");
        trie.Add("string");
        Assert.AreEqual(1, trie.Size);
        Assert.IsTrue(trie.Contains("string"));
    }

    [Test]
    public void RemoveNonExistentStringShallDoNothing()
    {
        trie.Add("abc");
        trie.Add("efg");
        trie.Remove("d");
        Assert.AreEqual(2, trie.Size);
    }

    [Test]
    public void RemoveMethodShallRemoveString()
    {
        trie.Add("someString");
        trie.Remove("someString");
        Assert.IsFalse(trie.Contains("someString"));
    }

    [Test]
    public void NonStoredStringShallNotBeContained()
    {
        trie.Add("gdfgdfgdf");
        Assert.IsFalse(trie.Contains("gd"));
        Assert.IsFalse(trie.Contains("gdfgdfgdfa"));
    }

    [Test]
    public void HowManyStartsWithNonStoredPrefixWorksCorrectly()
    {
        trie.Add("12324");
        trie.Add("1");
        trie.Add("12");
        trie.Add("123241");
        trie.Add("56765");
        trie.Remove("1");
        trie.Add("abc");
        trie.Add("abcd");
        trie.Add("a");
        Assert.AreEqual(3, trie.HowManyStartsWithPrefix("1"));
        Assert.AreEqual(3, trie.HowManyStartsWithPrefix("a"));
        Assert.AreEqual(0, trie.HowManyStartsWithPrefix("abcde"));
    }
}