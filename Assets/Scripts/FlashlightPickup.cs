using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightPickup : MonoBehaviour
{

    [SerializeField] private FlashlightData flashlightData = new FlashlightData(10.0f, 10.0f, 10.0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log(other.gameObject.tag);
            other.gameObject.GetComponent<Flashlight>().AddLightVals(flashlightData);
            Destroy(gameObject);
        }
    }
}
