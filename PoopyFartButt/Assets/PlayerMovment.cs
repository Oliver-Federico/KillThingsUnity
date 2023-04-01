using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovment : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float mouseSensitivity = 5.0f;
    public float jumpForce = 10.0f;
    private Rigidbody rb;

    float verticalRotation = 0;
    public float upDownRange = 60.0f;

    bool isGrounded = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Lock cursor to game window and hide it
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Mouse movement
        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotLeftRight, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        // Keyboard movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;
        rb.MovePosition(rb.position + transform.TransformDirection(movement) * movementSpeed * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
        // Sprinting
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movementSpeed = movementSpeed + 10;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            movementSpeed = movementSpeed - 10;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
