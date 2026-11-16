using TMPro;
using UnityEngine;

public class BigCensor : MonoBehaviour
{
    EnemyAI _ai;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _ai = GetComponentInParent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        _ai._isActive = false;
        _ai._isAttack[0] = false;
    }
}
