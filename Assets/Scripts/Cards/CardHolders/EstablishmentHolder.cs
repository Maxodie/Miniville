using System;

[Serializable]
public class EstablishmentHolder
{
    public string cardName;
    public string cardColor;
    public int cardTypeID;
    public string cardEffectDescription;
    public int[] requiredDiceValues;
    public int gains;
    public int requiredCardTypeID;
    public int cardPriorityID;
    public int constructionCost;
}
