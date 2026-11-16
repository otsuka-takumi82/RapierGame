using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyAI : MonoBehaviour
{
    [SerializeField, Header("攻撃時")] public UnityEvent[] _attackEvents;
    [SerializeField, Header("ダメージ判定")] public GameObject _attackCollider;
    Player _player;
    Animator _anim;
    UIManager _ui;
    Enemy _enemy;
    public bool _isActive;
    /// <summary>
    /// インデックス0は攻撃できるか1は攻撃してるか
    /// </summary>
    public bool[] _isAttack = new bool[2];
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _player = FindFirstObjectByType<Player>();
        //_ui = FindFirstObjectByType<UIManager>();
        _anim = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(EnemyAttack());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_isAttack[0]);
    }
    private void FixedUpdate()
    {
        if( _isActive )
        {
            Move();
        }
    }
    public IEnumerator AttackCol()
    {
        _attackCollider.gameObject.SetActive(true);
        yield return new WaitForFixedUpdate();
        _attackCollider.gameObject.SetActive(false);
    }
    public IEnumerator EnemyAttack()
    {
        while (true)
        {
            yield return new WaitUntil(() => _isAttack[0]);
            _attackEvents[0].Invoke();
            _isAttack[1] = true;
            yield return new WaitForSeconds(2f);
            _isAttack[1] = false;
        }
    }
    public void Attack()
    {
        if (_isAttack[0])
        {
            StartCoroutine(AttackCol());
        }
    }
    public void Move()
    {

        Vector3 direction = _player.transform.position.ChangeVector3(y: 0) - transform.position.ChangeVector3(y: 0);
        Quaternion targetPos = Quaternion.LookRotation(direction);
        float distance = (_player.transform.position - transform.position).magnitude;
        Debug.Log(direction.y);
        if (distance >= 5)
        {
            //範囲外
            _isAttack[0] = false;
        }
        else
        {
            //範囲内
            _isAttack[0] = true;
        }
        if (!_isAttack[1])
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetPos, 0.1f);
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _enemy._speed[0] / 100);
        }
    }
}
