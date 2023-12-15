using System;

// Class representing a Ravioli
public class Ravioli
{
    // Private character array to store the word "Ravioli"
    private char[] str = new char[7] // Initialize an array of size 7
    {
        'R', // Index 0: 'R'
        'a', // Index 1: 'a'
        'v', // Index 2: 'v'
        'i', // Index 3: 'i'
        'o', // Index 4: 'o'
        'l', // Index 5: 'l'
        'i'  // Index 6: 'i'
    };

    // Override ToString method to concatenate characters in the array to form the word "Ravioli"
    public override string ToString()
    {
        string string1 = ""; // Initialize an empty string

        // Iterate through the character array and concatenate each character to the string
        foreach (var i in str)
        {
            string1 += i; // Append each character to the string
        }

        return string1; // Return the concatenated string ("Ravioli")
    }
}