using UnityEngine;
public class CreatureSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _creatureToSpawn;

    [SerializeField] 
    private GameObject _destination;

    [SerializeField]
    private float _nextSpawnTime = 0;

    [SerializeField]
    private float _nextMinSpawnCooldown = 1;

    [SerializeField]
    private float _nextMaxSpawnCooldown = 10;

    [SerializeField]
    private float _maximumNumberOfCreatures = 6;

    [SerializeField]
    private float _currentNumberOfCreatures = 0;

    private void Start()
    {
        _nextSpawnTime = Time.time + Random.Range(0, _nextMaxSpawnCooldown);
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
        if(_currentNumberOfCreatures >= _maximumNumberOfCreatures)
        {
            return;
        }

        GameObject creatureObject = Instantiate(_creatureToSpawn, this.transform.position, this.transform.rotation);
        creatureObject.GetComponentInChildren<CreatureDestinationMovement>().Init(_destination.transform);
        _currentNumberOfCreatures++;

        CreatureHealth creature = creatureObject.GetComponent<CreatureHealth>();
        creature.OnDeath.AddListener(OnCreatureDeath);
    }

    public void OnCreatureDeath()
    {
        _currentNumberOfCreatures--;
    }
}
