using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float _verticalRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * Player.Instance._mouseSpeed[0];
        _verticalRotation -= mouseY;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -80, 80);
        transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
    }
}
