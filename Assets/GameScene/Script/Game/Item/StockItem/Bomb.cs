using UnityEngine;

public class Bomb : ItemBase
{
    [SerializeField, Header("爆発")] public GameObject _exprotion;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Exprotion();
        }
    }
    public override void ItemUse()
    {
        //Exprotion();
    }
    public void Exprotion()
    {
        Instantiate(_exprotion,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

