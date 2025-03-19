using System;
using System.IO;
using System.Linq;

class TextAnalyser
{
    public static int CountPrintableCharacters(string input)
    {
        int count = 0;
        foreach (char c in input)
        {
            if (char.IsControl(c))
                count++;
        }
        return count;
    }

    public static int CountWhitespaceCharacters(string input)
    {
        int count = 0;
        foreach (char c in input)
        {
            if (char.IsWhiteSpace(c))
                count++;
        }
        return count;
    }

    public static int CountVowels(string input)
    {
        int count = 0;
        string vowels = "aeiouAEIOU";
        foreach (char c in input)
        {
            if (vowels.Contains(c))
                count++;
        }
        return count;
    }

    public static int CountConsonants(string input)
    {
        int count = 0;
        string consonants = "bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ";
        foreach (char c in input)
        {
            if (consonants.Contains(c))
                count++;
        }
        return count;
    }

    public static void CountVowelFrequencies(string input, int[] vowelFrequencies)
    {
        string vowels = "aeiou";
        foreach (char c in input.ToLower())
        {
            int index = vowels.IndexOf(c);
            if (index != -1)
                vowelFrequencies[index]++;
        }
    }

    static void Main(string[] args)
    {
        string path = "Treasureisland.txt";
        if (File.Exists(path))
        {
            string input = File.ReadAllText(path);
            Console.WriteLine("Analysis of the text:");
            Console.WriteLine("Number of printable characters: {CountPrintableCharacters(input)}");
            Console.WriteLine("Number of whitespace characters: {CountWhitespaceCharacters(input)}");
            Console.WriteLine("Number of vowels: {CountVowels(input)}");
            Console.WriteLine("Number of consonants: {CountConsonants(input)}");

            int[] vowelFrequencies = new int[5]; // a, e, i, o, u
            CountVowelFrequencies(input, vowelFrequencies);
            Console.WriteLine("Vowel frequencies:");
            Console.WriteLine($"a: {vowelFrequencies[0]}, e: {vowelFrequencies[1]}, i: {vowelFrequencies[2]}, o: {vowelFrequencies[3]}, u: {vowelFrequencies[4]}");
        }
        else
        {
            Console.WriteLine("Text file not found.");
        }
    }
}
