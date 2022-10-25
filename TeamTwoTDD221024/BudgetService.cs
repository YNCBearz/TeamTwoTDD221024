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

        var startedMonthDailyAmount = GetDailyAmount(startDate);
        var endedMonthDailyAmount = GetDailyAmount(endDate);

        if (IsSameMonth(startDate, endDate))
        {
            var selectedDays = (endDate - startDate).Days + 1;
            return selectedDays * startedMonthDailyAmount;
        }

        var startedBudgetsAmount = (GetDaysInMonth(startDate) - startDate.Day + 1) * startedMonthDailyAmount;
        var endedBudgetsAmount = endDate.Day * endedMonthDailyAmount;

        var middleMonthsAmount = 0;

        var middleMonth = startDate.AddMonths(1);

        while (middleMonth <= endDate)
        {
            middleMonthsAmount += GetBudgetAmount(middleMonth);
            middleMonth = middleMonth.AddMonths(1);
        }

        return startedBudgetsAmount + endedBudgetsAmount + middleMonthsAmount;
    }

    private int GetDailyAmount(DateTime startDate)
    {
        return GetBudgetAmount(startDate) / GetDaysInMonth(startDate);
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