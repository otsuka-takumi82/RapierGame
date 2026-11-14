using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    [SerializeField, Header("射程距離")] public float _rapierDistance;
    [SerializeField, Header("攻撃力")] public float[] _damage;
    /// <summary>
    /// インデックス0は速度、1は一時保存用
    /// </summary>
    [SerializeField, Header("マウス速度")] public float[] _mouseSpeed;
    [SerializeField, Header("ジャンプ速度")] public float _jumpSpeed = 1;
    [SerializeField, Header("速度")] public float _speed;
    [SerializeField, Header("縦速度")] public float _verticalSpeed;
    [SerializeField, Header("重力速度")] public float _gravity;
    [SerializeField, Header("カーソル")] public Image _cousle;
    [SerializeField]Transform _camera;
    CharacterController _characterController;
    Rigidbody _rb;
    Animator _anim;
    Vector3 _rapierHitPosition;
    private void Awake()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        //レイピア溜め
        RapierCharge();
        //レイピア突き
        RapierRapid();
        //横回転
        MouseRotateX();
        //キャラクターコントローラー関係
        Move();
    }
    public void MouseRotateX()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * _mouseSpeed[0], 0);
    }
    public void Move()
    {
        float sideSpeed = Input.GetAxisRaw("Horizontal") * _speed;
        float forwardSpeed = Input.GetAxisRaw("Vertical") * _speed;
        
        if(_characterController.isGrounded)
        {
            _verticalSpeed = -1f;
            if(Input.GetButtonDown("Jump"))
            {
                _verticalSpeed = _jumpSpeed;
            }
        }

        _verticalSpeed += Time.deltaTime * _gravity;
        Vector3 moveMent = new Vector3(sideSpeed, _verticalSpeed, forwardSpeed) * Time.deltaTime;
        moveMent = transform.rotation * moveMent;
        _characterController.Move(moveMent);
    }
    public void RapierCharge()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _anim.SetBool("Charge", true);
            _mouseSpeed[1] = _mouseSpeed[0];
            _mouseSpeed[0] *= 0.5f;
        }
        RapierDistance();
    }
    public void RapierRapid()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _anim.SetBool("Charge", false);
            _mouseSpeed[0] = _mouseSpeed[1];
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
                hit.collider.GetComponent<Enemy>()?.EnemyHelth(-_damage[0], 1f);
            }
            else if (hit.collider.gameObject.CompareTag("WeakPoints"))
            {
                hit.collider.GetComponentInParent<Enemy>()?.EnemyHelth(-_damage[0], 2f);
            }
        }
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
