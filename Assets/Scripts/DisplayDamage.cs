using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{

    [SerializeField] private Canvas damageImage;
    [SerializeField] private float timeToStayActive = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        damageImage.gameObject.SetActive(false);
    }

    public void ShowDamageImage()
    {
        StartCoroutine(ShowDamageCanvas());
    }

    private IEnumerator ShowDamageCanvas()
    {
        damageImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeToStayActive);
        damageImage.gameObject.SetActive(false);
    }

    public void TurnOffCanvas()
    {
        damageImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
