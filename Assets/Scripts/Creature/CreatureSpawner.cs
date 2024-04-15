using UnityEngine;
public class CreatureSpawner : MonoBehaviour
{
    [SerializeField]
    private BoolVariable _tutorialProgressVariable;

    [SerializeField] 
    private GameObject _destination;

    [SerializeField]
    private float _nextSpawnTime = 0;

    [SerializeField]
    private float _currentNumberOfCreatures = 0;

    [SerializeField]   
    private bool _spawnAtStart = false;

    private void Start()
    {
        if(!_spawnAtStart)
        {
            var (min, max) = CreatureSpawnerManager.Instance.GetSpawnCooldown();

            _nextSpawnTime = Time.time + Random.Range(0, max);
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if(Time.time > _nextSpawnTime)
        {
            SpawnCreature();

            var (min, max) = CreatureSpawnerManager.Instance.GetSpawnCooldown();
            _nextSpawnTime = Time.time + Random.Range(min, max) + CreatureSpawnerManager.Instance.GetGuaranteedCooldownMinimum();
        }
    }

    private void SpawnCreature()
    {
        if(_currentNumberOfCreatures >= CreatureSpawnerManager.Instance.GetMaxSpawnCount())
        {
            return;
        }

        var creatureType = CreatureSpawnerManager.Instance.GetNextCreatureType();
        
        if(creatureType == null)
        {
            return;
        }
        _currentNumberOfCreatures++;


        GameObject creatureObject = Instantiate(CreatureSpawnerManager.Instance.GetCreaturePrefab(), this.transform.position, this.transform.rotation);
        creatureObject.GetComponentInChildren<CreatureDestinationMovement>().Init(CreatureSpawnerManager.Instance.GetDestination().transform);

        CreatureHealth creature = creatureObject.GetComponent<CreatureHealth>();
        creature.Init(creatureType);
        creature.OnDeath.AddListener(OnCreatureDeath);
    }

    public void OnCreatureDeath()
    {
        _currentNumberOfCreatures--;
    }
}
