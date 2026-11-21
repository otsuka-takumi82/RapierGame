using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField, Header("回復霧")]public GameObject _mist;
    public void SpornMist()
    {
        Instantiate(_mist,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
