using UnityEngine;
using UnityEngine.Events;
using System.Threading;
using Cysharp.Threading.Tasks;

public class AttackAIBase : MonoBehaviour
{
    [SerializeField, Header("攻撃時")] public UnityEvent[] _attackEvents;
    [SerializeField, Header("ダメージ判定")] public GameObject[] _attackCollider;
    public Player _player;
    Animator _anim;
    UIView _ui;
    public Enemy _enemy;
    public CancellationTokenSource _cts;
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
    //コライダーのつけ消し
    public virtual async UniTask AttackCol(CancellationToken ct, int num = 0)
    {
        _attackCollider[num].SetActive(true);
        await UniTask.WaitForFixedUpdate(ct);
        _attackCollider[num].SetActive(false);
    }
    //攻撃の頻度
    public virtual async UniTask EnemyAttack(CancellationToken ct)
    {
        Debug.Log("オーバライドしろ");
    }
    //攻撃自体のメソッド
    public virtual void Attack()
    {
        Debug.Log("オーバライドしろ");
    }
}
