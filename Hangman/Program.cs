using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hangman
{
    class Program
    {
        static string GetRandomWord() => "TEMPERAMENT SCHIZOFRENIC STERNOCLEIDOMASTOIDIAN ELECTROCARDIOGRAMA XILOFON PNEUMATIC SPERMATOZOID ZBENGHI TRANSPLANT LATIFUNDIAR OPIACEU ARBITRARIETATE PNEUMOGASTRIC STUPEFIANT SOIOS BIBLIOLOGIE COMPLEXITATE METALURGIE ELECTROGLOTOSPECTROGRAFIE FEROMICROAZOTOMBOHIDRIC ELECTROGLOTOSPECTROGRAFIE NEOLOGISM SPECTRU RADIOGRAFIE JARGON INCAPABIL NECOOPERANT METACARP CICLIC TRAPEZOID PISIFORM NOSTALGIE ELOCVENT REGENERATOR DERMATOVENEROLOGIE PNEUMATIC HOMODIEGETIC"
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
            return letters.Where(letter => letter != '_').Distinct().ToList();
        }
        static void PrintStatus(IEnumerable<char> letters, IEnumerable<char> guesses, int livesLeft)
        {
            Console.Clear();
            Console.WriteLine($"Word:\t\t\t{string.Join(" ", letters)}");
            Console.WriteLine($"Used characters:\t[{string.Join(" ", guesses)}]");
            Console.WriteLine($"Lives left: {livesLeft}");
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
            int lives = 26;
            var word = GetRandomWord();
            var startingArray = GetStartingArray(word);
            SetStartingLetters(startingArray, word);
            var guessed = GetGuessed(startingArray);
            while (string.Join("", startingArray) != word && lives > 0)
            {
                PrintStatus(startingArray, guessed, lives);
                Console.WriteLine("Input letter:");
                var guess = Char.ToUpper(Console.ReadKey().KeyChar);
                if (!TrySetLetter(guess, startingArray, word))
                    lives--;
                if (!guessed.Contains(guess))
                    guessed.Add(guess);
            }
            if (string.Join("", startingArray) == word)
            {
                PrintStatus(startingArray, guessed, lives);
                Console.WriteLine($"Congrats, you've guessed the word {word} !!");
            }
            else
                Console.WriteLine($"You've failed in guessing the word {word} !");
        }
    }
}
