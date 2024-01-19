using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class OnShoot : NetworkBehaviour
{
    #region Shoot
    public Animator anim;
    public float OnRepit = 0.5f;
    public float magazine = 20;
    public Camera _cameraPozition;
    private RaycastHit hit;
    public int distanse = 200;
    public int damage = 10;
    public float fireRate = 1f;
    private float nextFire = 0f;
    public AudioClip clipShoot, recherg;
    public ParticleSystem shootEffects;
    public AudioSource audios;
    private Player _pl;
    // Start is called before the first frame update
    void Start()
    {
        _pl = GetComponent<Player>();
        if (!isLocalPlayer) return;
        
            AudioManager.Instanse.audioPlayer = audios;
        
       
        //anim=gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer) return;
       
        if (Input.GetMouseButton(0) && Time.time > nextFire && magazine > 0)
        {

            nextFire = Time.time + 1f / fireRate;
            CmdShoot();
            Ray ray = new Ray(_cameraPozition.transform.position, _cameraPozition.transform.forward);
            
            if (Physics.Raycast(ray, out hit, distanse))
            {
                if (hit.collider.gameObject.GetComponent<PayerHealth>())
                {
                    CmdShoot(hit.collider.gameObject.GetComponent<PayerHealth>(), hit.collider.gameObject.GetComponent<NetworkIdentity>().netId);
                }
              
            }
            anim.SetTrigger("Shoot");
            magazine -= 1;
            if (magazine <= 0)
            {
                CmdRecherg();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            CmdRecherg();

        }
    }

    [Command]
    public void CmdRecherg()
    {
        Targetrecherg();  

    }

    [ClientRpc]
    void Targetrecherg()
    {
        AudioManager.Instanse.OnPlayClip(recherg);
        anim.SetTrigger("Recherg");
        StartCoroutine(waitRecherge());
    }


    [Command]
    public void CmdShoot(PayerHealth playerHealth,uint player)
    {
        if (playerHealth)
        {
            playerHealth.Damage(damage);
           
        }
    }

    [Command]
    public void CmdShoot()
    {
        TargetShoot();
       
    }

    [ClientRpc]
    void TargetShoot()
    {
        shootEffects.Play();
        AudioManager.Instanse.OnPlayClip(clipShoot);
    }
    IEnumerator waitRecherge()
    {
        yield return new WaitForSeconds(1.3f);
        magazine = 20;
    }

    #endregion

   
    
}
