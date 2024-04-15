using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreatureTypeBank", menuName = "Creature/CreatureTypeBank", order = 1)]
public class CreatureTypeBank : ScriptableObject
{
    public List<CreatureType> CreatureTypes;
}
