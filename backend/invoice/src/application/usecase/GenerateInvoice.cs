using invoice.src.domain.dto;

namespace invoice.src.application.usecase;

public class GenerateInvoice
{
    public async Task execute(GenerateInvoiceInputDto input)
    {
        Console.WriteLine($"Gerando invoice... rideId = {input.rideId} - amount = {input.amount}");
    }
}