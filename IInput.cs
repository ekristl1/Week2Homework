namespace CharacterConsole;

public interface IInput
{
    // function that processes and displays character data
    public static void charVar(string[] lines, Action<string> displayFunction)
    {

        for (int i = 1; i < lines.Length; i++) 
        {
            string line = lines[i];

            string name;
            string fields;

            if (line.StartsWith('"'))  // If line from input.csv starts with ", then remove it. 
            {

                var firstQuotePos = lines[i].IndexOf('"');  //Check for first case of ".
                name = lines[i].Substring(firstQuotePos + 1);
                var lastQuotePos = name.IndexOf('"');

                name = name.Substring(firstQuotePos, lastQuotePos - firstQuotePos);  // 
                fields = line.Substring(lastQuotePos + 1);
            }
            else
            {
                name = lines[i].Split(",")[0];
                int firstComma = line.IndexOf(",");
                fields = line.Substring(firstComma);
            }

            string charClass = fields.Split(",")[1];
            string level = fields.Split(",")[2];
            string hp = fields.Split(",")[3];
            string equipmentLine = fields.Split(",")[4];

        }

        static void DisplayAllCharacters(string name)
        {
            Console.WriteLine("\nName: " + name);  //Name  Field
            Console.WriteLine("Class: " + charClass);  //Class Field
            Console.WriteLine("Level: " + level);
            Console.WriteLine("HP: " + hp);
            string[] equipment = equipmentLine.Split('|');
            Console.WriteLine("Equipment List:");
            for (int j = 0; j < equipment.Length; j++)  // To make the items in Equipment List more legible 
            {
                Console.WriteLine(" - " + equipment[j]);
            }
        }

        static void DisplayAllCharacters(string name, string charClass, string equipmentLine, int level, int hp)
        {
            Console.WriteLine("\nName: " + name);  //Name  Field
            Console.WriteLine("Class: " + charClass);  //Class Field
            Console.WriteLine("Level: " + level);
            Console.WriteLine("HP: " + hp);
            string[] equipment = equipmentLine.Split('|');
            Console.WriteLine("Equipment List:");
            for (int j = 0; j<equipment.Length; j++)  // To make the items in Equipment List more legible 
            {
                Console.WriteLine(" - " + equipment[j]);
            }
        }
        void LevelUpCharacter(string[] lines, string filePath, IOutput output)
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
    }
    string ReadLine();
}