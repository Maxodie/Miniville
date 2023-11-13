using System.Collections.Generic;

public class Player
{
    public string name;
    public int coins;
    public int maxDices; //add
    public int currentDice; //add
    public List<Card> buildingCards = new List<Card>();
    public Card[] monumentCards = new Card[4];
    
    public Player(string name, int coins, int maxDices, int currentDice, List<Card> deck, Card[] monument) //add
    {
        this.name = name;
        this.coins = coins;
        this.maxDices = maxDices; 
        this.currentDice = currentDice; 
        this.buildingCards = deck;
        this.monumentCards = monument;
    }
    
    public void AddCard(Card card) //Méthode à rajouter dans l'UML //The method add a card to the player deck
    {
        buildingCards.Add(card);
    }
    
    public int GetCardsNbByType(CardType cardType) //Since some cards have effects depending on what number of that precise type of card you have, we need a function that count a specific type of card
    {
        int count = 0;
        
        foreach (var card in buildingCards) //We compare all the card type that the player have with the card type we want to check
        {
            if (card.cardType == cardType)
            {
                count++;
            }
        }

        return count;
    }
}
