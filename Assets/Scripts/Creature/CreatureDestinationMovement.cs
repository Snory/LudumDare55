using UnityEngine;
using UnityEngine.AI;

public class CreatureDestinationMovement : MonoBehaviour
{
    private Transform _destination;
    private NavMeshAgent _navMeshAgent;

    [SerializeField]
    private float _speed;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (_navMeshAgent == null)
            Debug.LogError("NavMeshAgent component not found on " + gameObject.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent.isStopped = false;
    }
 
    public void Init(Transform destination)
    {
        _destination = destination;
        ToDestination();
    }

    private void ToDestination()
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = _speed;
        _navMeshAgent.SetDestination(_destination.position);
    }

    public void OnTriesExceeded()
    {
        ToDestination();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destination"))
        {
            Destroy(this.gameObject);
        }
    }
}
