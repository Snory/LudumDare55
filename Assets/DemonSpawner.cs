using UnityEngine;

public class DemonSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _demonPrefab;

    [SerializeField]
    private float _spawnerTime;

    [SerializeField]
    private float _spawnerCoolMinTime = 15, _spawnerCoolmaxTime = 30;

    [SerializeField]
    private int _maxDemons = 8;

    [SerializeField]
    private int _countOfDemons;

    private void Awake()
    {
        _spawnerTime = Time.time + Random.Range(0, _spawnerCoolmaxTime);
    }

    private void Update()
    {
        if(Time.time > _spawnerTime)
        {
            SpawnDeamon();
            _spawnerTime = Time.time + Random.Range(_spawnerCoolMinTime, _spawnerCoolmaxTime);
        }
    }

    public void OnWrongSacrifice()
    {
        SpawnDeamon();
    }

    private void SpawnDeamon()
    {
        if(_demonPrefab == null)
        {
            return;
        }

        if(_countOfDemons >= _maxDemons)
        {
            return;
        }

        _countOfDemons++;

        GameObject demonObject = Instantiate(_demonPrefab, this.transform.position, Quaternion.identity);
    }
}
    