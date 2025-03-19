using System;
using System.Text;

class TuringMachine
{
    private StringBuilder tape;
    private string currentState;
    private int currentPosition;

    public TuringMachine(string input)
    {
        tape = new StringBuilder(input);
        currentState = "START";
        currentPosition = new Random().Next(input.Length); // Start at a random position in the input
    }

    public void Run()
    {
        Console.WriteLine($"Initial tape: {tape}");
        while (currentState != "HALT")
        {
            Console.WriteLine($"Current state: {currentState}");

            switch (currentState)
            {
                case "START":
                    if (tape[currentPosition] != '$')
                    {
                        MoveRight();
                        currentState = "CONTINUE";
                    }
                    else
                    {
                        currentState = "ERASE";
                    }
                    break;

                case "CONTINUE":
                    if (tape[currentPosition] != '$')
                    {
                        MoveRight();
                    }
                    else
                    {
                        currentState = "ERASE";
                    }
                    break;

                case "ERASE":
                    int start = currentPosition;
                    MoveRight();
                    while (tape[currentPosition] != '$')
                    {
                        if (currentPosition >= tape.Length)
                        {
                            Console.WriteLine("TM cannot halt - no second $ found.");
                            return;
                        }
                        tape[currentPosition] = '*';
                        MoveRight();
                    }
                    Console.WriteLine($"Erased tape from {start} to {currentPosition}: {tape}");
                    currentState = "HALT";
                    break;
            }
        }
        Console.WriteLine($"TM halted. Final tape: {tape}");
    }

    private void MoveRight()
    {
        currentPosition++;
        // Wrap around if we go beyond the tape's length
        if (currentPosition >= tape.Length) currentPosition = 0;
    }
}

class Program
{
    static void Main()
    {
        string input = "+ab$12x%$=4p";
        TuringMachine tm = new TuringMachine(input);
        tm.Run();
    }
}
