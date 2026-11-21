using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Subsystems;

public class ChaseAI : MonoBehaviour
{
    Player _player;
    UIView _ui;
    Enemy _enemy;
    //CancellationTokenSource _cts;
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _player = FindFirstObjectByType<Player>();
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
