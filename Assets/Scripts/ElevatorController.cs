using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElevatorController : MonoBehaviour
{
    public GameObject[] elevatorFloors;
    public float moveSpeed = 5f;
    public DoorController[] doors;
    public Transform player;

    public bool isMoving = false;
    private Vector3 targetPosition;
    private Queue<int> floorQueue = new Queue<int>();
    public int currentFloor = -1;
    private bool playerCalledElevator = false;

    public void QueueFloor(int floorIndex, bool calledByPlayer = false)
    {
        if (!floorQueue.Contains(floorIndex))
        {
            floorQueue.Enqueue(floorIndex);
            if (calledByPlayer) playerCalledElevator = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) QueueFloor(0, true);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) QueueFloor(1, true);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) QueueFloor(2, true);

        if (!isMoving && floorQueue.Count > 0) MoveToFloor(floorQueue.Dequeue());
    }

    void MoveToFloor(int floorIndex)
    {
        if (currentFloor == floorIndex || isMoving) return;

        currentFloor = floorIndex;
        targetPosition = elevatorFloors[floorIndex].transform.position;
        StartCoroutine(MoveElevator(targetPosition));
    }

    IEnumerator MoveElevator(Vector3 targetPos)
    {
        isMoving = true;
        
        if (playerCalledElevator && player != null) player.SetParent(transform);

        while (Vector3.Distance(transform.position, targetPos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
        
        if (playerCalledElevator && player != null)
        {
            player.SetParent(null);
            playerCalledElevator = false;
        }

        if (currentFloor >= 0 && currentFloor < doors.Length) doors[currentFloor].OpenDoor();
    }
}
