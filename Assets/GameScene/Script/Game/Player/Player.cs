using System.Collections;
using UniRx;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    [SerializeField, Header("射程距離")] public float _rapierDistance;
    [SerializeField, Header("アイテムの場所")] public Transform[] _itemStock;
    //[SerializeField, Header("刺したアイテム")] public GameObject[] _rapidItem;
    [SerializeField, Header("攻撃力")] public float[] _damage;
    [SerializeField, Header("最大HP")] public float _maxHp;
    public float _currentHp; 
    [SerializeField, Header("カーソル")] public Image _hpImage;
    /// <summary>
    /// インデックス0は速度、1は一時保存用
    /// </summary>
    [SerializeField, Header("マウス速度")] public float[] _mouseSpeed;
    [SerializeField, Header("ジャンプ速度")] public float _jumpSpeed = 1;
    [SerializeField, Header("速度")] public float[] _speed;
    [SerializeField, Header("縦速度")] public float _verticalSpeed;
    [SerializeField, Header("重力速度")] public float _gravity;
    [SerializeField, Header("カーソル")] public Image _cousle;
    [SerializeField]Transform _camera;
    public IntReactiveProperty _enemyDisabled = new IntReactiveProperty(0);
    float _sideSpeed;
    float _forwardSpeed;
    public float _isGrounded;
    CharacterController _characterController;
    Rigidbody _rb;
    Animator _anim;
    UIView _ui;
    Vector3 _rapierHitPosition;
    bool _isJump;
    int _layer;
    private void Awake()
    {
        _layer = LayerMask.NameToLayer("Ignore Raycast");
        _ui = FindFirstObjectByType<UIView>();
        _anim = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _rb = GetComponent<Rigidbody>();
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void OnEnable()
    {
        GameManager.Instance._pauseReseum += PauseReseum;
        
    }
    private void OnDestroy()
    {
        _enemyDisabled.Dispose();
        if(Instance == this)
        {
            Instance = null;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _sideSpeed = Input.GetAxisRaw("Horizontal") * _speed[0];
        _forwardSpeed = Input.GetAxisRaw("Vertical") * _speed[0];
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        _sideSpeed = Input.GetAxisRaw("Horizontal") * _speed[0];
        _forwardSpeed = Input.GetAxisRaw("Vertical") * _speed[0];
        if (Input.GetButtonDown("Jump"))
        {
            _isJump = true;
        }
        //レイピア溜め
        RapierCharge();
        //レイピア突き
        RapierRapid();
        //横回転
        MouseRotateX();
    }
    private void FixedUpdate()
    {
        //キャラクターコントローラー関係
        Move();
    }
    public void ModifyHelth(float amount, float pile)
    {
        _currentHp = Mathf.Clamp(_currentHp + amount * pile, 0, _maxHp);
        _ui.HP(_hpImage, _currentHp, _maxHp);
        if (_currentHp <= 0)
        {
            //敵倒した処理
            gameObject.SetActive(false);
        }
    }
    public void MouseRotateX()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * _mouseSpeed[0], 0);
    }
    public void Move()
    {
        if(_characterController.isGrounded)
        {
            _verticalSpeed = -1f;
            if(_isJump)
            {
                _verticalSpeed = _jumpSpeed;
            }
            _isJump = false;
        }
        _verticalSpeed += Time.deltaTime * _gravity;
        Vector3 moveMent = new Vector3(_sideSpeed, _verticalSpeed, _forwardSpeed) * Time.deltaTime;
        moveMent = transform.rotation * moveMent;
        _characterController.Move(moveMent);
    }
    public void RapierCharge()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _anim.SetBool("Charge", true);
            _speed[1] = _speed[0];
            _speed[0] *= 0.5f;
            _mouseSpeed[1] = _mouseSpeed[0];
            _mouseSpeed[0] *= 0.5f;
            //アイテム使用
            _itemStock[2].GetComponentInChildren<ItemBase>()?.ItemUse();
            if (_itemStock[2].childCount == 0)
            {
                Debug.Log("Charge");
                if (_itemStock[1].childCount > 0)
                    ItemMove(_itemStock[1].GetChild(0), _itemStock[1 + 1]);
                if (_itemStock[0].childCount > 0)
                    ItemMove(_itemStock[0].GetChild(0), _itemStock[0 + 1]);
            }
            
        }
        RapierDistance();
    }
    public void RapierRapid()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _anim.SetBool("Charge", false);
            _speed[0] = _speed[1];
            _mouseSpeed[0] = _mouseSpeed[1];
            if (_itemStock[2].GetComponentInChildren<ItemBase>() is IRapid rapid)
            {
                rapid.RapidAction(_camera.transform.forward);
            }
        }
    }
    public void RapierCollider()
    {
        Ray ray = new Ray(_camera.position, _camera.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * _rapierDistance, Color.red);
        _rapierHitPosition = _camera.position + _camera.forward * _rapierDistance;
        if (Physics.Raycast(ray, out hit, _rapierDistance))
        {
            _rapierHitPosition = hit.point;
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                Enemy em = hit.collider.GetComponent<Enemy>();
                em?.EnemyHelth(-_damage[0], 1f);
                em._isActive = true;
            }
            else if (hit.collider.gameObject.CompareTag("WeakPoints"))
            {
                Enemy em = hit.collider.GetComponentInParent<Enemy>();
                em?.EnemyHelth(-_damage[0], 2f);
                em?.WeakPointsDestroy();
                em._isActive=true;
            }
            else if (hit.collider.gameObject.CompareTag("Item"))
            {
                for(int i = 0; i < _itemStock.Length; i++)
                {
                    if (_itemStock[i].childCount == 0)
                    {
                        hit.collider.isTrigger = true;
                        hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        hit.collider.gameObject.layer = _layer;
                        ItemMove(hit.collider.transform, _itemStock[i]);
                        break;
                    }
                }
            }
        }
    }
    public void ItemMove(Transform children, Transform pearent)
    {
        children.position = pearent.position;
        children.SetParent(pearent.transform, true);
    }
    public void RapierDistance()
    {
        Ray ray = new Ray(_camera.position, _camera.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * _rapierDistance, Color.blue);
        _rapierHitPosition = _camera.position + _camera.forward * _rapierDistance;
        if (Physics.Raycast(ray, out hit, _rapierDistance))
        {
            _rapierHitPosition = hit.point;
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                _cousle.color = Color.yellow;
            }
            else if (hit.collider.gameObject.CompareTag("WeakPoints"))
            {
                _cousle.color = Color.red;
            }
        }
        else
        {
            _cousle.color = Color.white;
        }
    }
    public void PauseReseum(bool isPaused)
    {
        if(isPaused)
        {

        }
        else
        {

        }
    }
    //public void Jump()
    //{
    //    //if (Input.GetKeyDown(KeyCode.Space))
    //    //{
    //    //    _rb.AddForce(Vector3.up * _jumpSpeed, ForceMode.Impulse);
    //    //}
    //}
}
