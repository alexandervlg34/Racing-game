using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnCooldown;
    [SerializeField] private List<Transform> _spawnPoints;
    
    private Coroutine _spawn;
    
    private EnemyFactory _factory;

    public void Startwork()
    {
        Stopwork();

        _spawn = StartCoroutine(Spawn());
    }
    
    public void Stopwork()
    {
        if (_spawn != null)
        {
            StopCoroutine(_spawn);
        }
    }

    private IEnumerator Spawn()
    {
        EnemyType selectedEnemyType = (EnemyType)Random.Range(0, Enum.GetValues(typeof(EnemyType)).Length);
        Enemy enemy = _factory.Get(selectedEnemyType);
        enemy.MoveTo(_spawnPoints[Random.Range(0, _spawnPoints.Count)].position);
        yield return new WaitForSeconds(_spawnCooldown);
    }
}