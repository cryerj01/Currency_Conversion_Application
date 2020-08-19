using System;

public class Currency
{
    public Currency()
    {
        CurrencyName = "Unkown";
        CurrencyValue = 0;
        CurrencySysmble = "Unkown"
    }
	public Currency(string name, double Value, string symble)
    {
        CurrencyName = name;
        CurrencyValue = Value;
        CurrencySysmble = symble
    }
    
    public string CurrencyName { get; set; }
    public double CurrencyValue { get; set; }
    public string CurrencySysmble { get; set; }

    public void storeCurrency()
    {

    }
   

}


