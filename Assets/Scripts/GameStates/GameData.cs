using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class GameData {
    public List<Player> players = new List<Player>();
    public List<Monument> monuments = new List<Monument>();
    public Dictionary<Establishment, int> establishments = new Dictionary<Establishment, int>();
    
    const string jsonPath = "cards";

    public GameData()
    {
        InitDatabase();
    }
    
    /// <summary>
    /// Fill monuments List and establishments Dictionary
    /// </summary>
    void InitDatabase()
    {
        // Get json data
        TextAsset jsonData = Resources.Load<TextAsset>(jsonPath);
        // Convert json text to CardHolder
        CardsHolder cardsHolder = JsonUtility.FromJson<CardsHolder>(jsonData.text);
        
        SetMonumentList(cardsHolder.monumentHolders);
        SetEstablishmentDictionary(cardsHolder.establishmentHolders);
    }

    /// <summary>
    /// Fill Monuments List
    /// </summary>
    /// <param name="monumentHolders">Array of monuments card</param>
    private void SetMonumentList(MonumentHolder[] monumentHolders)
    {
        // Create instance of each holder type and add it to monumentsList
        Type monumentType = null;
        for (int i = 0; i < monumentHolders.Length; i++)
        {
            MonumentHolder card = monumentHolders[i];
            switch (i)
            {
                case 0: monumentType = typeof(TrainStation);
                    break;
                case 1: monumentType = typeof(ShoppingMall);
                    break;
                case 2: monumentType = typeof(RadioTower);
                    break;
                case 3: monumentType = typeof(AmusementPark);
                    break;
            }
            
            // Create instance of each card with right Type and add it to monuments List
            monuments.Add((Monument)Activator.CreateInstance(monumentType, 
                card.cardName,
                CardType.CITYLIFE,
                card.cardEffectDescription,
                card.constructionCost,
                card.gains,
                (CardType)card.requiredCardTypeID,
                false,
                (CardPriority)card.requiredCardTypeID));
        }
    }

    /// <summary>
    /// Fill establishments Dictionary
    /// </summary>
    /// <param name="establishmentHolders">Array of establishments card</param>
    private void SetEstablishmentDictionary(EstablishmentHolder[] establishmentHolders)
    {
        // Get color of cards
        Type establishmentType = null;
        int nbCard = 6;
        foreach (EstablishmentHolder card in establishmentHolders)
        {
            switch (card.cardColor.ToUpper())
            {
                case "BLUE": establishmentType = typeof(BlueCard);
                    break;
                case "GREEN": establishmentType = typeof(GreenCard);
                    break;
                case "RED": establishmentType = typeof(RedCard);
                    break;
                case "PURPLE":
                    // Each purple cards are specials and can't be stored as PurpleCard
                    // Set directly cardType by card name who inherit PurpleCard
                    switch (card.cardName.ToUpper())
                    {
                        case "STADIUM": 
                            establishmentType = typeof(Stadium);
                            nbCard = 4;
                            break;
                        case "BUSINESS CENTER": 
                            establishmentType = typeof(BusinessCenter);
                            nbCard = 4;
                            break;
                        case "TV STATION": 
                            establishmentType = typeof(TvStation);
                            nbCard = 4;
                            break;
                    }
                    break;
            }
            
            // Create instance of each card with right Type and add it to establishment Dictionary
            establishments.Add((Establishment)Activator.CreateInstance(establishmentType, 
                card.cardName,
                (CardType)card.cardTypeID,
                card.cardEffectDescription,
                card.constructionCost,
                card.gains,
                (CardType)card.requiredCardTypeID,
                (CardPriority)card.cardPriorityID,
                card.requiredDiceValues), nbCard);
        }
    }
}

public class CardsHolder
{
    public MonumentHolder[] monumentHolders;
    public EstablishmentHolder[] establishmentHolders;
}
