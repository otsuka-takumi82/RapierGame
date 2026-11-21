using UnityEditor.Search;
using UnityEngine;

public class Eye : MonoBehaviour
{
    [SerializeField]public float _speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, _speed, 0);
    }
    public void Look(Vector3 rotatePos)
    {
        transform.rotation = Quaternion.LookRotation(-rotatePos);
    }
    public void ZeroOrOne()
    {
        if(_speed == 0)
        {
            _speed = 1;
        }
        else if(_speed == 1) 
        {
            _speed = 0;
        }
    }
}
