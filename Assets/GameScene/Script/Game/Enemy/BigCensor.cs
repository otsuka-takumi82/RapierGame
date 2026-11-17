using TMPro;
using UnityEngine;

public class BigCensor : MonoBehaviour
{
    Enemy _en;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _en = transform.root.GetComponentInChildren<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        _en._isActive = false;
        _en._isAttack[0] = false;
    }
}
