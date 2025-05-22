using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 1, -6);
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private float minYAngle = -20f;
    [SerializeField] private float maxYAngle = 60f;
    private float yaw = 0f;
    private float pitch = 20f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minYAngle, maxYAngle);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = target.position + rotation * offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
