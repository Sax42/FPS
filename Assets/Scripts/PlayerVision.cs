using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlayerVision: MonoBehaviour
{
    public static PlayerVision instance;

    private void Awake()
    {
        instance = this;
    }

    public float mouseSensitivity = 100f; // Controls how fast the camera rotates
    public Transform playerBody; // Reference to the player's body (Capsule)
    public Transform objectFollowCameraRotation;
    private float xRotation = 0f;

    private void Start()
    {
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * StaticVal.timeScale;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * StaticVal.timeScale;

        // Rotate the camera vertically
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,-90f,90f); // Limit vertical rotation

        transform.localRotation = Quaternion.Euler(xRotation,0f,0f);

        // Rotate the player horizontally
        playerBody.Rotate(Vector3.up * mouseX);
        objectFollowCameraRotation.rotation = transform.rotation;
    }
}