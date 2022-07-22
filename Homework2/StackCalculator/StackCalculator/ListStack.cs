namespace StackCalculator;

/// <summary>
/// Implements stack using linked list.
/// </summary>
public class ListStack : IStack
{
    private class StackElement
    {
        public double Value { get; }
        public StackElement? Next { get; set; }

        public StackElement(double number)
        {
            Value = number;
        }
    }

    private StackElement? _head;
    
    public void Push(double number)
    {
        _head = new StackElement(number)
        {
            Next = _head
        };
    }

    public double Pop()
    {
        if (_head == null)
        {
            throw new InvalidOperationException("Tried to remove element from empty stack");
        }
        var value = _head?.Value ?? 0;
        _head = _head.Next;
        return value;
    }
}