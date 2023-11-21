using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string playerName;
    public int coins;
    public int maxDice; 
    public int totalThrowValue;
    public int throwValue1; 
    public int throwValue2; 
    public bool hasBuild;
    public bool canReplay = false;
    public List<Establishment> buildingCards = new List<Establishment>();
    public Monument[] monumentCards = new Monument[4];
    
    public Player(string playerName, int coins, int maxDices, int currentDice, List<Establishment> deck, Monument[] monument) //add
    {
        this.playerName = playerName;
        this.coins = coins;
        this.maxDice = maxDices; 
        this.buildingCards = deck;
        this.monumentCards = monument;
    }
    
    public void AddCard(Establishment establishment) //The method add a card to the player deck
    {
        buildingCards.Add(establishment);
    }

    public void BuildMonument(Monument monument) {
        for(int i=0; i<monumentCards.Length; i++) {
            if(monumentCards[i] == monument)
                monumentCards[i].built = true;
        }
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
    
    public void ThrowDice(int diceChoice) //We perform a throw depending on how much dices the player want to throw
    {
        int throwValue1 = 0;
        int throwValue2 = 0;
        throwValue1 += Random.Range(1, 7);
        if (diceChoice == 2)
        {
            throwValue2 += Random.Range(1, 7);
        }

        totalThrowValue = throwValue1 + throwValue2;
    }
}
