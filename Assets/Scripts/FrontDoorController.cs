using UnityEngine;
using System.Collections;

public class FrontDoorController : MonoBehaviour
{
    public float moveDistance = 1.0f;
    public float moveSpeed = 2.0f;
    public float closeDelay = 5.0f;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isOpen = false;
    private bool playerInTrigger = false;

    void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.down * moveDistance;
    }

    void Update()
    {
        if (playerInTrigger && !isOpen) OpenDoor();
    }

    public void OpenDoor()
    {
        if (!isOpen) StartCoroutine(OpenDoorRoutine());
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

        yield return new WaitForSeconds(closeDelay);

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
            if (gameObject.CompareTag("FrontDoor")) OpenDoor();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerInTrigger = false;
    }
    
}
