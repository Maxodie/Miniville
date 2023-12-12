using UnityEngine;
using System.Collections.Generic;

public class AIPlayer : Player {
    public AIPlayer(bool isCurrentPlayer, string playerName, int coins, int maxDice, int currentDice, List<Establishment> deck, Monument[] monument, GameObject playerCanvas) : 
    base(isCurrentPlayer, playerName, coins, maxDice, currentDice, deck, monument, playerCanvas) {

    }

    public override void OptionalPlayerThrowDice(ThrowDiceBehaviour throwDiceBehaviour, GameData gameData) { 
        if (maxDice == 2)
        {
            throwDiceBehaviour.PlayerThrowTwoDice();
        }
        else
            throwDiceBehaviour.PlayerThrowOneDice();
    }

    public override void OptionalPlayerBuild(BuildBehaviour buildBehaviour, GameData gameData)
    {
        foreach (var t in monumentCards)
        {
            if (coins < t.constructionCost) continue;
            buildBehaviour.BuilddMonumentCard(t);
            buildBehaviour.QuitState();
        }
        buildBehaviour.QuitState();
    }

    public override void OptionalPlayerInteraction(InteractionBehaviour interactionBehaviour, GameData gameData) {
        foreach (var e in gameData.players)
        {
            for (int i = 0; i < e.buildingCards.Count; i++)
            {
                if (e.buildingCards[i][0].cardType != CardType.CITYLIFE)
                {
                    //gameData.players[interactionBehaviour.playerTurn].ExchangeCard(gameData.players[interactionBehaviour.playerTurn].buildingCards[0][0], e.buildingCards[i][0], );
                }
            }
        }
        interactionBehaviour.QuitState();
    }
}