using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningCircleExploreState : BaseSummoningCircleState
{
    [SerializeField]
    private float _timeToExplore = 5f;
    private float _timeToExploreCounter = 0f;

    [SerializeField]
    private SphereCollider _sphereCollider;

    public override void OnEnter()
    {
        _sphereCollider.isTrigger = true;
    }

    public override void OnExit()
    {
        _sphereCollider.isTrigger = false;
    }

    public override void OnTriggerEnterAction(Collider other)
    {
    }

    public override void OnUpdate()
    {
        _timeToExploreCounter += Time.deltaTime;

        if (_timeToExploreCounter >= _timeToExplore)
        {
            Ended?.Invoke();
        }
    }
}
