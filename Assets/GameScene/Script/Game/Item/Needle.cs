using Unity.VisualScripting;
using UnityEngine;

public class Needle : ItemBase, IRapid
{
    [SerializeField, Header("飛距離")] public float _force;
    [SerializeField, Header("ダメージ")] public float _healthAmount;
    public Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().EnemyHelth(-_healthAmount);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Floor"))
        {
            gameObject.layer = 0;
        }
    }
    public override void ItemUse()
    {
        //transform.LookAt(_player.transform.forward);
    }
    public void RapidAction(Vector3 v)
    {
        transform.SetParent(null, true);
        GetComponent<Collider>().isTrigger = false;
        _rb.isKinematic = false;
        transform.LookAt(transform.position += v);
        _rb.AddForce(v * _force, ForceMode.Impulse);
    }
}
