using System.Text;

namespace Empo.Shared.Utility.PasswordGenerator;

public static class RandomPasswordGenerator
{
    public static async Task<string> RandomPassword(int size = 0)
    {
        string _char = "!@#$%&*";
        Random random = new Random();
        int num = random.Next(0, _char.Length);
        var specialChar = _char[num];
        StringBuilder builder = new StringBuilder();
        builder.Append(RandomStringGenerator.RandomString(4, true));
        builder.Append(RandomNumberGenerator.RandomNumber(1000, 9999));
        builder.Append(RandomStringGenerator.RandomString(2, false));
        var password = builder.ToString();
        return password + specialChar;
    }
}
