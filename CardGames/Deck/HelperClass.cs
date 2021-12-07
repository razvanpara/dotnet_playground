using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGames.Deck
{
    public static class HelperClass
    {
        public static string GetInputWithMessage(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
        public static IEnumerable<T> GetEnumValues<T>() where T : Enum
        {
            var type = typeof(T);
            var instance = Enum.ToObject(type, 0);
            var fields = type.GetFields();
            var members = type.GetMembers();
            return fields.Select(f => (T)f.GetValue(instance)).Distinct();
        }

        public static string AsString(this CardValue value)
        {
            return value switch
            {
                CardValue.Ace => "A",
                CardValue.Jack => "J",
                CardValue.Queen => "Q",
                CardValue.King => "K",
                _ => ((int)value + 2).ToString()
            };
        }
        public static string AsString(this CardSuit cardSuit)
        {
            return cardSuit switch
            {
                CardSuit.Spades => $"{'\u2660'}",
                CardSuit.Hearts => $"{'\u2665'}",
                CardSuit.Diamonds => $"{'\u2666'}",
                _ => $"{'\u2663'}"
            };
        }
    }

}
