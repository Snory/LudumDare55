using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FullFillment", menuName = "FullFillment/FullFillment")]
public class FullFillment : ScriptableObject
{
    public List<FullFillmentItem> FullfillmentItems;

    public List<CreatureType> GetUniqueCreatureTypes()
    {
        List<CreatureType> creatureTypes = new List<CreatureType>();
        foreach (var sacrificeType in FullfillmentItems)
        {
            if (!creatureTypes.Contains(sacrificeType.CreatureType))
                creatureTypes.Add(sacrificeType.CreatureType);
        }

        return creatureTypes;
    }

    public int GetMaxFullFillment()
    {
        int maxValue = 0;
        foreach (var item in FullfillmentItems)
        {
            maxValue += item.Amount;
        }

        return maxValue;
    }

    public int GetCurrentFilling()
    {
        int currentFilling = 0;
        foreach (var item in FullfillmentItems)
        {
            currentFilling += item.CurrentAmount;
        }

        return currentFilling;
    }

    public FullFillmentItem GetNextSacrifice(int fullfilmentAmount)
    {
        int currentFullfillment = 0;
        FullFillmentItem nextSacrifice = null;

        for (int i = 0; i < FullfillmentItems.Count; i++)
        {            
            currentFullfillment += FullfillmentItems[i].Amount;
            if(fullfilmentAmount < currentFullfillment)
            {
                nextSacrifice = FullfillmentItems[i];
                break;
            }
        }
        
        return nextSacrifice;
    }

    public List<FullFillmentItem> GetNextSacrifices()
    {
        return FullfillmentItems.Where(x => x.Amount != x.CurrentAmount).ToList();
    }

    public override string ToString()
    {
        string fullfillmentString = "";

        fullfillmentString += "FullfillmentRemaining: " + GetCurrentFilling() +  "\n";
        fullfillmentString += "FullfillmentMax: " + GetMaxFullFillment() + "\n";

        foreach (var item in FullfillmentItems)
        {
            fullfillmentString += item.ToString() + "\n";
        }

        return fullfillmentString;
    }

}
