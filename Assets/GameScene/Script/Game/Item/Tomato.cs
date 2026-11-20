using UnityEngine;

public class Tomato : ItemBase, IHealable
{
    [SerializeField, Header("回復値")] public float _healthAmount;
    public override void ItemUse()
    {
        Heal(_healthAmount);
        Destroy(gameObject);
    }
    public void Heal(float amount)
    {
        _player.ModifyHelth(amount, 1);
    }
}
