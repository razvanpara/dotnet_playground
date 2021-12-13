using System;

namespace HashTableImpl
{
    class Program
    {
        static void Main(string[] args)
        {
            var table = new HashTable<string>();
            table["raz"] = "van";
            table["raz\0"] = "vanul";

            var hashSet = new HashSet<Person>();
            for (int i = 0; i < 10000; i++)
            {
                hashSet.Add(new Person($"raz{i}", i));
            }
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(table, table.GetType()));
        }
    }
}
