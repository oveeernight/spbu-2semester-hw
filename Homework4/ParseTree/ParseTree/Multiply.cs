namespace ParseTree;

/// <summary>
/// Represents multiply arithmetic operation
/// </summary>
public class Multiply : Operation
{
    public override void Calculate()
    {
        if (LeftOperation != null && RightOperation != null)
        {
            Result = LeftOperation.Result * RightOperation.Result;
        }
    }
}