using UnityEngine;

public class SummoningCircleConjureState : BaseSummoningCircleState
{

    [SerializeField]
    private float _timeToConjure = 5f;
    private float _timeToConjureCounter = 0f;

   
    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }

    public override void OnTriggerEnterAction(Collider other)
    {

    }

    public override void OnUpdate()
    {
        _timeToConjureCounter += Time.deltaTime;

        if (_timeToConjureCounter >= _timeToConjure)
        {
            Debug.Log("Conjure Complete");
            Ended?.Invoke();
            Destroy(this.gameObject);
        }
    }
}