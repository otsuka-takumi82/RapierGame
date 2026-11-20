using System;
using UnityEngine;

public class Chicken : ItemBase,IHealable
{
    [SerializeField,Header("回復値")]public float _healthAmount;
    public override void ItemUse()
    {
        Heal(_healthAmount);
    }
    public void Heal(float amount)
    {
        _player.ModifyHelth(amount, 1);
    }
}
