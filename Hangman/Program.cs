using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hangman
{
    class Program
    {
        static string GetRandomWord() => "ABRUPTLY AWKWARD BAGPIPES BANDWAGON BANJO BEEKEEPER BIKINI BLIZZARD ZOMBIE"
            .Split(" ")
            .OrderBy(_ => Guid.NewGuid())
            .First();

        static char[] GetStartingArray(string word)
        {
            var length = word.Length;
            var arr = new char[length];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = '_';
            return arr;
        }

        static int[] GetRandomIndexes(string word)
        {
            var random = new Random();
            var length = word.Length;

            var previouslySetCharactersCount = (int)Math.Ceiling((double)length / 4);

            var hashSet = new HashSet<int>();
            while (hashSet.Count < previouslySetCharactersCount)
                hashSet.Add(random.Next(0, length));

            return hashSet.ToArray();
        }

        static void SetStartingLetters(char[] letters, string word)
        {
            var length = letters.Length;
            var indexes = GetRandomIndexes(word);

            foreach (var index in indexes)
                TrySetLetter(word[index], letters, word);
        }

        static List<char> GetGuessed(char[] letters)
        {
            return letters.Where(letter => letter != '_').ToList();
        }
        
        static bool TrySetLetter(char guess, char[] letters, string word)
        {
            bool set = false;
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == guess && letters[i] != guess)
                {
                    letters[i] = guess;
                    set = true;
                }
            }
            return set;
        }
       
        static void Main(string[] args)
        {
            int lives = 5;
            var word = GetRandomWord();
            var startingArray = GetStartingArray(word);
            SetStartingLetters(startingArray, word);
            var guessed = GetGuessed(startingArray);
            while (guessed.Count < word.Length && lives > 0)
            {
                Console.Clear();
                Console.WriteLine(string.Join(" ", startingArray));
                Console.WriteLine($"Used characters: [{string.Join(" ", guessed)}]");
                Console.WriteLine("Input letter:");
                var guess = Char.ToUpper(Console.ReadKey().KeyChar);
                Console.Write('\b');
                if (!TrySetLetter(guess, startingArray, word))
                    lives--;
                guessed.Add(guess);
            }
            if (guessed.Count == word.Length)
            {
                Console.Clear();
                Console.WriteLine(string.Join(" ", startingArray));
                Console.WriteLine($"Used characters: [{string.Join(" ", guessed)}]");
                Console.WriteLine($"Congrats, you've guessed the word {word} !!");
            }
            else
                Console.WriteLine($"You've failed in guessing the word {word} !");
        }
    }
}
