using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float mouseSensitivity = 100.0f;
    public Transform playerCamera;

    private float rotationX = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);

        float moveX = (Input.GetKey(KeyCode.D) ? 1f : 0f) - (Input.GetKey(KeyCode.A) ? 1f : 0f);
        float moveZ = (Input.GetKey(KeyCode.W) ? 1f : 0f) - (Input.GetKey(KeyCode.S) ? 1f : 0f);
        
        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, moveDirection, out hit, speed * Time.deltaTime))
        {
            StartCoroutine(IgnoreMovementForDuration(0.2f));
            return;
        }
        
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    IEnumerator IgnoreMovementForDuration(float duration)
    {
        enabled = false;
        yield return new WaitForSeconds(duration);
        enabled = true;
    }
}