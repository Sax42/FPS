using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving: MonoBehaviour
{
    [HideInInspector] public Vector3 direction;

    public float speed;

    private Vector3 velocity;
    public bool facingDirection = true;

    public Vector3 Velocity
    {
        get => velocity;
        private set => velocity = value;
    }

    public bool faceDirection = true;

    protected virtual void Init(Vector3 pDirection,float pSpeed,bool pfaceDirection)
    {
        direction = pDirection;
        speed = pSpeed;
        faceDirection = pfaceDirection;
    }

    public void SetMoving(Vector3 pDirection,float pSpeed)
    {
        speed = pSpeed;
        direction = pDirection;
        GetVelocity();
    }

    public void SetMoving(Vector3 velocity)
    {
        speed = velocity.magnitude;
        direction = velocity.normalized;
        GetVelocity();
    }

    public Vector3 GetVelocity()
    {
        velocity = direction * speed;
        return velocity;
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Movement(GetVelocity());
    }

    private void Movement(Vector3 pVelocity)
    {
        transform.position += pVelocity * Time.deltaTime;
        if(facingDirection)
        {
            transform.rotation = Quaternion.LookRotation(transform.forward,transform.up);
        }
    }
}