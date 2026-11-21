using UnityEngine;

public class Mist : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerTag"))
        {
            other.GetComponent<Player>().ModifyHelth(30);
        }
    }
}
