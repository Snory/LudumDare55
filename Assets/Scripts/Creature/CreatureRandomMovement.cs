using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class CreatureRandomMovement : MonoBehaviour
{
    private NavMeshAgent _agent;

    private float _nextDestinationTime = 0;

    [SerializeField]
    private float _nextDestinationMinCooldown = 1;
    [SerializeField]
    private float _nextDestinationMaxCooldown = 5;

    [SerializeField]
    private float _minRandomTries = 1;

    [SerializeField]
    private float _maxRandomTries = 3;

    [SerializeField]
    private float _currentRandomTries = 0;

    [SerializeField]
    private UnityEvent TriesExceeded;

    [SerializeField]
    private bool _canMove;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();  
        if(_agent == null)
        {
            Debug.LogError("NavMeshAgent component not found");
        }

        _agent.isStopped = true;
        _canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent.isStopped)
        {
            return;
        }

        if (!_canMove)
        {
            return;
        }

        if (!_agent.hasPath || _agent.remainingDistance < 0.5f)
        {
            if (Time.time > _nextDestinationTime)
            {
                SetRandomDestination();
            }
        }
    }

    public void OnNudgeMovementStarted()
    {
        _agent.isStopped = true;
    }

    public void OnNudgeMovementEnded()
    {
        _agent.isStopped = false;
        SetRandomDestination();
    }

    private void SetRandomDestination()
    {
        _currentRandomTries++;

        if(_currentRandomTries > Random.Range(_minRandomTries, _maxRandomTries))
        {
            _agent.isStopped = true;
            TriesExceeded?.Invoke();
            return;
        }
        
        //random movement
        Vector3 randomDirection = Random.insideUnitSphere * 10;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 10, 1);
        Vector3 finalPosition = hit.position;

        if(!_agent.isOnNavMesh)
        {
            return;
        }
        _nextDestinationTime = Time.time + Random.Range(_nextDestinationMinCooldown, _nextDestinationMaxCooldown);
        _agent.SetDestination(finalPosition);   
    }

    public void OnStopMovementPlayed()
    {
        _canMove = false;
        _agent.isStopped = true;
    }

    public void OnStopMovementEnded()
    {
        _canMove = true;
        _agent.isStopped = false;
    }
}
