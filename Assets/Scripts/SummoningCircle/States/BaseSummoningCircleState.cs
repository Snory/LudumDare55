using UnityEngine;
using UnityEngine.Events;

public abstract class BaseSummoningCircleState : MonoBehaviour
{
    public UnityEvent Ended;

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();

    public abstract void OnTriggerEnterAction(Collider other);

}
