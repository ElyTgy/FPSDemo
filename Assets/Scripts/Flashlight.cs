using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{

    [SerializeField] private Light flashLight;
    [SerializeField] FlashlightData reductionAmount = new FlashlightData(-0.001f, -0.001f, -0.001f);
    [SerializeField] float minSpotAngle = 10.0f;

    void Start()
    {

    }

    void Update()
    {
        AddLightVals(reductionAmount);
    }

    public void AddLightVals(FlashlightData data)
    {
        flashLight.range += data.m_range;
        flashLight.spotAngle += data.m_spotAngle;
        flashLight.intensity += data.m_intensity;

        if(flashLight.spotAngle <= minSpotAngle)
        {
            flashLight.spotAngle = minSpotAngle;
        }

        if(flashLight.spotAngle <= 0.0f || flashLight.range <= 0.0f || flashLight.intensity <= 0.0f)
        {
            flashLight.range = 0.0f;
            flashLight.spotAngle = 0.0f;
            flashLight.intensity = 0.0f;
        }
    }
}
