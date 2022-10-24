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
    public void No_budget_data()
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

    [Test]
    public void Query_one_day()
    {
        GivenBudgets(new List<Budget>
        {
            new Budget
            {
                YearMonth = "202210",
                Amount = 3100
            }
        });

        var expected = QueryBudgets(new DateTime(2022, 10, 1), new DateTime(2022, 10, 1));
        expected.Should().Be(100);
    }

    [Test]
    public void Query_multiday()
    {
        GivenBudgets(new List<Budget>
        {
            new Budget
            {
                YearMonth = "202210",
                Amount = 3100
            }
        });

        var expected = QueryBudgets(new DateTime(2022, 10, 1), new DateTime(2022, 10, 3));
        expected.Should().Be(300);
    }

    [Test]
    public void Query_cross_month()
    {
        GivenBudgets(new List<Budget>
        {
            new Budget
            {
                YearMonth = "202210",
                Amount = 3100
            },
            new Budget
            {
                YearMonth = "202211",
                Amount = 30000
            },
        });

        var expected = QueryBudgets(new DateTime(2022, 10, 30), new DateTime(2022, 11, 1));
        expected.Should().Be(1200);
    }
    
    [Test]
    public void Query_cross_multiple_month()
    {
        GivenBudgets(new List<Budget>
        {
            new Budget
            {
                YearMonth = "202210",
                Amount = 3100
            },
            new Budget
            {
                YearMonth = "202211",
                Amount = 30000
            },
            new Budget
            {
                YearMonth = "202212",
                Amount = 31
            },
        });

        var expected = QueryBudgets(new DateTime(2022, 10, 30), new DateTime(2022, 12, 5));
        expected.Should().Be(30205);
    }
    
    [Test]
    [Ignore("not implemented")]
    public void Query_cross_year_multiple_month()
    {
        GivenBudgets(new List<Budget>
        {
            new Budget
            {
                YearMonth = "202210",
                Amount = 3100
            },
            new Budget
            {
                YearMonth = "202211",
                Amount = 30000
            },
            new Budget
            {
                YearMonth = "202212",
                Amount = 31
            },
            new Budget
            {
                YearMonth = "202301",
                Amount = 310
            },
        });

        var expected = QueryBudgets(new DateTime(2022, 10, 30), new DateTime(2023, 01, 5));
        expected.Should().Be(30281);
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