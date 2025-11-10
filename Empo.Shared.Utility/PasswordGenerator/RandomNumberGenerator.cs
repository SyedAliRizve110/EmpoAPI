namespace Empo.Shared.Utility.PasswordGenerator;

public static class RandomNumberGenerator
{
    public static int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }
}
