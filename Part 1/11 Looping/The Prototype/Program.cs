Console.Write("User 1, enter a number between 0 and 100: ");
int target_number = Convert.ToInt32(Console.ReadLine());
while (target_number < 0 || target_number > 100)
{
    Console.Write("Sorry, the number must be between 0 and 100: ");
    target_number = Convert.ToInt32(Console.ReadLine());
}

Console.Clear();

Console.WriteLine("User 2, guess the number.");
int guess;
while (true)
{
    Console.Write("What is your next guess? ");
    guess = Convert.ToInt32(Console.ReadLine());
    if (guess == target_number)
        break;
    Console.WriteLine($"{guess} is too {(guess > target_number ? "high" : "low")}.");
}
Console.WriteLine("You guessed the number!");