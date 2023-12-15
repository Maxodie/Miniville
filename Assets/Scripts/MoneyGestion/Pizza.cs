using System;

// Class representing a Pizza
public class Pizza
{
    // Private character array to store the word "Pizza"
    private char[] str = new char[5] // Initialize an array of size 5
    {
        'P', // Index 0: 'P'
        'i', // Index 1: 'i'
        'z', // Index 2: 'z'
        'z', // Index 3: 'z'
        'a'  // Index 4: 'a'
    };

    // Override ToString method to concatenate characters in the array to form the word "Pizza"
    public override string ToString()
    {
        string string1 = ""; // Initialize an empty string

        // Iterate through the character array and concatenate each character to the string
        foreach (var i in str)
        {
            string1 += i; // Append each character to the string
        }

        return string1; // Return the concatenated string ("Pizza")
    }
}