using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput: MonoBehaviour
{
    private Inventory inventory;
    private bool InteractibleObjectNear;
    private InteractibleObject interactibleObject;
    public Transform gunSocket;
    public static PlayerInput instance;
    private Vector2 inputVelocity = Vector2.zero;

    private PhysicHandler physicHandler;
    public float speed = 10;

    public float maxSpeed = 1000f;

    public Force dashForce;
    public Force moveForce;
    public Force jumpForce;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
        physicHandler = GetComponent<PhysicHandler>();
        jumpForce.name = "gravity";
        instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKey(StaticVal.interact) && InteractibleObjectNear)
        {
            PickUpWeapon();
        }

        if(Input.GetKey(StaticVal.shoot))
        {
            Shoot();
        }

        if(Input.GetKey(StaticVal.scrollWeapon))
        {
            inventory.ChangeEquipedItem(true);
        }

        inputVelocity.x = Input.GetAxis("Horizontal");
        inputVelocity.y = Input.GetAxis("Vertical");

        moveForce.direction = transform.rotation * new Vector3(inputVelocity.x,0,inputVelocity.y);
        dashForce.direction = Camera.main.transform.forward;
        jumpForce.direction = Vector3.up;
        physicHandler.AddForce(moveForce);
        if(Input.GetKeyDown(KeyCode.C))
        {
            physicHandler.AddForce(dashForce);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            physicHandler.AddForce(jumpForce);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        InteractibleObject lObject = other.GetComponent<InteractibleObject>();
        if(lObject != null)
        {
            InteractibleObjectNear = true;
            interactibleObject = lObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InteractibleObject lObject = other.GetComponent<InteractibleObject>();
        if(lObject != null)
        {
            InteractibleObjectNear = false;
            interactibleObject = null;
        }
    }

    private void PickUpWeapon()
    {
        if(interactibleObject is null)
            return;
        interactibleObject.Interact(gameObject);
    }

    private void Shoot()
    {
        if(inventory.currentWeapon == null)
            return;
        inventory.currentWeapon.ShootRequest();
    }
}