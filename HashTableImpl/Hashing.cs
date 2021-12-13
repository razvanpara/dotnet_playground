namespace HashTableImpl
{
    public static class Hashing
    {
        public static int ByteFoldHash(string s)
        {
            int hash = 0;
            int val = 0;
            for (int i = 1; i <= s.Length; i++)
            {
                var ch = s[i - 1] << 24;
                val >>= 8;
                val |= ch;
                if (i % 4 == 0)
                {
                    hash += val;
                    val &= 0;
                }
            }
            if (s.Length % 4 != 0)
            {
                var rshift = (24 - (s.Length % 4 - 1) * 8);
                hash += (val >> rshift);
            }
            return hash;
        }
    }
}
