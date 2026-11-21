using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Tomato : ItemBase, IHelthSystem
{
    [SerializeField, Header("回復値")] public float _healthAmount;
    public override void ItemUse()
    {
        ModifyHelth(_healthAmount);
        Destroy(gameObject);
    }
    public void ModifyHelth(float amount, float pile = 1)
    {
        _player.ModifyHelth(amount, 1);
    }
}
