namespace Trie;

/// <summary>
/// Represents trie object
/// </summary>
public class Trie
{
    /// <summary>
    /// Represents a Trie node
    /// </summary>
    private class TrieNode
    {
        public TrieNode()
        {
            Children = new Dictionary<char, TrieNode>();
        }
        
        public Dictionary<char, TrieNode> Children { get; }
        public bool IsTerminal { get; set; }
        public int HowManyStartsWith { get; set; }
    }
    
    private TrieNode root;

    public Trie()
    {
        root = new TrieNode()
        {
            HowManyStartsWith = 0
        };
        Size = 0;
    }
    
    /// <summary>
    /// Returns the number of stored strings
    /// </summary>
    public int Size { get; private set; }
    
    /// <summary>
    /// Adds element to Trie
    /// </summary>
    public bool Add(string element)
    {
        // наверное, плохо пробегать дважды, но теперь свойство HowManyStartsWith поможет
        // быстро посчитать HowManyStartsWithPrefix(element)
        if (Contains(element))
        {
            return false;
        }
        var currentNode = root;
        foreach (var letter in element)
        {
            if (!currentNode.Children.ContainsKey(letter))
            {
                currentNode.Children.Add(letter, new TrieNode());
            }
            currentNode = currentNode.Children[letter];
            currentNode.HowManyStartsWith++;
        }
        Size++;
        currentNode.IsTerminal = true;
        return true;
    }
    
    /// <summary>
    /// Determines whether the Trie contains the element.
    /// </summary>
    public bool Contains(string element)
    {
        var currentNode = root;
        foreach (var letter in element)
        {
            if (currentNode.Children.ContainsKey(letter))
            {
                currentNode = currentNode.Children[letter];
            }
            else
            {
                return false;
            }
        }
 
        return currentNode.IsTerminal;
    }
    
    /// <summary>
    /// Removes the element from the Trie 
    /// </summary>
    public bool Remove(string element)
    {
        var trace = new Queue<TrieNode>();
        // наверное, плохо пробегать дважды, но теперь свойство HowManyStartsWith поможет
        // быстро посчитать HowManyStartsWithPrefix(element)
        if (!Contains(element))
        {
            return false;
        }
        var currentNode = root;
        foreach (var letter in element)
        {
            trace.Enqueue(currentNode);
            currentNode = currentNode.Children[letter];
            currentNode.HowManyStartsWith--;
        }
        
        // сравнивается не с 1, а с 0, потому что по пути уменьшали HowManyStartsWith
        if (currentNode.HowManyStartsWith > 0)
        {
            currentNode.IsTerminal = false;
        }
        else
        {
            while (trace.Peek().HowManyStartsWith > 0)
            {
                trace.Peek().HowManyStartsWith--;
                trace.Dequeue();
            }
            
            trace.Peek().Children.Clear();
        }
        
        trace.Clear();
        Size--;
        return true;
    }
    
    /// <summary>
    /// Returns the number of words starting with the prefix
    /// </summary>
    public int HowManyStartsWithPrefix(string prefix)
    {
        var currentNode = root;
        foreach (var letter in prefix)
        {
            if (!currentNode.Children.ContainsKey(letter))
            {
                return 0;
            }
            currentNode = currentNode.Children[letter];
        }
 
        return currentNode.HowManyStartsWith;
    }
}