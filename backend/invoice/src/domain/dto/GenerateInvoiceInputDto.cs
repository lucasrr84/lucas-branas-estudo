using System;

namespace invoice.src.domain.dto;

public class GenerateInvoiceInputDto
{
    public string rideId { get; set; } = "";
    public double amount { get; set; } = 0;
}
