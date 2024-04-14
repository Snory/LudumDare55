using UnityEngine;
public class CreatureSpawner : MonoBehaviour
{
    [SerializeField] 
    private GameObject _destination;

    [SerializeField]
    private float _nextSpawnTime = 0;

    [SerializeField]
    private float _nextMinSpawnCooldown = 1;

    [SerializeField]
    private float _nextMaxSpawnCooldown = 10;

    [SerializeField]
    private float _currentNumberOfCreatures = 0;

    [SerializeField]   
    private bool _spawnAtStart = false;

    private void Start()
    {
        if(!_spawnAtStart)
        {
            _nextSpawnTime = Time.time + Random.Range(0, _nextMaxSpawnCooldown);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > _nextSpawnTime)
        {
            SpawnCreature();
            _nextSpawnTime = Time.time + Random.Range(_nextMinSpawnCooldown,_nextMaxSpawnCooldown);
        }
    }

    private void SpawnCreature()
    {
        if(_currentNumberOfCreatures >= Pentragram.Instance.GetLevelSetting().MaxCreaturesPerSpawn)
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
