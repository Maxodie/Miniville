using UnityEngine;
using System.Collections.Generic;

public class AIPlayer : Player
{
    private Monument[] monumentArray;
    private Establishment[] establishments;
    
    public AIPlayer(bool isCurrentPlayer, string playerName, int coins, int maxDice, int currentDice, List<Establishment> deck, Monument[] monument, GameObject playerCanvas) : 
    base(isCurrentPlayer, playerName, coins, maxDice, currentDice, deck, monument, playerCanvas)
    {
    }

    public override void OptionalPlayerThrowDice(ThrowDiceBehaviour throwDiceBehaviour)
    {
        if (maxDice == 2)
        {
            throwDiceBehaviour.PlayerThrowTwoDice();
        }
        else
        {
            throwDiceBehaviour.PlayerThrowOneDice();
        }
    }

    public override void OptionalPlayerBuild(BuildBehaviour buildBehaviour)
    {
        // TODO: Fill the arrays with the cards and then check because for some reasons we fucked up the polymorphism
        /* foreach (CardUIPrefab card in buildBehaviour.cardPrefabs)
        {
            if (card.card.GetType() == typeof(Monument))
            {
                if (!buildBehaviour.CanBuild(card.card)) continue;
                if (Random.Range(0, 101) >= 25) continue;
                buildBehaviour.BuildMonumentCard(card.card);
                buildBehaviour.EndBuild();
                return;
            }

            if (card.card.GetType() != typeof(Establishment)) continue;
            if (!buildBehaviour.CanBuild(card.card)) continue;
            if (Random.Range(0, 101) >= 25) continue;
            buildBehaviour.BuildEstablishmentCard(card.card);
            buildBehaviour.EndBuild();
            return; */
        }
    }
    public override void OptionalPlayerInteraction(ITurnState turnState)
    { 
        turnState.QuitState();
    }
}
