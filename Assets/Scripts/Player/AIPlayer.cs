using UnityEngine;
using System.Collections.Generic;

public class AIPlayer : Player {
    public AIPlayer(bool isCurrentPlayer, string playerName, int coins, int maxDices, int currentDice, List<Establishment> deck, Monument[] monument, GameObject playerCanvas) : 
    base(isCurrentPlayer, playerName, coins, maxDices, currentDice, deck, monument, playerCanvas) {

    }

    public override void OptionalPlayerThrowDice(ITurnState turnState) {
        turnState.QuitState();
    }

    public override void OptionalPlayerBuild(ITurnState turnState) { 
        turnState.QuitState();
    }
    public override void OptionalPlayerInteraction(ITurnState turnState) { 
        turnState.QuitState();
    }
}
