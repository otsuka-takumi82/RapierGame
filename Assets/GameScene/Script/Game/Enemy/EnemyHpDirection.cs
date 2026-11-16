using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    private Transform _cameraTransform;

    void Start()
    {
        // シーン上のメインカメラを自動で見つける
        if (Camera.main != null)
        {
            _cameraTransform = Camera.main.transform;
        }
    }

    void LateUpdate()
    {
        if (_cameraTransform != null)
        {
            // 常にカメラの方向を正面に見据える
            transform.LookAt(transform.position + _cameraTransform.rotation * Vector3.forward,
                             _cameraTransform.rotation * Vector3.up);
        }
    }
}