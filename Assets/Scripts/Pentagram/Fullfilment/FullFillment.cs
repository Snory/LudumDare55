using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FullFillment", menuName = "FullFillment/FullFillment")]
public class FullFillment : ScriptableObject
{
    public List<FullFillmentItem> CreatureTypesNeeded;

    public List<CreatureType> GetUniqueCreatureTypes()
    {
        List<CreatureType> creatureTypes = new List<CreatureType>();
        foreach (var sacrificeType in CreatureTypesNeeded)
        {
            if (!creatureTypes.Contains(sacrificeType.CreatureType))
                creatureTypes.Add(sacrificeType.CreatureType);
        }

        return creatureTypes;
    }

    public int GetFullFillmentValue()
    {
        int fullFillmentValue = 0;
        foreach (var item in CreatureTypesNeeded)
        {
            fullFillmentValue += item.Amount;
        }

        return fullFillmentValue;
    }

    public FullFillmentItem GetNextSacrifice(int fullfilmentAmount)
    {
        int currentFullfillment = 0;
        FullFillmentItem nextSacrifice = null;

        for (int i = 0; i < CreatureTypesNeeded.Count; i++)
        {            
            currentFullfillment += CreatureTypesNeeded[i].Amount;
            if(fullfilmentAmount < currentFullfillment)
            {
                nextSacrifice = CreatureTypesNeeded[i];
                break;
            }
        }
        
        return nextSacrifice;
    }

}
