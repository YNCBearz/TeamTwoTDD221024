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
        var expected = budgetService.Query(new DateTime(), new DateTime());
        expected.Should().Be(0);
    }

    [Test]
    public void Query_whole_month()
    {
        var budgetRepo = Substitute.For<IBudgetRepo>();

        budgetRepo.GetAll().Returns(new List<Budget>
        {
            new Budget
            {
                YearMonth = "202210",
                Amount = 3100
            }
        });

        var budgetService = new BudgetService(budgetRepo);
        var expected = budgetService.Query(new DateTime(2022, 10, 1), new DateTime(2022, 10, 31));
        expected.Should().Be(3100);
    }
}