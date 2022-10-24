using FluentAssertions;
using NSubstitute;

namespace TeamTwoTDD221024;

public class Tests
{
    [Test]
    public void NoBudget_Data()
    {
        var budgetRepo = Substitute.For<IBudgetRepo>();
        budgetRepo.GetAll().Returns(new List<Budget>());
        var budgetService = new BudgetService(budgetRepo);

        var expected = budgetService.Query();

        expected.Should().Be(0);
    }
}

public class BudgetService
{
    public BudgetService(IBudgetRepo budgetRepo)
    { }

    public decimal Query()
    {
        return 0;
    }
}