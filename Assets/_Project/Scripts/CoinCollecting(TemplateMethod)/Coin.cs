using UnityEngine;

public abstract class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICoinCollector  coinCollector))
        {
            AddCoins(coinCollector);
            Destroy(gameObject);
        }
    }
    protected abstract void AddCoins(ICoinCollector coinCollector);
} 