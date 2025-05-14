using Empo.BuildingBlocks.Domain.Interfaces;

namespace Empo.BuildingBlocks.Domain.Rules;

public class EmailAddressUniqueRule :IBusinessRule
{
    private readonly bool _isExist;
    public EmailAddressUniqueRule(bool isExist)
    {
            _isExist = isExist;
    }
    public string Message => "Email address already exists";

    public bool IsBroken()
    {
        if (_isExist)
        {
            return true;
        }
        return false;
    }
}
