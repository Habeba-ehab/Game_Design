using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 1f, -5f);
    public float smoothSpeed = 8f;
    public bool followRotation = true;
    public float minHeight = 1f;
    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition;

        if (followRotation)
            desiredPosition = target.position + target.TransformDirection(offset);
        else
            desiredPosition = target.position + offset;

        // Prevent camera from going below minimum height
        desiredPosition.y = Mathf.Max(desiredPosition.y, minHeight);

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}