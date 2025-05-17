using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    [SerializeField] private float deathAngleThreshold = 1.0f; 
    [SerializeField] private float raycastDistance = 1.0f;
    [SerializeField] private LayerMask platformLayerMask;
    
    private Rigidbody rb;
    private bool isDead = false;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        if (isDead)
        {
            rb.freezeRotation = false;
            rb.constraints = RigidbodyConstraints.None;
        }

        if (rb.linearVelocity.y < 0 && !isDead)
        {
            CheckForDeadlyPlatform();
        }
    }
    
    private void CheckForDeadlyPlatform()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down * raycastDistance, Color.red);
        
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, platformLayerMask))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            print(angle);
            
            if (angle > deathAngleThreshold)
            {
                isDead = true;
                print("Game Over");
                GameManager.Instance.GameOver();
            }
        }
    }
}
