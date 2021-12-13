namespace HashTableImpl
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Person p)
                return false;
            return p.Name == this.Name && p.Age == this.Age;
        }

        public override int GetHashCode()
        {
            var hash = Hashing.ByteFoldHash(this.Name + Age);
            return hash;
        }
    }
}
