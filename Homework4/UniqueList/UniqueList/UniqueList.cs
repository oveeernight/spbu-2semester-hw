namespace UniqueList;

/// <summary>
/// Represents a list where values are unique.
/// </summary>
public class UniqueList : List
{
    /// <summary>
    /// Adds the number to the Unique List.
    /// </summary>
    public override void Add(int number)
    {
        if (Contains(number))
        {
            throw new AddingExistingNumberException();
        }
        base.Add(number);
    }
    
    /// <summary>
    /// Removes the number from the Unique List.
    /// </summary>
    public override void Remove(int number)
    {
        if (!Contains(number))
        {
            throw new RemovingNonExistingNumberException();
        }
        base.Remove(number);
    }

    public override int this[int index]
    {
        get => base[index];
        set
        {
            if (index >= Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            if (Contains(value) && value != base[index])
            {
                throw new AddingExistingNumberException();
            }
            base[index] = value;
        }
    }
}