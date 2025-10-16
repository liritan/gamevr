using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HDRisRotation: MonoBehaviour
{
    public float rotationSpeed= 0.5f;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * -rotationSpeed);
    }
}