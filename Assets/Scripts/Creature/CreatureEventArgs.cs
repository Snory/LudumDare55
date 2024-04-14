using System;

internal class CreatureEventArgs : EventArgs
{
    public CreatureType CreatureType;
    public bool GhostSpawn;
    public CreatureEventArgs(CreatureType creatureType, bool ghostSpawn = false)
    {
        this.CreatureType = creatureType;
        GhostSpawn = ghostSpawn;
    }
}