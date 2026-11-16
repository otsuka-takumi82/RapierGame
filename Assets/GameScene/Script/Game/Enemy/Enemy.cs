using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("最大HP")] public float _maxHp;
    public float _currentHp;
    [SerializeField, Header("ダメージ")] public float[] _damage;
    [SerializeField, Header("スピード")] public float[] _speed;
    [SerializeField, Header("弱点")] public GameObject _weakPoints;
    [SerializeField, Header("弱点場所")] public Transform[] _weakPosition;
    Animator _anim;


    [SerializeField, Header("敵HP ")] public Image _enemyHp;
    UIManager _ui;
    Player _player;
    int _weakPosIndex;
    private void Awake()
    {
        _player = FindFirstObjectByType<Player>();
        _ui = FindFirstObjectByType<UIManager>();
        //_anim = GetComponent<Animator>();
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
        if (_currentHp <= 0)
        {
            //敵倒した処理
            gameObject.SetActive(false);
        }
    }
    public void WeakPointsDestroy()
    {
        _weakPosIndex = RandomRun();
        _weakPoints.transform.position = _weakPosition[_weakPosIndex].position;
        
    }
    public int RandomRun()
    {
        int random = Random.Range(0, _weakPosition.Length);
        if (random == _weakPosIndex)
        {
            return RandomRun();
        }
        return random;
    }
}
