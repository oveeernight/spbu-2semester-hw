namespace Routers;

/// <summary>
/// Represents max heap
/// </summary>
public class Heap<T> where T : IComparable<T>
{
    private List<T> _list = new ();

    public int Count => _list.Count;

    /// <summary>
    /// Returns max item in heap and deletes it
    /// </summary>
    public T Extract()
    {
        if (_list.Count == 0)
        {
            throw new InvalidOperationException("The heap was empty");
        }
        var result = _list[0];
        (_list[0], _list[^1]) = (_list[^1], _list[0]);
        _list.RemoveAt(_list.Count - 1);
        BuildHeap(0);
        return result;
    }

    /// <summary>
    /// Inserts item in heap
    /// </summary>
    public void Insert(T item)
    {
        _list.Add(item);
        CorrectNewItemPosition();
    }

    private void BuildHeap(int index)
    {
        while (true)
        {
            var largest = index;

            if (2 * index + 1 < _list.Count &&  _list[2 * index + 1].CompareTo(_list[largest]) > 0)
            {
                largest = 2 * index + 1;
            }
            if (2 * index + 2 < _list.Count && _list[2 * index + 2].CompareTo(_list[largest]) > 0)
            {
                largest = 2 * index + 2;
            }

            if (_list.Count == 0 || _list[largest].CompareTo(_list[index]) == 0)
            {
                return;
            }

            (_list[index], _list[largest]) = (_list[largest], _list[index]);
            index = largest;
        }
    }

    private void CorrectNewItemPosition()
    {
        var i = _list.Count - 1;
        while (i > 0)
        {
            if (_list[i].CompareTo(_list[i / 2]) > 0)
            {
                (_list[i], _list[i / 2]) = (_list[i / 2], _list[i]);
            }
            else
            {
                return;
            }
            i /= 2;
        }
    }
}