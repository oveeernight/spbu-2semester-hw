namespace ParseTree;

/// <summary>
/// Represents minus arithmetic operation
/// </summary>
public class Minus : Operation
{
    public override void Calculate()
    {
        if (LeftOperation != null && RightOperation != null)
        {
            Result = LeftOperation.Result - RightOperation.Result;
        }
    }
}