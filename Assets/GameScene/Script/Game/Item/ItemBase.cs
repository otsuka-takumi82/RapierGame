using UnityEngine;

public class ItemBase : MonoBehaviour
{
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
