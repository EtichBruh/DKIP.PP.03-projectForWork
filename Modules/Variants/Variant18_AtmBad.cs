namespace ReviewSamples.Modules.Variants;

public class Variant18_Account
{
    public string Number;
    public double Balance;
    public string Pin;
}

public class Variant18_AtmBad
{
    public string Withdraw(Variant18_Account a, string pin, double amount)
    {
        if (a.Pin != pin)
        {
            Console.WriteLine("Wrong PIN: entered " + pin + ", expected " + a.Pin);
            return "bad";
        }

        if (a.Balance < amount)
        {
            return "bad";
        }

        a.Balance = a.Balance - amount;

        File.AppendAllText("atm.log", "Withdraw " + amount + " from " + a.Number + ", PIN=" + pin + "\n");

        return "ok";
    }

    public string Deposit(Variant18_Account a, double amount)
    {
        a.Balance = a.Balance + amount;
        return "ok";
    }

    public double Balance(Variant18_Account a, string pin)
    {
        if (a.Pin != pin)
        {
            return -1;
        }
        return a.Balance;
    }
}
