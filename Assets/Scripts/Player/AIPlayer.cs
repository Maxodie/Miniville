using UnityEngine;
using System.Collections.Generic;

public class AIPlayer : Player {
    public AIPlayer(bool isCurrentPlayer, string playerName, int coins, int maxDice, int currentDice, List<Establishment> deck, Monument[] monument, GameObject playerCanvas, GameObject playerFrame, UIPlayerFrameScriptableObject uIPlayerFrameScriptableObject) : 
    base(isCurrentPlayer, playerName, coins, maxDice, currentDice, deck, monument, playerCanvas, playerFrame, uIPlayerFrameScriptableObject) {

    }

    public override void OptionalPlayerThrowDice(ThrowDiceBehaviour throwDiceBehaviour, GameData gameData) { 
        if (currentDice == 2)
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
            return;
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
                    gameData.players[interactionBehaviour.playerTurn].ExchangeCard(0, e, i);
                    interactionBehaviour.QuitState();
                    return;
                }
            }
        }
        interactionBehaviour.QuitState();
    }
}