using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;

    public float Distance = 3f;
    public float Height = 2f;
    public float Rotation = 0f;

    public float Damping = 2f;

    void Update()
    {
        if(Target == null) return;

        var flatDirection = Target.forward;
        flatDirection.y = 0f;

        var targetPosition = Target.position - (Quaternion.Euler(0f, Rotation, 0f) * flatDirection * Distance) + (Vector3.up * Height);

        transform.position = Vector3.Lerp(transform.position, targetPosition, Damping * Time.deltaTime);
        transform.LookAt(Target);
    }
}