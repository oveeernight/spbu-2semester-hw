namespace ParseTree;

/// <summary>
/// Represents plus arithmetic operation
/// </summary>
public class Plus : Operation
{
    public override void Calculate()
    {
        if (LeftOperation != null && RightOperation != null)
        {
            Result = LeftOperation.Result + RightOperation.Result;
        }
    }
}