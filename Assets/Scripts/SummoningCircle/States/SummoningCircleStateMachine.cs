using UnityEngine;

public class SummoningCircleStateMachine : MonoBehaviour
{
    private BaseSummoningCircleState _currentState;

    [SerializeField]
    private SummoningCircleConjureState _conjureState;

    [SerializeField]
    private SummoningCircleExploreState _exploreState;

    private void Awake()
    {
        ChangeState(_exploreState);
    }

    public void ChangeState(BaseSummoningCircleState newState)
    {
        if (_currentState != null)
        {
            _currentState.OnExit();
        }

        _currentState = newState;
        _currentState.OnEnter();
    }

    public void OnStateEnded()
    {
        if(_currentState as SummoningCircleExploreState != null)
        {
            ChangeState(_conjureState);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.OnUpdate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_currentState != null)
        {
            _currentState.OnTriggerEnterAction(other);
        }
    }
}
