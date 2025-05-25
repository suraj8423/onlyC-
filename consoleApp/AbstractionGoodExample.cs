using System;
namespace InterfaceExample;

public interface IPaymentMethod
{
    bool ProcessPayment(decimal amount);
}

public class CreditCardPayment : IPaymentMethod
{
    public bool ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing credit card payment of {amount:C}");
        return true;
    }
}

public class PayPalPayment : IPaymentMethod
{
    public bool ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing PayPal payment of {amount:C}");
        return true;
    }
}

public class CheckoutSystem
{
    public void Checkout(IPaymentMethod paymentMethod, decimal amount)
    {
        if (paymentMethod.ProcessPayment(amount))
        {
            Console.WriteLine("Payment successful!");
        }
        else
        {
            Console.WriteLine("Payment failed.");
        }
    }
}