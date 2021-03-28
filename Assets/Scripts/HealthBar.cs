using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = playerHealth.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = playerHealth.health;
    }
}
