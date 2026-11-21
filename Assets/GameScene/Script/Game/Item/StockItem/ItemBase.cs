using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public enum BakedState
    {
        Raw,
        Bake
    }

    [HideInInspector]public Player _player;
    private void Awake()
    {
        _player = FindFirstObjectByType<Player>();
    }
    public virtual void ItemUse()
    {
        Debug.Log("OverRideして！");
    }
}
