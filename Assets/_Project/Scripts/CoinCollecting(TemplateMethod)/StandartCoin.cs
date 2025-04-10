using UnityEngine;

public class StandartCoin : Coin
{
    [SerializeField, Range(0, 50)] private int _value;
    protected override void AddCoins(ICoinCollector coinCollector) => coinCollector.Add(_value);
}