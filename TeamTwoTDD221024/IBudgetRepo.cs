namespace TeamTwoTDD221024;

public interface IBudgetRepo
{
    List<Budget> GetAll();
}

public class Budget
{
    public string YearMonth { get; set; }
    public int Amount { get; set; }
}

class BudgetRepo : IBudgetRepo
{
    public List<Budget> GetAll()
    {
        throw new NotImplementedException();
    }
}