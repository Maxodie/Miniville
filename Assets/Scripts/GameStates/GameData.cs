using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class GameData {
    public List<Player> players = new List<Player>();
    public List<Monument> monuments = new List<Monument>();
    
    const string jsonPath = "cards";

    public GameData()
    {
        InitDatabase();
    }
    
    /// <summary>
    /// Get every cards to json file and put on cards list
    /// </summary>
    void InitDatabase()
    {
        // Get json data
        TextAsset monumentsData = Resources.Load<TextAsset>(jsonPath);
        // Store monument holders data
        CardHolder holders = JsonUtility.FromJson<CardHolder>(monumentsData.text);
        
        // Create instance of each holder type and add it to monumentsList
        Type monumentType = null;
        for (int i = 0; i < holders.monumentHolders.Length; i++)
        {
            MonumentHolder holder = holders.monumentHolders[i];
            switch (i)
            {
                case 0:
                    monumentType = typeof(TrainStation);
                    break;
                case 1:
                    monumentType = typeof(ShoppingMall);
                    break;
                case 2:
                    monumentType = typeof(RadioTower);
                    break;
                case 3:
                    monumentType = typeof(AmusementPark);
                    break;
            }
            
            monuments.Add((Monument)Activator.CreateInstance(monumentType, 
                holder.cardName,
                CardType.CITYLIFE,
                holder.cardEffectDescription,
                holder.constructionCost,
                holder.gains,
                (CardType)holder.requiredCardTypeID,
                false));
        }
        
        Debug.Log(monuments[0]);
        Debug.Log(monuments[0].requiredCardType);
        Debug.Log(monuments[1]);
        Debug.Log(monuments[1].requiredCardType);
        Debug.Log(holders.monumentHolders.Length);
    }
}

public class CardHolder
{
    public MonumentHolder[] monumentHolders;
}
