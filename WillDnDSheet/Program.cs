
//Jan 21, 2025
//Code from William


Console.WriteLine("Welcome to D&D Character Sheet Creator");
Console.WriteLine("--------------------------------------");
Console.WriteLine("Please input your Character name, then press enter");
Console.Write("Name: ");

string playerName = Console.ReadLine();

Console.Clear();

//Player class select
string[] playerClassChoices = { "Bard", "Cleric", "Fighter", "Mage", "Paladin", "Ranger" };

Console.WriteLine("Choose a class for your Character: ");
Console.WriteLine("Input the number corresponding with your desired class");
Console.WriteLine("Then press enter");
Console.WriteLine("----------------------------------");

for (int i = 0; i < playerClassChoices.Length; i++)
{
    Console.WriteLine(playerClassChoices[i] + $" ({i})");
}

Console.WriteLine();
Console.Write("Choice (corresponding number) -> ");

string playerClassInput = Console.ReadLine();
int playerClassIndex = int.Parse(playerClassInput);

string playerClass = playerClassChoices[playerClassIndex];

Console.WriteLine("You have chosen the " + playerClass + " Class");
Console.WriteLine("Press enter to continue to Stats");
Console.ReadKey();
Console.Clear();

//stats generating
Random randomRoll = new Random();

string[] statList = { "Strength", "Dexterity", "Constitution", "Intellegence", "Wisdom", "Charisma" };
List<int> finalPlayerStats = new List<int>();

int RollDice()
{
    int rollDice = randomRoll.Next(1, 6); // randomizes a number between 1 and 6 
    return rollDice;
}

int CreateStat()
{
    int[] rollList = new int[4];

    for (int i = 0; i < 4; i++)
    {
        int generatedRoll = RollDice();
        rollList[i] = generatedRoll;
        Console.Write(" " + generatedRoll + " ");
    }
    Console.WriteLine();
    Console.WriteLine("======================");
    int discardedRoll = rollList.Min();

    Console.WriteLine("Discarding lowest roll...");
    Console.WriteLine("- " + discardedRoll + " -");
    Console.WriteLine("======================");
    Console.WriteLine();

    int StatTotal = rollList[0] + rollList[1] + rollList[2] + rollList[3];
    int finalStatTotal = StatTotal - discardedRoll;

    return finalStatTotal;
}

Console.WriteLine("Would you like to have a standard array or roll for your player stats?");
Console.WriteLine(" - Standard Stat Array (s) - ");
Console.WriteLine(" - Roll dice for stats (r) - ");
Console.WriteLine("Input the letter corresponding with your choice");
Console.Write("Choice: ");

string statChoice = Console.ReadLine().ToLower();

List<int> characterStatList = new List<int>(); //holds stat values for user to allocate

void ShowRemainingStats(int index, int number)
{
    Console.WriteLine("||*>----------------------------------------<*||");
    Console.WriteLine("Remaining numbers : ");
    foreach (int s in characterStatList)
    {
        Console.WriteLine($" {s} | Index: " + number);
        number++;
        Thread.Sleep(100);
    }
    Console.WriteLine("||*>----------------------------------------<*||");
    Console.WriteLine();

    Console.WriteLine("Choose a number to assign to: " + statList[index]);
    Console.WriteLine("Write the Index that corresponds with the number you want to use");
    Console.Write("Input Index: ");

    string playerInput = Console.ReadLine();
    int statInput = int.Parse(playerInput);

    finalPlayerStats.Add(characterStatList[statInput]);
    characterStatList.RemoveAt(statInput);
}

if (statChoice == "r")
{
    //Rolling for stats

    //roll 4 dice

    Console.WriteLine("You have chosen to roll dice for your character's stats");
    Console.WriteLine("Rolling your dice...");
    Console.WriteLine("-------------------------------------------------------");
    Thread.Sleep(500);

    int statIndex = 1; //tells user the number of rolls

    for (int i = 0; i < 6; i++)
    {
        Console.WriteLine("Stat roll " + statIndex);
        characterStatList.Add(CreateStat());
        statIndex++;
        Console.WriteLine();
        Thread.Sleep(1000);
    }

    Console.WriteLine("||*>----------------------------------------<*||");
    Console.WriteLine("Here are your final stats that you can allocate: ");
    Console.Write("|");
    foreach (int s in characterStatList)
    {
        Console.Write($" {s} |");
        Thread.Sleep(200);
    }
    Console.Write("\n");
    Console.WriteLine("||*>----------------------------------------<*||");
    Console.WriteLine();
    Console.WriteLine("Press enter when you are ready to create your character stats");
    Console.ReadKey();
    Console.Clear();

    int statNumber = 0;

    for (int i = 0; i < 6; i++)
    {
        ShowRemainingStats(i, statNumber);
    }

    Console.WriteLine("Finished Stat Allocation");
    Console.WriteLine("Press enter to continue to final sheet");
    Console.ReadKey();
    Console.Clear();

    DisplayFinalCharacter();
}
else
{
    //Standard stats (info taken from d&d wiki)
    characterStatList.Add(15);
    characterStatList.Add(14);
    characterStatList.Add(13);
    characterStatList.Add(12);
    characterStatList.Add(10);
    characterStatList.Add(8);

    Console.WriteLine("You chose to not randomize stats");
    Console.WriteLine("||*>----------------------------------------<*||");
    Console.WriteLine("Here are your stats that you can allocate: ");
    Console.Write("|");
    foreach (int s in characterStatList)
    {
        Console.Write($" {s} |");
        Thread.Sleep(200);
    }
    Console.Write("\n");
    Console.WriteLine("||*>----------------------------------------<*||");
    Console.WriteLine();
    Console.WriteLine("Press enter when you are ready to create your character stats");
    Console.ReadKey();
    Console.Clear();

    int statNumber = 0;

    for (int i = 0; i < 6; i++)
    {
        ShowRemainingStats(i, statNumber);
    }

    Console.WriteLine("Finished Stat Allocation");
    Console.WriteLine("Press enter to continue to final sheet");
    Console.ReadKey();
    Console.Clear();

    DisplayFinalCharacter();
}

void DisplayFinalCharacter()
{
    Console.WriteLine(@" \ Completed D&D Character Sheet /");
    Console.WriteLine("<=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=>");
    Console.WriteLine($" - Name : {playerName}");
    Console.WriteLine($" - Class : {playerClass}");
    Console.WriteLine("<=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=>");
    Thread.Sleep(300);
    Console.WriteLine("      << Character's Stats >> ");
    Console.WriteLine("-----------------------------------");
    Console.WriteLine(" | Strength (STR)     : " + finalPlayerStats[0]);
    Console.WriteLine(" | Dexterity (DEX)    : " + finalPlayerStats[1]);
    Console.WriteLine(" | Constitution (CON) : " + finalPlayerStats[2]);
    Console.WriteLine("-----------------------------------");
    Console.WriteLine(" | Intellegence (INT) : " + finalPlayerStats[3]);
    Console.WriteLine(" | Wisdom (WIS)       : " + finalPlayerStats[4]);
    Console.WriteLine(" | Charisma (CHA)     : " + finalPlayerStats[5]);
    Console.WriteLine("-----------------------------------");

    Console.ReadLine();
}
