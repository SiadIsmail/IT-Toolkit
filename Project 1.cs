int x = 1;
while (x
    == 1)
{
    Console.WriteLine("Give me a number:");
    int number = int.Parse(Console.ReadLine());

    Console.WriteLine("Give me a second number:");
    int secondNumber = int.Parse(Console.ReadLine());

    int sum = number + secondNumber;
    Console.WriteLine("The sum is: " + sum);

    Console.WriteLine("Want to try again? (yes/no)");
    string answer = Console.ReadLine();
    if (answer.ToLower() == "yes")
    {
        continue; // Restart the loop
    }
    else
    {
        Console.WriteLine("Goodbye!");
        break; // Exit the loop and end the program
    }
}