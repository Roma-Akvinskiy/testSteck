using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class MouseLook : NetworkBehaviour
{
    public Transform playerBody;
    public float mouseSenselivity = 100f;
    private float xRotation = 0f;
    public AnimationController anim;
     public Transform character;
     public float smoothTime = 0f; //примерно
    private Vector3 vel;
    public Player _pl;
    // Start is called before the first frame update
    void Start()
    {
        _pl=playerBody.gameObject.GetComponent<Player>();
        if ( !isLocalPlayer)
        {
            gameObject.SetActive(false);
            return;
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        transform.rotation = Quaternion.Euler(0, 0, 0);
       //_cameraPozition = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

         transform.position = Vector3.SmoothDamp(transform.position, character.position, ref vel, smoothTime); //плавно перемещает камеру в точку координату персонажа
        transform.forward = Vector3.SmoothDamp(transform.forward, character.forward, ref vel, smoothTime); //плавно перемещает forward (поворачивает) cameraRig чтобы смотреть в то же место, куда и персонаж.
        float mouseX = Input.GetAxis("Mouse X") * mouseSenselivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenselivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -50f, 50f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        anim.OnMuvHand(-xRotation);

    }

   
}
