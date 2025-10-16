using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 0, 100);

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}