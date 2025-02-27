namespace eSusInsurers.Helpers;

public class RandomOTP
{
    public static string CreateRandomOTP()
    {
        var _allowedChars = "0123456789";
        var randomNum = new Random();
        var chars = new char[4];
        for (var i = 0; i < 4; i++) chars[i] = _allowedChars[(int)(_allowedChars.Length * randomNum.NextDouble())];
        return new string(chars);
    }
}