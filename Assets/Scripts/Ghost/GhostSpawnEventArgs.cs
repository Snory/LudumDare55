using System;
using UnityEngine;

public class GhostSpawnEventArgs : EventArgs
{
    public Vector3 TargetPosition;
    public CreatureType SourceCreatureType;

    public GhostSpawnEventArgs(Vector3 targetPosition, CreatureType sourceCreatureType)
    {
        this.TargetPosition = targetPosition;
        this.SourceCreatureType = sourceCreatureType;
    }
}