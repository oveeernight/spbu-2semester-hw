namespace StackCalculator;

/// <summary>
/// Represents LIFO object which allows Pop and Push 
/// </summary>
public interface IStack
{
    /// <summary>
    /// Inserts the number to the top of the stack.
    /// </summary>
    void Push(double number);
    
    /// <summary>
    /// Removes and returns the element from the top of the stack.
    /// </summary>
    /// <returns>The number removed from the top of the stack. </returns>
    double Pop();
}