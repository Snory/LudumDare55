using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class PoissionSampleDiskSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _whatToSpawn;

    [SerializeField]
    private LayerMask _samePrefabMask;

    [SerializeField]
    private float _spawnMinCoolDown = 5f, _spawnMaxCoolDown = 10f;

    [SerializeField]
    private float _nextSpawnTime = 0f;

    [SerializeField]
    private float _spawnRadius = 10f, _spawnFromOtherDistance = 5f;

    [SerializeField]
    private bool _canSpawn;

    private void Start()
    {
        _nextSpawnTime = Time.time + Random.Range(_spawnMinCoolDown, _spawnMaxCoolDown);
    }

    // Update is called once per frame
    void Update()
    {
        if(!_canSpawn)
        {
            return;
        }

        // select random position on navmesh and spawn summoning circle

        if (Time.time >= _nextSpawnTime)
        {
            Vector3 randomPosition = GetRandomPositionOnNavMesh();
            _nextSpawnTime = Time.time + Random.Range(_spawnMinCoolDown, _spawnMaxCoolDown);

            if (randomPosition == Vector3.zero)
            {
                return;
            }

            Instantiate(_whatToSpawn, randomPosition, Quaternion.identity);
            StopSpawning();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _spawnRadius);
    }

    private Vector3 GetRandomPositionOnNavMesh()
    {
        //20 is generate test
        for (int i = 0; i < 20; i++)
        {
            float randomAngle = Mathf.PI * 2 * Random.Range(0f, 1f);
            float x = Mathf.Cos(randomAngle) * Random.Range(0f, _spawnRadius);
            float z = Mathf.Sin(randomAngle) * Random.Range(0f, _spawnRadius);

            Vector3 positionCandidate = new Vector3(x, this.transform.position.y, z) + this.transform.position;

            Collider[] overlaps = Physics.OverlapSphere(positionCandidate, _spawnFromOtherDistance, _samePrefabMask);

            if (overlaps.Length > 0)
            {
                continue;
            }

            NavMeshHit hit;
            bool found = NavMesh.SamplePosition(positionCandidate, out hit, 0.1f, 1);

            if (found)
            {
                return hit.position;
            }
        }

        return Vector3.zero;
    }

    public void StopSpawning()
    {
        _canSpawn = false;
    }

    public void StartSpawning()
    {
        _canSpawn = true;
        _nextSpawnTime = Time.time + Random.Range(_spawnMinCoolDown, _spawnMaxCoolDown);
    }
}
