using System;
using System.Text.RegularExpressions;

namespace ride.src.domain.vo;

public class Email
{
    private readonly string _value;

	public Email(string value)
    {
        if (!Regex.IsMatch(value, @"^(.+)@(.+)$")) throw new Exception("Invalid email");
		_value = value;
    }

	public string getValue() {
		return _value;
	}
}
