namespace SkipList;

using System.Collections;

/// <summary>
/// Represents ordered linked list with skips
/// </summary>
public class SkipList<T> : IList<T> where T : IComparable, new ()
{
    /// <summary>
    /// Represents a node of SkipList<T>
    /// </summary>
    public class SkipListNode
    {
        public T Key { get; set; }
        public SkipListNode? Next { get; set; }
        public SkipListNode? Previous { get; set; }
        public SkipListNode? NextLower { get; set; }
        public SkipListNode? NextHigher { get; set; }

        public SkipListNode(T key)
        {
            Key = key;
        }

        public SkipListNode()
        {
            Key = new T();
        }
    }

    private class PositiveInfinitySkipListNode : SkipListNode
    {
    }

    private class NegativeInfinitySkipListNode : SkipListNode
    {
    }

    private SkipListNode _sentinel;
    private SkipListNode? _sentinelZeroLvl;
    private int _maxNodeLvl;
    private static Random _random = new ();

    public int Count { get; private set; }
    public bool IsReadOnly => false;

    public SkipList()
    {
        _maxNodeLvl = -1;
        Count = 0;
        _sentinel = GetNewLvl();
        _sentinelZeroLvl = _sentinel;
    }

    private SkipListNode GetNewLvl()
    {
        _maxNodeLvl++;
        var negativeInfinity = new NegativeInfinitySkipListNode();
        var positiveInfinity = new PositiveInfinitySkipListNode();
        negativeInfinity.Next = positiveInfinity;
        positiveInfinity.Previous = negativeInfinity;
        return negativeInfinity;
    }

    private static int GetNodeLvl()
    {
        var result = 0;
        while (_random.NextDouble() >= 0.5)
        {
            result++;
        }

        return result;
    }

    public IEnumerator<T> GetEnumerator() => new SkipListEnumerator(this);
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Adds the key to SkipList
    /// </summary>
    public void Add(T key)
    {
        var nodeLvl = GetNodeLvl();
        var newLvlCount = nodeLvl - _maxNodeLvl;
        Count++;
        while (newLvlCount > 0)
        {
            var newLvlNode = GetNewLvl();
            newLvlNode.NextLower = _sentinel;
            _sentinel.NextHigher = newLvlNode;
            _sentinel = newLvlNode;
            newLvlCount--;
        }

        var current = _sentinel;
        var currentLvl = _maxNodeLvl;
        var previousUpperLvl = new SkipListNode();
        while (currentLvl >= 0)
        {
            while (current!.Next is not PositiveInfinitySkipListNode && key.CompareTo(current.Next!.Key) > 0)
            {
                current = current.Next;
            }

            var newNode = new SkipListNode(key);
            if (currentLvl == nodeLvl)
            {
                previousUpperLvl = newNode;
            }

            if (currentLvl <= nodeLvl)
            {
                newNode.Previous = current;
                newNode.Next = current.Next;
                current.Next.Previous = newNode;
                current.Next = newNode;
                newNode.NextHigher = currentLvl == nodeLvl ? null : previousUpperLvl;
                previousUpperLvl.NextLower = newNode;
                previousUpperLvl = newNode;
            }

            current = currentLvl == 0 ? current : current.NextLower;

            currentLvl--;
        }
    }

    /// <summary>
    /// Removes all items from SkipList
    /// </summary>
    public void Clear()
    {
        _maxNodeLvl = -1;
        _sentinel = GetNewLvl();
        _sentinelZeroLvl = null;
        Count = 0;
    }

    /// <summary>
    /// Determines whether the specific key contains in SkipList
    /// </summary>
    public bool Contains(T key)
    {
        var current = _sentinel;
        var currentLvl = _maxNodeLvl;
        while (currentLvl >= 0)
        {
            while (current!.Next is not PositiveInfinitySkipListNode && key.CompareTo(current.Next!.Key) >= 0)
            {
                if (key.CompareTo(current.Next.Key) == 0) return true;
                current = current.Next;
            }

            current = currentLvl == 0 ? current : current.NextLower;
            currentLvl--;
        }

        if (current!.Next is PositiveInfinitySkipListNode)
        {
            return false;
        }

        return key.CompareTo(current.Next!.Key) == 0;
    }

