using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            offset.y = 1.2f;
            Vector3 targetPosition = target.position + offset;
            targetPosition.y = target.position.y + (offset.y * 1f); 
            transform.position = targetPosition;
        }
    }
}
