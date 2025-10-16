using UnityEngine;

public class AddColliders : MonoBehaviour
{
    void Start()
    {
        foreach (Transform child in transform) if (child.gameObject.GetComponent<Collider>() == null) child.gameObject.AddComponent<BoxCollider>();
    }
}