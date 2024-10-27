
namespace account.src.domain.vo;

public class UUID
{
    private readonly string _value;

    public UUID(string value)
    {
        _value = value;
    }

    public static UUID create()
    {
        var uuid = Guid.NewGuid().ToString();
        return new UUID(uuid);
    }

    public string getValue()
    {
        return _value;
    }

}
