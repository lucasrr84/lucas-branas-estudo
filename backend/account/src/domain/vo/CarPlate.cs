using System.Text.RegularExpressions;

namespace account.src.domain.vo;

public class CarPlate
{
    private readonly string _value;

	public CarPlate(string value)
    {
        if (!Regex.IsMatch(value, @"^[A-Z]{3}[0-9]{4}$")) throw new Exception("Invalid car plate");
		_value = value;
    }

	public string getValue() {
		return _value;
	}
}
