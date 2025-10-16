using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController: MonoBehaviour
{
    public float rotationSpeed= 10f;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * -rotationSpeed);
    }
}