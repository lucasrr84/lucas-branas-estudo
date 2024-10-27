using System;

namespace src.infra.gateway;

public interface IPaymentGateway
{
    Task<OutputPaymentGateway> createTransaction(InputPaymentGateway input);
}

public class InputPaymentGateway()
{
	public string cardHolder;
	public string creditCardNumber;
	public string expDate;
	public string cvv;
	public decimal amount;
}

public class OutputPaymentGateway
{
	public string tid;
	public string authorizationCode;
	public string status;
}
