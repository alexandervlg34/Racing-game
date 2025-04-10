using System;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    public event Action<Obstacle> Destroyed;
    
    public abstract void Accept(IObstacleVisitor obstacleVisitor);
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger entered by {other.name}");
        if (other.TryGetComponent(out IObstacleDestroyer obstacleDestroyer))
        {
            Debug.Log($"Destroying {gameObject.name}");
            Destroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
    
}