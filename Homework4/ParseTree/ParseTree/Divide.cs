namespace ParseTree;

/// <summary>
/// Represents divide arithmetic operation
/// </summary>
public class Divide : Operation
{
    public override void Calculate()
    {
        if (LeftOperation != null && RightOperation != null)
        {
            Result = LeftOperation.Result / RightOperation.Result;
        }
    }
}