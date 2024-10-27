using System;

namespace src.domain;

public class Transaction
{
    private readonly UUID _transactionId;
    private readonly UUID _rideId;
    private readonly decimal _amount;
    private readonly DateTime _date;
    private string _status;

    public Transaction(string transactionId, string rideId, decimal amount, DateTime date, string status)
    {
        _transactionId = new UUID(transactionId);
        _rideId = new UUID(rideId);
    }

    public static Transaction create(string rideId, decimal amount)
    {
        var transactionId = UUID.create();
        var date = new DateTime();
        var status = "waiting_payment";
        return new Transaction(transactionId.getValue(), rideId, amount, date, status);
    }

    public void pay()
    {
        _status = "paid";
    }

    public string getStatus()
    {
        return _status;
    }
}
