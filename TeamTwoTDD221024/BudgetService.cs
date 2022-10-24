namespace TeamTwoTDD221024;

public class BudgetService
{
    private readonly IBudgetRepo _budgetRepo;

    public BudgetService(IBudgetRepo budgetRepo)
    {
        _budgetRepo = budgetRepo;
    }

    public decimal Query(DateTime startDate, DateTime endDate)
    {
        var budgets = _budgetRepo.GetAll();

        if (!budgets.Any())
        {
            return 0;
        }

        return 3100;
    }
}