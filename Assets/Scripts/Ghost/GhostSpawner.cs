using System;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField]
    private float _spawnCooldown = 0;

    [SerializeField]
    private GameObject _ghostPrefab;    

    private float _nextTimeToSpawn = 0;

    private void Start()
    {
        SetTimer();
    }

    private void Update()
    {
        if (Time.time >= _nextTimeToSpawn)
        {
            SpawnGhost();
        }
    }

    private void SetTimer()
    {
        _nextTimeToSpawn = Time.time + _spawnCooldown;
    }

    private void SpawnGhost()
    {
        GameObject gameObject = Instantiate(_ghostPrefab, transform.position, Quaternion.identity);
        SetTimer();
    }

    public void OnWrongSacrifice(EventArgs eventArgs)
    {
        SpawnGhost();
    }
}
