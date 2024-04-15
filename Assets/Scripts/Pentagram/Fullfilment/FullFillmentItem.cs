using UnityEngine;

[System.Serializable]   
public class FullFillmentItem 
{
    public CreatureType CreatureType;
    public int Amount;

    public int Order;

    public int CurrentAmount;

    public int RemainingAmount => Amount - CurrentAmount;
}
