using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public float moveDistance = 1.0f;
    public float moveSpeed = 2.0f;
    public float closeDelay = 5.0f;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isOpen = false;
    private bool playerInTrigger = false;

    public ElevatorController elevatorController;
    public int associatedFloor;

    void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.forward * moveDistance;
    }

    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E) && !isOpen)
        {
            if (elevatorController.currentFloor == associatedFloor && !elevatorController.isMoving)
            {
                OpenDoor();
            }
            else
            {
                elevatorController.QueueFloor(associatedFloor);
            }
        }
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            Debug.Log("Opening door at floor: " + associatedFloor);
            StartCoroutine(OpenDoorRoutine());
        }
    }

    IEnumerator OpenDoorRoutine()
    {
        isOpen = true;
        float elapsedTime = 0f;
        Vector3 startingPos = transform.position;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startingPos, targetPosition, elapsedTime);
            yield return null;
        }

        transform.position = targetPosition;
        StartCoroutine(CloseDoorWithDelay(closeDelay));
    }

    IEnumerator CloseDoorWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(CloseDoor());
    }

    IEnumerator CloseDoor()
    {
        float elapsedTime = 0f;
        Vector3 startingPos = transform.position;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(targetPosition, initialPosition, elapsedTime);
            yield return null;
        }

        transform.position = initialPosition;
        isOpen = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }
}
