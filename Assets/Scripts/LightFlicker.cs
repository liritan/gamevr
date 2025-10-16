using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Material engineMaterial;
    public float minEmission = 0f; 
    public float maxEmission = 1f;
    public float flickerSpeed = 5f;

    private float currentEmission;
    private bool isIncreasing = true;

    void Start()
    {
        if (engineMaterial.HasProperty("_EmissionColor"))
        {
            currentEmission = Random.Range(minEmission, maxEmission);
            UpdateEmission(currentEmission);
        }
    }

    void Update()
    {
        if (isIncreasing)
        {
            currentEmission += flickerSpeed * Time.deltaTime;
            if (currentEmission >= maxEmission)
            {
                currentEmission = maxEmission;
                isIncreasing = false;
            }
        }
        else
        {
            currentEmission -= flickerSpeed * Time.deltaTime;
            if (currentEmission <= minEmission)
            {
                currentEmission = minEmission;
                isIncreasing = true;
            }
        }

        UpdateEmission(currentEmission);
    }

    void UpdateEmission(float emissionValue)
    {
        Color baseColor = engineMaterial.GetColor("_Color");
        Color emissionColor = baseColor * Mathf.LinearToGammaSpace(emissionValue);
        engineMaterial.SetColor("_EmissionColor", emissionColor);
    }
}
