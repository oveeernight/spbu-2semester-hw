namespace StackCalculator;

/// <summary>
/// Represents a calculator that works with reverse polish notation.
/// </summary>
public class StackCalculator
{
    private IStack _stack;
    
    public StackCalculator(IStack stack)
    {
        _stack = stack;
    }
    
    public double Calculate(string expression)
    {
        var input = expression.Split();
        double result = 0;
        foreach (var element in input)
        {
            if (int.TryParse(element, out var number))
            {
                _stack.Push(number);
            }
            else
            {
                try
                {
                    // тут по идее бросится исключение и поймается в мейне
                    var storedNumber = _stack.Pop();
                    var inputNumber = _stack.Pop();
                    if (element == "/" && Math.Abs(inputNumber) < Math.Pow(10, -9))
                    {
                        throw new DivideByZeroException();
                    }

                    result = element switch
                    {
                        "+" => storedNumber + inputNumber,
                        "-" => storedNumber - inputNumber,
                        "*" => storedNumber * inputNumber,
                        "/" => storedNumber / inputNumber,
                        _ => throw new InvalidOperationException()
                    };
                    _stack.Push(result);
                }
                catch (InvalidOperationException e)
                {
                    if (e.Message.Contains("stack"))
                    {
                        throw new InvalidOperationException(
                            "The number of operands must be greater by 1 than amount of operations", e);
                    }

                    throw;
                }
            }
            
        }

        _stack.Pop();
        return result;
    }
}