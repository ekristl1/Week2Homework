namespace CharacterConsole;

public class CharacterManager
{
    private readonly IInput _input;
    private readonly IOutput _output;
    private string _filePath = "C:\\.C#\\School\\W3\\W3_homework\\input.csv";

    private string[] lines;

    public CharacterManager(IInput input, IOutput output)
    {
        _input = input;
        _output = output;
    }

    public void Run()
    {
        _output.WriteLine("Welcome to Character Management");

        lines = File.ReadAllLines(_filePath);

        while (true)
        {
            _output.WriteLine("\nMenu:");
            _output.WriteLine("1. Display Characters");
            _output.WriteLine("2. Find a Character");
            _output.WriteLine("3. Add Character");
            _output.WriteLine("4. Level Up Character");
            _output.WriteLine("5. Exit");
            _output.Write("Enter your choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    IInput.charVar(IInput.DisplayAllCharacters(lines)); //will throw errros but displays as intended. 
                    break;
                case "2":
                    IInput.charVar(IInput.DisplayAllCharacters(lines));  //Functionailty doesn't work
                    break;
                case "3":
                    IOutput.AddCharacter(_filePath); //Works, but to display you must restart program
                    break;
                case "4":
                    IOutput.updateCharacter(_filePath, lines); //Also doesn't work
                    break;
                case "5":
                    return;
                default:
                    _output.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}