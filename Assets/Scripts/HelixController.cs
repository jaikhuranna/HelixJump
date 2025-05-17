using UnityEngine;

public class HelixController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 150f;
    private float lastMouseX;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMouseX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            float deltaX = Input.mousePosition.x - lastMouseX;
            transform.Rotate(Vector3.up, -deltaX * rotationSpeed * Time.deltaTime);
            lastMouseX = Input.mousePosition.x;
        }
    }
}
