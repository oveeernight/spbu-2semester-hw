using System.Collections;

namespace UniqueList;

/// <summary>
/// Represents list with add, remove and indexer
/// </summary>
public class List
{
    private int position;
    private int[] array;

    public int Count => position;

    public List()
    {
        array = new int[2];
        position = 0;
    }

    public virtual int this[int index]
    {
        get
        {
            if (index >= Count || index < 0) throw new IndexOutOfRangeException();
            return array[index];
        }
        set
        {
            if (index >= Count || index < 0) throw new IndexOutOfRangeException();
            array[index] = value;
        }
    }
    
    /// <summary>
    /// Adds the number to the list.
    /// </summary>
    public virtual void Add(int number)
    {
        if (position >= array.Length)
        {
            var newArray = new int[array.Length * 2];
            array.CopyTo(newArray, 0);
            array = newArray;
        }

        array[position] = number;
        position++;
    }
    
    /// <summary>
    /// Removes first occurence of the number in list.
    /// </summary>
    public virtual void Remove(int number)
    {
        var findNumber = false;
        for (var i = 0; i < position; i++)
        {
            if (array[i] == number)
            {
                findNumber = true;
            }

            if (findNumber)
            {
                (array[i], array[i + 1]) = (array[i + 1], array[i]);
            }
        }

        array[position] = 0;
        position--;
    }

    protected bool Contains(int number) => Array[..position].Any(item => item == number);
}