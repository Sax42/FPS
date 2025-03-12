using UnityEngine;

public class CollisionHandler: MonoBehaviour
{
    [SerializeField] private float groundCheckDistance = 0.15f;
    [SerializeField] private LayerMask groundLayer = 1 << 6; // "Ground" layer by default

    private bool isGrounded;
    private Collider objectCollider;

    public bool IsGrounded => isGrounded;

    private void Awake()
    {
        objectCollider = GetComponent<Collider>();
        if(objectCollider == null)
        {
            Debug.LogError("CollisionHandler requires a Collider component.");
        }
    }

    private void FixedUpdate()
    {
        CheckGrounded();
    }

    private void CheckGrounded()
    {
        if(objectCollider == null)
            return;

        Vector3 origin = objectCollider.bounds.center;
        origin.y = objectCollider.bounds.min.y;

        isGrounded = Physics.Raycast(origin,Vector3.down,groundCheckDistance,groundLayer);
    }
}