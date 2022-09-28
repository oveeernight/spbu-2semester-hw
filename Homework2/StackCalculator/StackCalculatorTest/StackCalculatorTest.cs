using System;
using System.Collections.Generic;
using NUnit.Framework;
using StackCalculator;

namespace StackCalculatorTest;

public class Tests
{
    private readonly double delta = Math.Pow(10, -9);
    
    private static IEnumerable<TestCaseData> Calculators
        => new TestCaseData[]
        {
            new(new StackCalculator.StackCalculator(new ArrayStack())), 
            new(new StackCalculator.StackCalculator(new ListStack())),
        };
    

    [TestCaseSource(nameof(Calculators))]
    public void Test_DividingByZero_Should_ThrowException(StackCalculator.StackCalculator calculator)
    {
        Assert.Throws<DivideByZeroException>(() => calculator.Calculate("0 10 /"));
    }
    
    [TestCaseSource(nameof(Calculators))]
    public void Test_TwoNumbersExpression_CalculatedCorrectly(StackCalculator.StackCalculator calculator)
    {
        var result = calculator.Calculate("1 0 +");
        Assert.AreEqual(1, result, delta);
        result = calculator.Calculate("1 1 -");
        Assert.AreEqual(0, result, delta);
        result = calculator.Calculate("2 2 *");
        Assert.AreEqual(4, result, delta);
        result = calculator.Calculate("2 3 /");
        Assert.AreEqual(1.5, result, delta);
    }
    
    [TestCaseSource(nameof(Calculators))]
    public void Test_SeveralNumbersExpression_CalculatedCorrectly(StackCalculator.StackCalculator calculator)
    {
        var result = calculator.Calculate("1 2 3 4 + * -");
        Assert.AreEqual(13, result, delta);
        result = calculator.Calculate("3 5 15 / *");
        Assert.AreEqual(9, result, delta);
    }
    
    [TestCaseSource(nameof(Calculators))]
    public void Test_PopFromEmptyStack_ShouldThrowException(StackCalculator.StackCalculator calculator)
    {
        var ex = Assert.Throws<InvalidOperationException>(() => calculator.Calculate(" 2 2 + +"));
        if (ex != null)
        {
            Assert.That(ex.Message, Is.EqualTo("The number of operands must be greater by 1 than amount of operations"));
        }
    }
}