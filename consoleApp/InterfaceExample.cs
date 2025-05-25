using System;

namespace InterfaceExample;

public interface IPaymentGateway
{
    bool ProcessPayment(decimal amount);
}

public class PayPalGateway : IPaymentGateway
{
    public bool ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing payment of {amount:C} through PayPal.");
        return true;
    }
}

public class StripeGateway : IPaymentGateway
{
    public bool ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing payment of {amount:C} through Stripe.");
        return true;
    }
}

public class ShoppingCard
{
    private readonly IPaymentGateway _paymentGateway;

    public ShoppingCard(IPaymentGateway paymentGateway)
    {
        _paymentGateway = paymentGateway;
    }

    public void Checkout(decimal amount)
    {
        if (_paymentGateway.ProcessPayment(amount))
        {
            Console.WriteLine("Payment successful!");
        }
        else
        {
            Console.WriteLine("Payment failed.");
        }
    }
}