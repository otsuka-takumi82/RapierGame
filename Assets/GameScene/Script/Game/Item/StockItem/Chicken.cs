using System;
using UnityEngine;

public class Chicken : ItemBase, IHelthSystem
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
            ModifyHelth(-_healthAmount);
        }
        else if (_state == BakedState.Bake)
        {
            ModifyHelth(_healthAmount);
        }
        Destroy(gameObject);
    }
    public void ModifyHelth(float amount, float pile = 1)
    {
        _player.ModifyHelth(amount, 1);
    }
    public void ChangeColor()
    {
        GetComponentInChildren<Renderer>().material.color = Color.brown;
    }
}
