using UnityEngine;

public class Censor : MonoBehaviour
{
    Enemy _en;
    EnemyAI _ai;
    private void Awake()
    {
        _en = transform.root.GetComponentInChildren<Enemy>();
        _ai = transform.root.GetComponentInChildren<EnemyAI>();
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
        if(other.gameObject.CompareTag("PlayerTag"))
        {
            Ray ray = new Ray(transform.position, other.transform.position - transform.position);
            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.CompareTag("PlayerTag"))
                {
                    _ai._isActive = true;
                }
            }
        }
    }
}
