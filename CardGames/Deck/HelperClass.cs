using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGames.Deck
{
    public static class HelperClass
    {
        public static IEnumerable<T> GetEnumValues<T>() where T : Enum
        {
            var type = typeof(T);
            var instance = Enum.ToObject(type, 0);
            var fields = type.GetFields();
            var members = type.GetMembers();
            return fields.Select(f => (T)f.GetValue(instance)).Distinct();
        }
    }

}
