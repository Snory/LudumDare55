using System;

public class SacrificeEventArgs : EventArgs
{
    public CreatureType CreatureType { get; private set; }
    public ConjecturePoint ConjecturePoint { get; private set; }

    public SacrificeEventArgs(CreatureType creatureType, ConjecturePoint conjecturePoint)
    {
        CreatureType = creatureType;
        ConjecturePoint = conjecturePoint;
    }
}