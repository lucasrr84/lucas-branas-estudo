using account.src.domain.vo;

namespace account.src.domain.entity;

public class Account
{
    private readonly UUID _accountId;
    private readonly Name _name;
    private readonly Email _email;
    private readonly Cpf _cpf;
    private readonly CarPlate? _carPlate;
    private readonly Password _password;
    private readonly bool _isPassenger;
    private readonly bool _isDriver;

    public Account(string accountId, string name, string email, string cpf, string carPlate, string password, bool isPassenger, bool isDriver, string passwordType = "textplain")
    {
        _accountId = new UUID(accountId);
        _name = new Name(name);
        _email = new Email(email);
        _cpf = new Cpf(cpf);
        if (isDriver) _carPlate = new CarPlate(carPlate);
        _password = PasswordFactory.restore(passwordType, password);
        _isDriver = isDriver;
        _isPassenger = isPassenger;
    }

    public static Account create(string name, string email, string cpf, string carPlate, string password, bool isPassenger, bool isDriver, string passwordType = "textplain")
    {
        var accountId = UUID.create();
        var passwordValue = PasswordFactory.create(passwordType, password);
        return new Account(accountId.getValue(), name, email, cpf, carPlate, passwordValue.getValue()!, isPassenger, isDriver, passwordValue.type);
    }

    public string getAccountId () {
		return _accountId.getValue();
	}

	public string getName () {
		return _name.getValue();
	}

	public string getEmail () {
		return _email.getValue();
	}

	public string getCpf () {
		return _cpf.getValue();
	}

	public string getCarPlate () {
        if (_carPlate == null) return "";
        else if (String.IsNullOrEmpty(_carPlate.getValue())) return "";
        return _carPlate.getValue();
	}

	public string? getPassword () {
		return _password.getValue();
	}

	public string getPasswordType () {
		return _password.type;
	}

    public bool getIsPassenger()
    {
        return _isPassenger;
    }

    public bool getIsDriver()
    {
        return _isDriver;
    }
}
