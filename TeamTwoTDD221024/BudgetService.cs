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

        var startedYearMonth = startDate.ToString("yyyyMM");
        var endedYearMonth = endDate.ToString("yyyyMM");
        var budget = budgets.FirstOrDefault(x => x.YearMonth == startedYearMonth);
        if (startedYearMonth==endedYearMonth)
        {
            var daysInMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
            Console.WriteLine(daysInMonth);
            var selectedDays = (endDate-startDate).Days + 1;
            return budget.Amount * selectedDays / daysInMonth;
        }
         
        return budget.Amount;
    }
}