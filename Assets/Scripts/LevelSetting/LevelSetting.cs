using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSetting", menuName = "LevelSetting/LevelSetting")]
public class LevelSetting : ScriptableObject
{
    public FullFillmentBank FullFillmentBank;
    public int MaxCreaturesPerSpawn;

    public List<CreatureType> CreatureTypes(int fullFillment)
    {
        List<CreatureType> creatureTypes = new List<CreatureType>();

        if(fullFillment > FullFillmentBank.FullFillments.Count)
            return creatureTypes;

        foreach (var sacrificeType in FullFillmentBank.FullFillments[fullFillment].CreatureTypesNeeded)
        {
            if (!creatureTypes.Contains(sacrificeType.CreatureType))
                creatureTypes.Add(sacrificeType.CreatureType);
        }

        return creatureTypes;
    }
}
