public class BankAccount
{
    private double _balance;

    public double Balance => _balance;

    public bool IsOverdrawn => _balance < 0;

    public void Deposit(double amount)
    {
        if (amount <= 0)
            return;

        _balance += amount;
    }

    public void Withdraw(double amount)
    {
        if (amount <= 0)
            return;

        _balance -= amount;
    }
}
