using FluentAssertions;
using NSubstitute;

namespace TeamTwoTDD221024;

public class Tests
{
    private IBudgetRepo? _budgetRepo;
    private BudgetService _budgetService;

    [SetUp]
    public void SetUp()
    {
        _budgetRepo = Substitute.For<IBudgetRepo>();
        _budgetService = new BudgetService(_budgetRepo);
    }

    [Test]
    public void NoBudget_Data()
    {
        GivenBudgets(new List<Budget>());
        var expected = QueryBudgets(new DateTime(), new DateTime());
        expected.Should().Be(0);
    }

    [Test]
    public void Query_whole_month()
    {
        GivenBudgets(new List<Budget>
        {
            new Budget
            {
                YearMonth = "202210",
                Amount = 3100
            }
        });

        var expected = QueryBudgets(new DateTime(2022, 10, 1), new DateTime(2022, 10, 31));
        expected.Should().Be(3100);
    }

    private decimal QueryBudgets(DateTime startDate, DateTime dateTime)
    {
        var expected = _budgetService.Query(startDate, dateTime);
        return expected;
    }

    private void GivenBudgets(List<Budget> budgets)
    {
        _budgetRepo.GetAll().Returns(budgets);
    }
}