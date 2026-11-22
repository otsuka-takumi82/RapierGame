using UnityEngine;
using UnityEngine.Events;
using System.Threading;
using Cysharp.Threading.Tasks;

public class AttackAI : AttackAIBase
{
    public override async UniTask EnemyAttack(CancellationToken ct)
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
    public override void Attack()
    {
        if (_enemy._isAttack[0])
        {
            //var ct = this.GetCancellationTokenOnDestroy();
            AttackCol(_cts.Token).Forget();
        }
    }
}
