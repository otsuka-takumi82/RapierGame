using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Exprotion : MonoBehaviour
{
    [SerializeField,Header("ダメージ")]float _damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject,2);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Enemy en = other.gameObject.GetComponent<Enemy>();
            en._isActive = true;
            en?.ModifyHelth(-20f);
        }
    }
}
