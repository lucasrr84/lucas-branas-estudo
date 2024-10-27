
namespace account.src.domain.dto;

public class AccountDto
{
    public string accountId { get; set; } = "";
    public string name { get; set; } = "";
    public string email { get; set; } = "";
    public string cpf { get; set; } = "";
    public string carPlate { get; set; } = "";
    public string password { get; set; } = "";
    public bool isPassenger { get; set; } = false;
    public bool isDriver { get; set; } = false;
    public string passwordType { get; set; } = "textplain";
}