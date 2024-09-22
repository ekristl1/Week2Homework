namespace CharacterConsole;

public interface IOutput  //Outputting to File
{

    void writeCharacter(string name, string characterClass, int level, int health, List<String> equipment, string filepath)
    {
        string character = string.Format("{0},{1},{2},{3},{4}\n", name, characterClass, level, health, String.Join("|", equipment));
        using (StreamWriter writer = new StreamWriter(filepath, true))  // Saving to "database".
        {
            writer.Write(character);
            writer.Close();
        }
    }
    
    public static void updateCharacter(string _filepath, string line)
    {
        Console.Write("Enter the name of the character to level up: ");
        string nameToLevelUp = Console.ReadLine();

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            String characterName = line.Split(",")[0];

            if (line.Contains(nameToLevelUp))
            {
                int commaIndex;
                commaIndex = line.IndexOf(',');
                string level = lines[i].Split(",")[2];
                level = (int.Parse(level) + 1).ToString();
                string[] UpdatedCharacter = new string[] { lines[i].Split(",")[0], lines[i].Split(",")[1], level, lines[i].Split(",")[3], lines[i].Split(",")[4] };
                lines[i] = String.Join(",", UpdatedCharacter);

                using (StreamWriter writer = new StreamWriter(_filePath, false))
                {
                    for (int w = 1; w < lines.Length; w++)
                    {
                        writer.WriteLine(lines[w]);
                    }
                    writer.Close();
                }
                Console.WriteLine("\n{0} is now level {1}", characterName, level);

                break;
            }
        }
    }
    public static void AddCharacter(string _filePath)
    {
        Console.Write("\nEnter your character's name: ");
        string name = Console.ReadLine();

        Console.Write("\nEnter your character's class: ");
        string characterClass = Console.ReadLine();

        int level, health;

        while (true)
        {
            try
            {
                Console.Write("\nEnter your character's level: ");
                level = Convert.ToInt32(Console.ReadLine());
                break;
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter an integer.");
            }
        }
        while (true)
        {
            try
            {
                Console.Write("\nEnter your character's HP: ");
                health = Convert.ToInt32(Console.ReadLine());
                break;
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter an integer.");
            }
        }

        var equipment = new List<String>();
        // int equipmentIndex = 0;
        while (true)
        {
            Console.WriteLine("Enter new equipment name. Type 'q' to end: ");  //TODO Add a check to see if the weapon is valid.
                                                                               //TODO Add an option to list equipment.
                                                                               //TODO Add an option to see attributes of equipment.
            Console.Write("> ");
            string userInput = Console.ReadLine();
            if (userInput == "q")  //Check user input for equipment. If uI=q then break
            {
                break;
            }
            else
            {
                equipment.Add(userInput);
            }
        }

        Console.WriteLine($"\nWelcome, {name} the {characterClass}! You are level {level} and your equipment includes: {string.Join(", ", equipment)}.");

        writeCharacter(name, characterClass, level, health, equipment, _filePath);

        Console.WriteLine("Written");
    }

    void WriteLine(string message);
    void Write(string message);
}