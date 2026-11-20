using System;
using UnityEngine;

public class Chicken : ItemBase, IHealable
{
    [SerializeField, Header("肉は焼けているか")] public BakedState _state = BakedState.Raw;
    [SerializeField,Header("回復値")]public float _healthAmount;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Fire"))
        {
            _state = BakedState.Bake;
            ChangeColor();
        }
    }
    public override void ItemUse()
    {
        if( _state == BakedState.Raw)
        {
            Heal(-_healthAmount);
        }
        else if (_state == BakedState.Bake)
        {
            Heal(_healthAmount);
        }
        Destroy(gameObject);
    }
    public void Heal(float amount)
    {
        _player.ModifyHelth(amount, 1);
    }
    public void ChangeColor()
    {
        GetComponentInChildren<Renderer>().material.color = Color.brown;
    }
}
