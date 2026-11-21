using UnityEngine;
using UnityEngine.Events;
using System.Threading;
using Cysharp.Threading.Tasks;

public class AttackAI : MonoBehaviour
{
    [SerializeField, Header("攻撃時")] public UnityEvent[] _attackEvents;
    [SerializeField, Header("ダメージ判定")] public GameObject _attackCollider;
    Player _player;
    Animator _anim;
    UIView _ui;
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
            await UniTask.WaitUntil(() => _enemy._isAttack[0], cancellationToken: ct);
            _attackEvents[0].Invoke();
            _enemy._isAttack[1] = true;
            await UniTask.WaitForSeconds(2f, cancellationToken: ct);
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
}
