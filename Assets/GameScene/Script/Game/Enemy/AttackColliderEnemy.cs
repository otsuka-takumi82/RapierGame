using System.Collections;
using UnityEngine;

public class AttackColliderEnemy : MonoBehaviour
{
    Enemy _enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _enemy = transform.root.GetComponentInChildren<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerTag"))
        {
            Debug.Log("a");
            other.GetComponent<Player>()?.ModifyHelth(-_enemy._damage[0], 1);
        }
    }
    
}
