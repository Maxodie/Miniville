using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string name;
    public int coins;
    public int maxDices;
    public int throwValue; 
    public bool hasBuild; 
    public List<Establishment> buildingCards = new List<Establishment>();
    public Monument[] monumentCards = new Monument[4];
    
    public Player(string name, int coins, int maxDices, int currentDice, List<Establishment> deck, Monument[] monument) //add
    {
        this.name = name;
        this.coins = coins;
        this.maxDices = maxDices; 
        this.buildingCards = deck;
        this.monumentCards = monument;
    }
    
    public void AddCard(Establishment establishment) //The method add a card to the player deck
    {
        buildingCards.Add(establishment);
    }
    
    public int GetCardsNbByType(CardType cardType) //Since some cards have effects depending on what number of that precise type of card you have, we need a function that count a specific type of card
    {
        int count = 0;
        
        foreach (var establishment in buildingCards) //We compare all the card type that the player have with the card type we want to check
        {
            if (establishment.cardType == cardType)
            {
                count++;
            }
        }

        return count;
    }

    public int ThrowDice(int diceChoice) //We perform a throw depending on how much dices the player want to throw
    {
        for (int i = 0; i < diceChoice; i++)
        {
            throwValue += Random.Range(0, 7);
        }

        return throwValue;
    }
}
