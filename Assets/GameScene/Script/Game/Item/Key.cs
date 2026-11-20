using UnityEngine;

public class Key : ItemBase, IKey,IRapid
{
    [SerializeField, Header("飛距離")] public float _force;
    public Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            //ドアを開く処理
            KeyOpen("NomalDoor");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Floor"))
        {
            gameObject.layer = 0;
        }
    }
    public override void ItemUse()
    {
        //transform.LookAt(_player.transform.forward);
    }
    public void KeyOpen(string str)
    {
        
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
