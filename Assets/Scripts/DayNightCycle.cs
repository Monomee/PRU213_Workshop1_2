using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class DayNightCycle : MonoBehaviour
{
    [Header("Time of Day")]
    [Range(0, 24)] public float currentTime = 12f; 
    public float timeSpeed = 0.1f; 

    [Header("Sun")]
    public Light sunLight;
    public float maxSunIntensity = 100000f; 

    [Header("Moon")]
    public Light moonLight;
    public float maxMoonIntensity = 1.5f;

    private HDAdditionalLightData sunData;
    private HDAdditionalLightData moonData;

    void Start()
    {
        if (sunLight != null) sunData = sunLight.GetComponent<HDAdditionalLightData>();
        if (moonLight != null) moonData = moonLight.GetComponent<HDAdditionalLightData>();
    }

    void Update()
    {
        currentTime += timeSpeed * Time.deltaTime;
        if (currentTime >= 24f) currentTime = 0f;

        float sunRotationX = (currentTime / 24f) * 360f - 90f;

        if (sunLight != null)
            sunLight.transform.rotation = Quaternion.Euler(sunRotationX, -90f, 0f);

        if (moonLight != null)
            moonLight.transform.rotation = Quaternion.Euler(sunRotationX + 180f, -90f, 0f);

        UpdateLightIntensity(sunRotationX);
    }

    void UpdateLightIntensity(float rotationX)
    {
        float angleRad = rotationX * Mathf.Deg2Rad;

        float sunFactor = Mathf.Max(0, Mathf.Sin(angleRad));
        float moonFactor = Mathf.Max(0, -Mathf.Sin(angleRad));

        if (sunFactor < 0.2f && rotationX > 90f && rotationX < 270f)
        {
            moonFactor = Mathf.Max(moonFactor, (0.2f - sunFactor) * 0.5f);
        }

        if (sunData != null)
        {
            sunData.intensity = sunFactor * maxSunIntensity;
        }

        if (moonData != null)
        {
            float finalMoonFactor = Mathf.Max(0.05f, moonFactor);
            moonData.intensity = finalMoonFactor * maxMoonIntensity;
        }

        if (sunFactor > 0.02f)
        {
            if (sunLight != null) sunLight.shadows = LightShadows.Soft;
            if (moonLight != null) moonLight.shadows = LightShadows.None;
        }
        else
        {
            if (sunLight != null) sunLight.shadows = LightShadows.None;
            if (moonLight != null)
            {
                moonLight.shadows = (moonFactor > 0.05f) ? LightShadows.Soft : LightShadows.None;
            }
        }
    }
}
