using UnityEngine;
using System.Collections.Generic;

public class AIPlayer : Player {
    public AIPlayer(bool isCurrentPlayer, string playerName, int coins, int maxDice, int currentDice, List<Establishment> deck, Monument[] monument, GameObject playerCanvas) : 
    base(isCurrentPlayer, playerName, coins, maxDice, currentDice, deck, monument, playerCanvas) {

    }

    public override void OptionalPlayerThrowDice(ThrowDiceBehaviour throwDiceBehaviour) { 
        if (maxDice == 2)
        {
            throwDiceBehaviour.PlayerThrowTwoDice();
        }
        else
            throwDiceBehaviour.PlayerThrowOneDice();
    }

    public override void OptionalPlayerBuild(BuildBehaviour buildBehaviour) { 
        for(int i=0; i < monumentCards.Length; i++) {
            if(coins >= monumentCards[i].constructionCost) {
                
            }
        }

        buildBehaviour.QuitState();
    }

    public override void OptionalPlayerInteraction(InteractionBehaviour interactionBehaviour) { 
        interactionBehaviour.QuitState();
    }
}
