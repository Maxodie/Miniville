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
    }

    public override void OptionalPlayerBuild(BuildBehaviour buildBehaviour) { 
        turnState.QuitState();
    }
    public override void OptionalPlayerInteraction(ITurnState turnState) { 
        turnState.QuitState();
    }
}
