using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class DyrahanAI : AttackAIBase
{
    public float _attackNum;
    //攻撃の頻度
    public override async UniTask EnemyAttack(CancellationToken ct)
    {
        while (true)
        {
            await UniTask.WaitUntil(() => _enemy._isAttack[0], cancellationToken: ct);
            if(_attackNum == 0)
            {
                _attackEvents[0].Invoke();
            }
            else if( _attackNum == 1)
            {
                _attackEvents[1].Invoke();
            }
            if( _attackNum >= 1)
            {
                _attackNum = 0;
            }
            else
            {
                _attackNum++;
            }
            _enemy._isAttack[1] = true;
            await UniTask.WaitForSeconds(1, cancellationToken: ct);
            _enemy._isAttack[1] = false;
        }
    }
    //アニメーションで呼ばれる攻撃のメソッド
    public override void Attack()
    {
        if (_enemy._isAttack[0])
        {
            //var ct = this.GetCancellationTokenOnDestroy();
            AttackCol(_cts.Token, 0).Forget();
        }
    }
    public void AttackOne()
    {
        if (_enemy._isAttack[0])
        {
            //var ct = this.GetCancellationTokenOnDestroy();
            AttackCol(_cts.Token, 1).Forget();
        }
    }
    public void AttackTwo()
    {
        if (_enemy._isAttack[0])
        {
            AttackCol(_cts.Token, 2).Forget();
        }
    }
}
