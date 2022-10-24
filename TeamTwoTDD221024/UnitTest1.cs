using NSubstitute;

namespace TeamTwoTDD221024;

public class Tests
{
    [SetUp]
    public void Setup()
    { }

    [Test]
    public void Test1()
    {
        var Instance = Substitute.For<IBudgetRepo>();
        
        Assert.Pass();
    }
}