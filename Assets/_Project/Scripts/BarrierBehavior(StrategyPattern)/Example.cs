using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    [SerializeField] private Barrier _barrier;
    [SerializeField] private Transform _target;
    [SerializeField] private List<Transform> _patrolPoints;

    private void Awake()
    {
        _barrier.SetMover(new NoMovePattern());
    }

    private void Update()
    {
        
    }
}