Console.Write("What is the enemy's x-coordinate, relative to the watchtower? ");
int enemy_x = Convert.ToInt32(Console.ReadLine());

Console.Write("And what is their y-coordinate? ");
int enemy_y = Convert.ToInt32(Console.ReadLine());

if(enemy_x < 0)
{
    if(enemy_y < 0)
    {
        Console.WriteLine("The enemy is to the southwest!");
    }
    else if(enemy_y > 0)
    {
        Console.WriteLine("The enemy is to the northwest!");
    }
    else
    {
        Console.WriteLine("The enemy is to the west!");
    }
}
else if(enemy_x > 0)
{
    if (enemy_y < 0)
    {
        Console.WriteLine("The enemy is to the southeast!");
    }
    else if (enemy_y > 0)
    {
        Console.WriteLine("The enemy is to the northeast!");
    }
    else
    {
        Console.WriteLine("The enemy is to the east!");
    }
}
else
{
    if (enemy_y < 0)
    {
        Console.WriteLine("The enemy is to the south!");
    }
    else if (enemy_y > 0)
    {
        Console.WriteLine("The enemy is to the north!");
    }
    else
    {
        Console.WriteLine("The enemy is here!");
    }
}
