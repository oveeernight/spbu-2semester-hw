namespace ParseTree;

/// <summary>
/// Represents arithmetic operation
/// </summary>
public class Operation
{
    public Operation? LeftOperation { get; set; }
    public Operation? RightOperation { get; set; }
    public Operation? OperationAbove { get; set; }
    public int Result { get; protected set; }

    public Operation(int number)
    {
        Result = number;
    }

    protected Operation()
    {
        
    }
    
    /// <summary>
    /// Writes the result of application result plus to result of left and right operations
    /// </summary>
    public virtual void Calculate()
    {
        
    }
}
