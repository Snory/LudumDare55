using UnityEngine;
using UnityEngine.AI;

public class DemonRandomMovement : MonoBehaviour
{
    private NavMeshAgent _agent;

    private float _nextDestinationTime = 0;

    [SerializeField]
    private float _nextDestinationMinCooldown = 1;
    [SerializeField]
    private float _nextDestinationMaxCooldown = 5;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            Debug.LogError("NavMeshAgent component not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent.isStopped)
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

    public void OnFollowMovementStart()
    {
        _agent.isStopped = true;
    }

    public void OnFollowMovementEnd()
    {
        _agent.isStopped = false;
        SetRandomDestination();
    }

    private void SetRandomDestination()
    {
        //random movement
        Vector3 randomDirection = Random.insideUnitSphere * 10;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 10, 1);
        Vector3 finalPosition = hit.position;

        if (!_agent.isOnNavMesh)
        {
            return;
        }
        _nextDestinationTime = Time.time + Random.Range(_nextDestinationMinCooldown, _nextDestinationMaxCooldown);
        _agent.SetDestination(finalPosition);
    }
}
