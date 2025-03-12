using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Weapon, Animation
}

public class InteractibleObject: Item
{
    private bool deleteOnInteraction = false;

    private bool changeParentOnInteraction = true;

    private Vector3 newPosition;
    public ObjectType objectType;

    private void Interact()
    {
    }

    public virtual void Interact(GameObject pInteractingObject)
    {
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}