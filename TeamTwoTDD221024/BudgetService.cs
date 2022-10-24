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
        if (!_budgetRepo.GetAll().Any())
        {
            return 0;
        }

        var startedYearMonth = startDate.ToString("yyyyMM");
        var endedYearMonth = endDate.ToString("yyyyMM");
        var startedBudgets = GetMonthBudget(startedYearMonth);
        var endedBudgets = GetMonthBudget(endedYearMonth);
        var daysInStartedMonth = GetDaysInMonth(startDate);
        var daysInEndedMonth = GetDaysInMonth(endDate);

        if (IsSameMonth(startedYearMonth, endedYearMonth))
        {
            var selectedDays = (endDate - startDate).Days + 1;
            return startedBudgets.Amount * selectedDays / daysInStartedMonth;
        }

        var startedBudgetsAmount = (daysInStartedMonth - startDate.Day + 1) * startedBudgets.Amount / daysInStartedMonth;
        var endedBudgetsAmount = endDate.Day * endedBudgets.Amount / daysInEndedMonth;

        var middleMonthsAmount = 0;

        while (startDate.AddMonths(1).Month <= endDate.AddMonths(-1).Month)
        {
            var month = startDate.AddMonths(1).ToString("yyyyMM");
            middleMonthsAmount += GetMonthBudget(month).Amount;
            startDate = startDate.AddMonths(1);
        }

        return startedBudgetsAmount + endedBudgetsAmount + middleMonthsAmount;
    }

    private static bool IsSameMonth(string startedYearMonth, string endedYearMonth)
    {
        return startedYearMonth == endedYearMonth;
    }

    private static int GetDaysInMonth(DateTime date)
    {
        return DateTime.DaysInMonth(date.Year, date.Month);
    }

    private Budget? GetMonthBudget(string yearMonth)
    {
        return _budgetRepo.GetAll().FirstOrDefault(x => x.YearMonth == yearMonth);
    }
}