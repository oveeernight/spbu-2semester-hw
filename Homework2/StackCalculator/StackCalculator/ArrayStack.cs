namespace StackCalculator;

/// <summary>
/// Implements stack using array.
/// </summary>
public class ArrayStack : IStack
{
    private double[] _stack = new double[2];
    private int _lastIndex = -1;
    
    public void Push(double number)
    {
        _lastIndex++;
        if (_lastIndex >= _stack.Length)
        {
            var newArray = new double[_stack.Length * 2];
            _stack.CopyTo(newArray, 0);
            _stack = newArray;
        }
        _stack[_lastIndex] = number;
    }

    public double Pop()
    {
        if (_lastIndex == -1)
        {
            throw new InvalidOperationException("Tried to remove element from empty stack");
        }

        _lastIndex--;
        return _stack[_lastIndex + 1];
    }
}