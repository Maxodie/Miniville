public class Pizza
{
    private char[] str = new char[5]
    {
        'P',
        'i',
        'z',
        'z',
        'a'
    };

    public override string ToString()
    {
        string string1 = "";
        foreach (var i in str)
        {
            string1 += i;
        }
        return string1;
    }
}
