using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SacrificeTypeBank", menuName = "Pentagram/SacrificeTypeBank", order = 1)]
public class SacrificeTypeBank : ScriptableObject
{
    public List<SacrificeType> SacrificeTypes;
}
