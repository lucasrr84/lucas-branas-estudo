using System;

namespace ride.src.domain.vo;

public class Cpf
{
    const int CPF_LENGTH = 11;
    const int FACTOR_FIRST_DIGIT = 10;
    const int FACTOR_SECOND_DIGIT = 11;
    private readonly string _value;
    
    public Cpf (string value) {
		if (!this.validate(value)) throw new Exception("Invalid cpf");
		_value = value;
	}

    public bool validate(string cpf) 
    {   
        if (String.IsNullOrEmpty(cpf)) return false;
        cpf = RemoveNonDigits(cpf);
        if (cpf.Length != CPF_LENGTH) return false;
        if (AllDigitsTheSame(cpf)) return false;
        int digit1 = CalculateDigit(cpf, FACTOR_FIRST_DIGIT);
        int digit2 = CalculateDigit(cpf, FACTOR_SECOND_DIGIT);
        return ExtractActualDigit(cpf) == $"{digit1}{digit2}";
    }

    private string RemoveNonDigits(string cpf) 
    {
        return cpf.Replace(".", "").Replace("-", "");
    }

    private bool AllDigitsTheSame(string cpf) 
    {
        return cpf.All(digit => digit == cpf[0]);
    }

    private int CalculateDigit(string cpf, int factor) 
    {
        int total = 0;

        for (int i = 0; i < cpf.Length; i++) {
            if (factor > 1) 
            {
                total += Int32.Parse(cpf[i].ToString()) * factor--;
            }
        }
        
        int remainder = total % 11;
        return (remainder < 2) ? 0 : 11 - remainder;
    }

    private string ExtractActualDigit(string cpf) 
    {
        return cpf[9..];
    }

    public string getValue () {
		return _value;
	}
}
