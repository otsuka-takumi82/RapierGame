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
        if(collision.gameObject.CompareTag("Eye"))
        {
            Eye ey = collision.gameObject.GetComponent<Eye>();
            ey.ZeroOrOne();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy en = collision.gameObject.GetComponent<Enemy>();
            en._isActive = true;
            en.ModifyHelth(-_healthAmount);
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
        transform.rotation = Quaternion.LookRotation(v);
        _rb.AddForce(v * _force, ForceMode.Impulse);
    }
}
