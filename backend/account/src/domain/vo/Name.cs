using System.Text.RegularExpressions;

namespace account.src.domain.vo;

public class Name
{
    private readonly string _value;

	public Name(string value)
    {
        if (!Regex.IsMatch(value, @"^[a-zA-Z]+ [a-zA-Z]+$")) throw new Exception("Invalid name");
		_value = value;
    }

	public string getValue() {
		return _value;
	}
}
