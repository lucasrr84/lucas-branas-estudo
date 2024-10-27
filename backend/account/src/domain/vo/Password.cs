using System.Security.Cryptography;
using System.Text;

namespace account.src.domain.vo;

public abstract class Password
{
    protected string? _value;
    public abstract string type { get; }

    public Password(string value, string operation)
    {
        if (value.Length < 6) throw new Exception("Invalid password");
        if (operation == "create") _value = encrypt(value);
        if (operation == "restore") _value = value;
    }

    public string? getValue()
    {
        return _value;
    }

    public abstract string encrypt(string value);
    public abstract bool isValid(string value);
}

public class TextPlainPassword : Password
{
    public override string type => "textplain";

    public TextPlainPassword(string value, string operation) : base(value, operation) { }   

    public override string encrypt(string value)
    {
        return value;
    }

    public override bool isValid(string value)
    {
        return _value == value;
    }
}

public class MD5Password : Password
{
    public override string type => "md5";

    public MD5Password(string value, string operation) : base(value, operation) { }   

    public override string encrypt(string value)
    {
        using (var md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(value);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            foreach (var b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }

    public override bool isValid(string value)
    {
        return _value == encrypt(value);
    }
}

public class SHA1Password : Password
{
    public override string type => "sha1";

    public SHA1Password(string value, string operation) : base(value, operation) { }

    public override string encrypt(string value)
    {
        using (var sha1 = SHA1.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(value);
            byte[] hashBytes = sha1.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            foreach (var b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }

    public override bool isValid(string value)
    {
        return _value == encrypt(value);
    }
}

public static class PasswordFactory
{
    public static Password build(string type, string password, string operation)
    {
        if (type == "textplain") return new TextPlainPassword(password, operation);
        if (type == "md5") return new MD5Password(password, operation);
        if (type == "sha1") return new SHA1Password(password, operation);
        throw new ArgumentException("Invalid password type");
    }

    public static Password create(string type, string password)
    {
        return build(type, password, "create");
    }

    public static Password restore(string type, string password)
    {
        return build(type, password, "restore");
    }
}
