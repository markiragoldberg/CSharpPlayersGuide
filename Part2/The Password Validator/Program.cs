Console.WriteLine("Passwords must be 6 to 13 characters long, " +
    "contain at least one uppercase letter, " +
    "at least one lowercase letter, " +
    "at least one number, " +
    "and cannot contain 'T' or '&'.");
string? response = null;
while (response == null || response != "quit")
{
    Console.Write("Enter a password to validate, or quit: ");
    response = Console.ReadLine();
    if(response != null && response != "quit")
    {
        string? explanation;
        if(PasswordValidator.IsValidPassword(response, out explanation))
        {
            Console.WriteLine($"\"{response}\" is a valid password.");
        }
        else
        {
            Console.WriteLine($"\"{response}\" is not a valid password, because it {explanation}.");
        }
    }
}

// ------------------------------------

public class PasswordValidator
{
    public static bool IsValidPassword(string? password, out string? reasonIsInvalid)
    {
        if (password == null || password.Length < 6)
        {
            reasonIsInvalid = "is shorter than 6 characters";
            return false;
        }
        else if (password.Length > 13)
        {
            reasonIsInvalid = "is longer than 13 characters";
            return false;
        }
        bool anUpper = false;
        bool aLower = false;
        bool aNumber = false;
        foreach (char glyph in password)
        {
            if (glyph == 'T')
            {
                reasonIsInvalid = "contains 'T'";
                return false;
            }
            else if (glyph == '&')
            {
                reasonIsInvalid = "contains '&'";
                return false;
            }
            if (char.IsUpper(glyph))
                anUpper = true;
            else if (char.IsLower(glyph))
                aLower = true;
            else if (char.IsDigit(glyph))
                aNumber = true;
        }
        if (!anUpper)
        {
            reasonIsInvalid = "does not contain an uppercase letter";
            return false;
        }
        if (!aLower)
        {
            reasonIsInvalid = "does not contain a lowercase letter";
            return false;
        }
        if (!aNumber)
        {
            reasonIsInvalid = "does not contain a number";
            return false;
        }
        reasonIsInvalid = null;
        return true;
    }
}