using TMPro;
using UnityEngine;

public class BigCensor : MonoBehaviour
{
    [SerializeField] Enemy _en;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerTag"))
        {

            _en._isActive = false;
            _en._isAttack[0] = false;
        }
    }
}
