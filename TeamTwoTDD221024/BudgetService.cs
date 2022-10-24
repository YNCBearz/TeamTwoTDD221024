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
        var startedBudgets = budgets.FirstOrDefault(x => x.YearMonth == startedYearMonth);
        var endedBudgets = budgets.FirstOrDefault(x => x.YearMonth == endedYearMonth);
        var daysInStartedMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
        var daysInEndedMonth = DateTime.DaysInMonth(endDate.Year, endDate.Month);

        if (startedYearMonth == endedYearMonth)
        {
            Console.WriteLine(daysInStartedMonth);
            var selectedDays = (endDate - startDate).Days + 1;
            return startedBudgets.Amount * selectedDays / daysInStartedMonth;
        }

        var startedBudgetsAmount = (daysInStartedMonth - startDate.Day + 1) * startedBudgets.Amount / daysInStartedMonth;
        var endedBudgetsAmount = endDate.Day * endedBudgets.Amount / daysInEndedMonth;
        return startedBudgetsAmount + endedBudgetsAmount;
    }
}