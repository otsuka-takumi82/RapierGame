using UnityEngine;
using UniRx;

public class UIPresenter : MonoBehaviour
{
    public UIView _view;
    public Player _player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player._enemyDisabled.Subscribe((count) =>
        {
            _view.EnemyDisabled(count);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
