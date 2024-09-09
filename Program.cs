using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Xml.Linq;

class Program
{
    static string[] lines;

    string filePath = "input.csv";

    static void Main()
    {
        string filePath = "input.csv";
        lines = File.ReadAllLines(filePath);

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Display Characters");
            Console.WriteLine("2. Add Character");
            Console.WriteLine("3. Level Up Character");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayAllCharacters(lines);
                    break;
                case "2":
                    AddCharacter(ref lines, filePath);
                    break;
                case "3":
                    LevelUpCharacter(lines, filePath);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void DisplayAllCharacters(string[] lines)
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
            Console.WriteLine("\nName: " + name);  //Name  Field
            Console.WriteLine("Class: " + fields.Split(",")[1]);  //Class Field
            Console.WriteLine("Level: " + fields.Split(",")[2]);
            Console.WriteLine("HP: " + fields.Split(",")[3]);
            string equipmentLine = fields.Split(",")[4];
            string[] equipment = equipmentLine.Split('|');
            Console.WriteLine("Equipment List:");
            for (int j = 0; j < equipment.Length; j++)  // To make the items in Equipment List more legible 
            {
                Console.WriteLine(" - " + equipment[j]);
            }
        }
    }

    static void AddCharacter(ref string[] lines, string filePath)
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
        int equipmentIndex = 0;
        while (true)
        {
            Console.WriteLine("Enter new equipment name. Type 'q' to end: ");
            Console.Write("> ");
            string userInput = Console.ReadLine();
            if (userInput == "0")  //Check user input for equipment. If uI=0 then break
            {
                break;
            }
            else
            {
                equipment.Add(userInput);
            }
        }

        Console.WriteLine($"\nWelcome, {name} the {characterClass}! You are level {level} and your equipment includes: {string.Join(", ", equipment)}.");

        string character = string.Format("\n{0},{1},{2},{3},{4}", name, characterClass, level, health, String.Join("|", equipment));  // Simular to print(f)

        using (StreamWriter writer = new StreamWriter(filePath, true))  // Saving to "database".
        {
            writer.WriteLine(character);
            writer.Close();
        }
    }
    static void LevelUpCharacter(string[] lines, string filePath)
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

                using (StreamWriter writer = new StreamWriter(filePath, false))
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