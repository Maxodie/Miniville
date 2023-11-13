using System;

public class Ravioli
{
    private char[] str = new char[7]
    {
        'R',
        'a',
        'v',
        'i',
        'o',
        'l',
        'i'
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
