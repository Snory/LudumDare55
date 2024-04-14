using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreatureSpawnerManager : Singleton<CreatureSpawnerManager>
{
    private Dictionary<CreatureType, int> _creatureInGameCount;
    private Dictionary<CreatureType, int> _creatureSpawnCount;

    [SerializeField]    
    private GameObject _creaturePrefab;

    [SerializeField]
    private GameObject _destination;

    [SerializeField]
    private int _fullfillmentReserve;

    private int _wholeHistoryCount;
    private FullFillment _currentFullfillment;

    [SerializeField]
    private GeneralEvent _gameOver;

    private void Start()
    {
        _creatureInGameCount = new Dictionary<CreatureType, int>();
    }

    public GameObject GetCreaturePrefab()
    {
        return _creaturePrefab;
    }

    public GameObject GetDestination()
    {
        return _destination;
    }

    public CreatureType GetNextCreatureType()
    {
        if (_currentFullfillment == null)
        {
            return null;
        }

        if (IsFullfillmentReached())
        {
            CheckGameOver();
            return null;
        }

        int maxCount = int.MinValue;
        foreach (var creatureType in _creatureSpawnCount.Keys)
        {
            if (_creatureSpawnCount[creatureType] > maxCount)
            {
                maxCount = _creatureSpawnCount[creatureType];
            }
        }

        var possibleCreaturues = _creatureSpawnCount.Where(x => _creatureSpawnCount[x.Key] == maxCount).Select(x => x.Key).ToList();

        return possibleCreaturues[Random.Range(0, possibleCreaturues.Count)];
    }

    private bool IsFullfillmentReached()
    {
        var fullfillmentValue = _currentFullfillment.GetMaxFullFillment();
        return _wholeHistoryCount >= (fullfillmentValue + _fullfillmentReserve);
    }

    private void CheckGameOver()
    {
        var creatureNeeded = _currentFullfillment.GetNextSacrifices();

        foreach (var creatureType in creatureNeeded)
        {
            // If there are no more creatures to spawn and no more creatures in game
            if(_creatureSpawnCount[creatureType.CreatureType] <= 0 && _creatureInGameCount[creatureType.CreatureType] == 0)
            {
                _gameOver.Raise();
                return;
            }
        }
    }

    public void OnSpawned(EventArgs args)
    {
        CreatureEventArgs creatureArgs = (CreatureEventArgs)args;
        _wholeHistoryCount++;

        if (!_creatureInGameCount.ContainsKey(creatureArgs.CreatureType))
        {
            _creatureInGameCount.Add(creatureArgs.CreatureType, 0);
        }

        if (!creatureArgs.GhostSpawn)
        {
            _creatureSpawnCount[creatureArgs.CreatureType]--;
        }

        _creatureInGameCount[creatureArgs.CreatureType]++;
    }

    public void OnDeath(EventArgs args)
    {
        CreatureEventArgs creatureArgs = (CreatureEventArgs)args;
        _creatureInGameCount[creatureArgs.CreatureType]--;

        if(IsFullfillmentReached())
        {
            CheckGameOver();
        }
    }

    public void OnNewFullfillment(EventArgs args)
    {
        FullFillmentEventArgs fullFillmentArgs = (FullFillmentEventArgs)args;
        _currentFullfillment = fullFillmentArgs.CurrentFullfillment;
        _wholeHistoryCount = 0;

        _creatureSpawnCount = new Dictionary<CreatureType, int>();

        foreach (var creatureType in _currentFullfillment.FullfillmentItems)
        {
            if (!_creatureSpawnCount.ContainsKey(creatureType.CreatureType))
            {
                _creatureSpawnCount.Add(creatureType.CreatureType, 0);
            }

            _creatureSpawnCount[creatureType.CreatureType] += creatureType.Amount;
        }
    }

    public int GetMaxSpawnCount()
    {
        return Pentragram.Instance.GetCurrentFullFillmentIndex() + Pentragram.Instance.GetLevelSetting().MaxCreaturesPerSpawn;
    }

    public void OnGhostSpawn(EventArgs args)
    {
        GhostSpawnEventArgs ghostSpawnArgs = (GhostSpawnEventArgs)args;

        GameObject newCreatureObject = Instantiate(_creaturePrefab, ghostSpawnArgs.TargetPosition, Quaternion.identity);
        CreatureHealth newCreatureHealth = newCreatureObject.GetComponent<CreatureHealth>();
        newCreatureHealth.Init(ghostSpawnArgs.SourceCreatureType, true);
        newCreatureObject.GetComponentInChildren<CreatureDestinationMovement>().Init(_destination.transform);
    }
}
