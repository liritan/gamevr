using UnityEngine;
using System.Collections;

public class RopeController : MonoBehaviour
{
    public Transform[] ropePoints;
    public float moveSpeed = 5f;

    private bool playerInTrigger = false;
    private bool isMoving = false;
    private int currentTargetIndex = 0;
    private Rigidbody playerRigidbody;
    private float initialYPosition; // Начальная Y-координата игрока

    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E) && !isMoving)
        {
            StartCoroutine(MovePlayerAlongRope());
        }
    }

    private IEnumerator MovePlayerAlongRope()
    {
        isMoving = true;
        initialYPosition = playerRigidbody.position.y;
        playerRigidbody.useGravity = false;

        Vector3 targetPosition = ropePoints[currentTargetIndex].position;
        targetPosition.y = initialYPosition; 

        while (Vector3.Distance(playerRigidbody.position, targetPosition) > 0.05f)
        {
            Vector3 direction = (targetPosition - playerRigidbody.position).normalized;
            direction.y = 0f;
            playerRigidbody.MovePosition(playerRigidbody.position + direction * moveSpeed * Time.deltaTime);
            yield return null;
        }

        playerRigidbody.useGravity = true;

        currentTargetIndex++;
        if (currentTargetIndex >= ropePoints.Length) currentTargetIndex = 0;

        isMoving = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            playerRigidbody = other.GetComponent<Rigidbody>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerInTrigger = false;
    }
}