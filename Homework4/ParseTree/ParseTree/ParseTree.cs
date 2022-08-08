namespace ParseTree;

/// <summary>
/// Represents the parse tree of an arithmetic expression.
/// </summary>
public class ParseTree
{
    private readonly Operation _root;

    public int Result => _root.Result;

    public ParseTree(string expression)
    {
        var splitString = expression.Split(new [] { ' ', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
        var queue = new Queue<string>();
        foreach (var s in splitString)
        {
            queue.Enqueue(s);
        }

        _root = GetOperation(queue.Dequeue());
        Build(queue, _root);
        Calculate(_root);
    }

    private void Build(Queue<string> text, Operation currentOperation)
    {
        while (text.Count > 0)
        {
            var item = text.Dequeue();
            if (int.TryParse(item, out var number))
            {
                if (currentOperation.LeftOperation == null)
                {
                    currentOperation.LeftOperation = new Operation(number)
                    {
                        OperationAbove = currentOperation
                    };
                }
                else
                {
                    currentOperation.RightOperation = new Operation(number)
                    {
                        OperationAbove = currentOperation
                    };
                    while (currentOperation.LeftOperation != null && currentOperation.RightOperation != null)
                    {
                        if (currentOperation.OperationAbove == null)
                        {
                            break;
                        }
                        currentOperation = currentOperation.OperationAbove;
                    }
                }
            }
            else
            {
                var newOperation = GetOperation(item);
                newOperation.OperationAbove = currentOperation;
                if (currentOperation.LeftOperation == null)
                {
                    currentOperation.LeftOperation = newOperation;
                }
                else
                {
                    currentOperation.RightOperation = newOperation;
                }

                currentOperation = newOperation;
            }
        }
    }

    private void Calculate(Operation root)
    {
        if (root.LeftOperation != null)
        {
            Calculate(root.LeftOperation);
        }

        if (root.RightOperation != null)
        {
            Calculate(root.RightOperation);
        }

        if (_root.LeftOperation == null && _root.RightOperation == null)
        {
            return;
        }

        root.Calculate();

    }

    private static Operation GetOperation(string symbol)
    {
        return symbol switch
        {
            "*" => new Multiply(),
            "+" => new Plus(),
            "-" => new Minus(),
            "/" => new Divide(),
            _ => throw new InvalidOperationException()
        };
    }
}