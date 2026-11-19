using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Subsystems;

public class EnemyAI : MonoBehaviour
{
    [SerializeField, Header("攻撃時")] public UnityEvent[] _attackEvents;
    [SerializeField, Header("ダメージ判定")] public GameObject _attackCollider;
    Player _player;
    Animator _anim;
    UIManager _ui;
    Enemy _enemy;
    CancellationTokenSource _cts;
    /// <summary>
    /// インデックス0は攻撃できるか1は攻撃してるか
    /// </summary>
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _player = FindFirstObjectByType<Player>();
        //_ui = FindFirstObjectByType<UIManager>();
        _anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = new CancellationTokenSource();
        EnemyAttack(_cts.Token).Forget();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if(_enemy._isActive )
        {
            Move();
        }
    }
    private void OnDestroy()
    {
        NewMesods.StopAsync(ref _cts);
    }
    private void OnDisable()
    {
        NewMesods.StopAsync(ref _cts);
    }

    public async UniTask AttackCol(CancellationToken ct)
    {
        _attackCollider.gameObject.SetActive(true);
        await UniTask.WaitForFixedUpdate(ct);
        _attackCollider.gameObject.SetActive(false);
    }
    public async UniTask EnemyAttack(CancellationToken ct)
    {
        while (true)
        {
            await UniTask.WaitUntil(() => _enemy._isAttack[0], cancellationToken:ct);
            _attackEvents[0].Invoke();
            _enemy._isAttack[1] = true;
            await UniTask.WaitForSeconds(2f, cancellationToken:ct);
            _enemy._isAttack[1] = false;
        }
    }
    public void Attack()
    {
        if (_enemy._isAttack[0])
        {
            //var ct = this.GetCancellationTokenOnDestroy();
            AttackCol(_cts.Token).Forget();
        }
    }
    public void Move()
    {

        Vector3 direction = _player.transform.position.ChangeVector3(y: 0) - transform.position.ChangeVector3(y: 0);
        Quaternion targetPos = Quaternion.LookRotation(direction);
        float distance = (_player.transform.position - transform.position).magnitude;
        if (distance >= 5)
        {
            //範囲外
            _enemy._isAttack[0] = false;
        }
        else
        {
            //範囲内
            _enemy._isAttack[0] = true;
        }
        if (!_enemy._isAttack[1])
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetPos, 0.1f);
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _enemy._speed[0] / 100);
        }
    }
}