    /// <summary>
    /// Copies the elements of SkipList to the array starting from specific index
    /// </summary>
    /// <exception cref="IndexOutOfRangeException">Size of SkipList is greater than amount of array cells</exception>
    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array.Length - arrayIndex > Count || arrayIndex < 0)
        {
            throw new IndexOutOfRangeException();
        }

        var i = arrayIndex;
        foreach (var key in this)
        {
            array[i] = key;
            i++;
        }
    }

    private void ClearEmptyLvl()
    {
        var current = _sentinel;
        while (_maxNodeLvl >= 0 && current.Next is PositiveInfinitySkipListNode)
        {
            _maxNodeLvl--;
            var newSentinel = current.NextLower;
            current.Next.Previous = null;
            if (_maxNodeLvl > 0)
            {
                current.NextLower!.NextHigher = null;
                if (newSentinel != null)
                {
                    _sentinel = newSentinel;
                }
            }

            current = _sentinel;
        }
    }

    /// <summary>
    /// Removes the first occurence of specific key from SkipList
    /// </summary>
    public bool Remove(T key)
    {
        var current = _sentinel;
        var currentLvl = _maxNodeLvl;
        var requiredKeyExisted = false;
        while (currentLvl >= 0)
        {
            while (current!.Next is not PositiveInfinitySkipListNode && key.CompareTo(current.Next!.Key) >= 0)
            {
                current = current.Next;
            }

            if (key.CompareTo(current.Key) == 0)
            {
                var nodeLower = current.NextLower;
                current.Previous!.Next = current.Next;
                current.Next.Previous = current.Previous;
                if (currentLvl > 0)
                {
                    current.NextLower!.NextHigher = null;
                }

                current = nodeLower;
                requiredKeyExisted = true;
            }
            else
            {
                current = current.NextLower;
            }

            currentLvl--;
        }

        ClearEmptyLvl();
        if (requiredKeyExisted)
        {
            Count--;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Determines the index of the specific key in SkipList
    /// </summary>
    public int IndexOf(T key)
    {
        var index = 0;
        // если я правильно понял, то нет возможности по верхним уровням искать key, потому что придётся у Node
        // хранить индекс, и при каждом добавлении и удалении изменять все индексы, и эти операции станут работать за O(n)
        foreach (var value in this)
        {
            if (value.CompareTo(key) == 0) return index;
            index++;
        }

        return -1;
    }

    public void Insert(int index, T key)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Removes the element with specified index from SkipList
    /// </summary>
    /// <exception cref="IndexOutOfRangeException"> Specified index does not exist in SkipList</exception>
    public void RemoveAt(int index)
    {
        if (index > Count || index < 0)
        {
            throw new IndexOutOfRangeException();
        }

        Count--;

        var current = _sentinelZeroLvl;
        for (var i = -1; i < index; i++)
        {
            current = current!.Next;
        }

        var nextHigher = current!.NextHigher;
        if (nextHigher != null)
        {
            nextHigher.NextLower = null;
        }

        current.Previous!.Next = current.Next;
        current.Next!.Previous = current.Previous;
        current = nextHigher;
        while (current != null)
        {
            nextHigher = current.NextHigher;
            if (nextHigher != null)
            {
                nextHigher.NextLower = null;
            }

            current.Previous!.Next = current.Next;
            current.Next!.Previous = current.Previous;
            current = nextHigher;
        }

        ClearEmptyLvl();
    }

    public T this[int index]
    {
        get
        {
            if (index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            var current = _sentinelZeroLvl;
            for (var i = -1; i < index; i++)
            {
                current = current!.Next;
                if (current == null) throw new IndexOutOfRangeException();
            }

            return current!.Key;
        }
        set => throw new NotSupportedException();
    }

    private class SkipListEnumerator : IEnumerator<T> 
    {
        private SkipListNode? _current;

        public SkipListEnumerator(SkipList<T> list)
        {
            _current = list._sentinelZeroLvl;
        }

        public bool MoveNext()
        {
            _current = _current!.Next;
            return _current!.Next != null;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        object IEnumerator.Current => Current;

        public T Current => _current!.Key;

        public void Dispose()
        {

        }
    }
}