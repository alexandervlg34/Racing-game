using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private List<Obstacle> obstacles;
    private Score _score;

    private void Awake()
    {
        _score = new Score(obstacles);
    }
}
