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

        if (endDate < startDate)
        {
            return 0;
        }

        var daysInStartedMonth = GetDaysInMonth(startDate);
        var daysInEndedMonth = GetDaysInMonth(endDate);

        var startedBudgetAmount = GetBudgetAmount(startDate);
        var endedBudgetAmount = GetBudgetAmount(endDate);

        if (IsSameMonth(startDate, endDate))
        {
            var selectedDays = (endDate - startDate).Days + 1;
            return startedBudgetAmount * selectedDays / daysInStartedMonth;
        }

        var startedBudgetsAmount = (daysInStartedMonth - startDate.Day + 1) * startedBudgetAmount / daysInStartedMonth;
        var endedBudgetsAmount = endDate.Day * endedBudgetAmount / daysInEndedMonth;

        var middleMonthsAmount = 0;

        var middleMonth = startDate.AddMonths(1);

        while (middleMonth <= endDate)
        {
            var month = middleMonth.ToString("yyyyMM");
            middleMonthsAmount += _budgetRepo.GetAll().FirstOrDefault(x => x.YearMonth == month).Amount;
            middleMonth = middleMonth.AddMonths(1);
        }

        return startedBudgetsAmount + endedBudgetsAmount + middleMonthsAmount;
    }

    private int GetBudgetAmount(DateTime yearMonth)
    {
        return _budgetRepo.GetAll().FirstOrDefault(x => x.YearMonth == yearMonth.ToString("yyyyMM"))?.Amount ?? 0;
    }

    private static bool IsSameMonth(DateTime startedDate, DateTime endedDate)
    {
        return startedDate.Month == endedDate.Month;
    }

    private static int GetDaysInMonth(DateTime date)
    {
        return DateTime.DaysInMonth(date.Year, date.Month);
    }
}