using UnityEngine;

public class GhostTransformer : MonoBehaviour
{
    [SerializeField]
    private GeneralEvent _ghostSpawn;

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
            CreatureHealth creatureHealth = other.gameObject.GetComponent<CreatureHealth>();
            CreatureType creatureType = creatureHealth.CreatureType;

            _ghostSpawn.Raise(new GhostSpawnEventArgs(transform.position, creatureType));

            Destroy(gameObject);
        }
    }

}
