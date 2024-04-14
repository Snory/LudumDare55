using UnityEngine;

public class GhostTransformer : MonoBehaviour
{
    [SerializeField]
    private GeneralEvent _ghostSpawn;

    [SerializeField]
    private float _transformProtectionCooldown = 1f;
   
    private float _nextPossibleTransformTime = 0;

    private void Awake()
    {
        _nextPossibleTransformTime = Time.time + _transformProtectionCooldown;
    }

    private void Start()
    {
        if (_ghostSpawn == null)
        {
            Debug.LogError("GhostSpawn event is not set in GhostTransformer");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Creature"))
        {
            if (Time.time < _nextPossibleTransformTime)
            {
                return;
            }

            CreatureHealth creatureHealth = other.gameObject.GetComponent<CreatureHealth>();
            CreatureNudgeMovement creatureNudgeMovement = other.gameObject.GetComponent<CreatureNudgeMovement>();

            if (!creatureNudgeMovement.IsNudged())
            {
                return;
            }

            CreatureType creatureType = creatureHealth.CreatureType;

            _ghostSpawn.Raise(new GhostSpawnEventArgs(transform.position, creatureType));

            Destroy(gameObject);
        }
    }

}
