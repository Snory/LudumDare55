using UnityEngine;

[CreateAssetMenu(fileName = "SacrificeType", menuName = "Pentagram/SacrificeType", order = 1)]
public class SacrificeType : ScriptableObject
{
    public ConjecturePointType ConjecturePointType;
    public Material Material;
    public Sprite Sprite;

}
