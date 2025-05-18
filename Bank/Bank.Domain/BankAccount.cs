public class BankAccount
{
    public const string DebitAmountExceedsBalanceMessage = "Debit amount exceeds balance";
    public const string DebitAmountLessThanZeroMessage = "Debit amount is less than zero";
    public const string CreditAmountLessThanZeroMessage = "Credit amount is less than zero";

    private readonly string m_customerName;
    private double m_balance;

    public BankAccount(string customerName, double balance)
    {
        m_customerName = customerName;
        m_balance = balance;
    }

    public string CustomerName { get { return m_customerName; } }
    public double Balance { get { return m_balance; } }

    public void Debit(double amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), amount, DebitAmountLessThanZeroMessage);

        if (amount > m_balance)
            throw new ArgumentOutOfRangeException(nameof(amount), amount, DebitAmountExceedsBalanceMessage);

        m_balance -= amount;
    }

    public void Credit(double amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), amount, CreditAmountLessThanZeroMessage);

        m_balance += amount;
    }
}