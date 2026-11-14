using System;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    protected override bool dontDestroyOnLoad { get { return true; } }
    public Action<bool> _pauseReseum = default;
    public bool _isPaused;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            PauseReseum();
        }
    }
    public void PauseReseum()
    {
        _isPaused = !_isPaused;
        if(_pauseReseum != null)
        {
            _pauseReseum(_isPaused);
        }
    }
}
