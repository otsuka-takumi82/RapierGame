using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("最大HP")] public float _maxHp;
    public float _currentHp;
    [SerializeField, Header("敵HP ")] public Image _enemyHp;
    UIManager _ui;
    private void Awake()
    {
        _ui = FindFirstObjectByType<UIManager>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void OnDisable()
    {
        _currentHp = _maxHp;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnemyHelth(float amount, float pile = 1)
    {
        _currentHp = Mathf.Clamp(_currentHp + amount * pile, 0, _maxHp);
        _ui.HP(_enemyHp, _currentHp, _maxHp);
        if(_currentHp <= 0)
        {
            //敵倒した処理
            gameObject.SetActive(false);
        }
    }
}
