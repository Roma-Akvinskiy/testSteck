using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UIElements;
using System.Data;

public class PlayerMovenent : NetworkBehaviour
{
    public CharacterController controller;
    public AudioClip clipmuve;
    public Transform graundCheck;
    public LayerMask graundMask;
    public AnimationController animControl;
    public PayerHealth health;
    public float speed = 5f;
    public float speedrun = 8;
    private float curentSpeed;
    private float gravity = -9.8f;
    private float graundDitanse = 0.4f;
    public float junpHte = 3f;
    public float fireRate = 1f;
    private float nextFire = 0f;
    float x, z;
    Vector3 velosity;
    bool isGraunded;
    [SyncVar]public bool _isdead;
    private Player _pl;
    private void Start()
    {
        _pl = GetComponent<Player>();
    }

    void FixedUpdate()
    {

        if (!isLocalPlayer ) return;
        if (_isdead==true)
        {
            Vector3 newPos = NetworkManager.singleton.GetStartPosition().position;
            controller.transform.position = newPos;
            _isdead = false;
        }else if (_isdead==false)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            x = Mathf.Clamp(x, -1, 1);
            z = Mathf.Clamp(z, -1, 1);
            if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
            {
                animControl.OnSetBool("Runinih", true);
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
            {
                animControl.OnSetBool("Runinih", false);
            }
            OnRun();
            OnTransformPozition();
            OnJump();
            if (Input.GetAxis("Horizontal") != 0 && Time.time > nextFire || Input.GetAxis("Vertical") != 0 && Time.time > nextFire)
            {
                nextFire = Time.time + 1f / fireRate;
                StartCoroutine(OnAudioMuve());
            }

        }



    }

    private void OnTransformPozition()
    {
        
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * curentSpeed * Time.fixedDeltaTime);
        velosity.y += gravity * Time.fixedDeltaTime;
        controller.Move(velosity * Time.fixedDeltaTime);
        animControl.OnMuvment(x,z);
    }

    private void OnJump()
    {
        if (!isLocalPlayer) return;
        isGraunded = Physics.CheckSphere(graundCheck.position, graundDitanse, graundMask);
        if (isGraunded && velosity.y < 0)
        {
            velosity.y = -2f;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGraunded)
        {
            animControl.OnJump();
            StartCoroutine(OnVelosity());

        }

    }

    private IEnumerator OnVelosity()
    {
        yield return new WaitForSeconds(0.13f);
            velosity.y = Mathf.Sqrt(junpHte * -2f * gravity);
    }
    private void OnRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
        {
            curentSpeed = speedrun;
            
        }
        else
        {
            curentSpeed = speed;
            
        }
    }

    private IEnumerator OnAudioMuve()
    {
        AudioManager.Instanse.OnPlayClip(clipmuve);
        yield return new WaitForSeconds(1);
    }
    
    

   


}
