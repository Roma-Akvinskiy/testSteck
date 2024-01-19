using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Camera _cameraPozition;
    public GameObject player;
    private RaycastHit hit;
    public float distanse = 500;
    public Image[] images;
    public Color colorRed,colorWhite;
    public OnShoot Shooting;
    public Image imageRecherg;
    public float MaxCharge;
    public float newCharge;
    public float state;

    void Start()
    {
        MaxCharge = Shooting.magazine;

    }


    private void FixedUpdate()
    {
        onraycast();
        newCharge = Shooting.magazine;
        state = newCharge / MaxCharge;
        imageRecherg.fillAmount = state;
    }
    private void onraycast()
    {
        Ray ray = new Ray(_cameraPozition.transform.position, _cameraPozition.transform.forward);

        if (Physics.Raycast(ray, out hit, distanse))
        {
            //  var playerHealth = hit.collider.gameObject.GetComponent<PayerHealth>();
            if (hit.transform.gameObject.tag == "Player")
            {
                OnColorChanged(colorRed);
            }
            else
            {
                OnColorChanged(colorWhite);
            }

        }
    }

private void OnColorChanged(Color color)
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = color;
        }
    }


}
