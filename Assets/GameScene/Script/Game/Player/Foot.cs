using UnityEngine;

public class Foot : MonoBehaviour
{
    private void Awake()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Floor"))
        {
            Player.Instance._isGrounded = 0;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            Player.Instance._isGrounded++;
        }
    }
}
