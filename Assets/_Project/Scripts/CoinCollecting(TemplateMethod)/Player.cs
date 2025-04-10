using UnityEngine;

public class Player : MonoBehaviour, ICoinCollector
{
    public int Coins { get; private set; }
    public void Add(int value)
    {
        Coins += value; 
    }
}
