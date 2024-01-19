using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator anim;
    public bool isRun=false;
    public bool isSit=false;
    // Start is called before the first frame update

    public void OnGunMoove( float vertical)
    {
        anim.SetFloat("VerHan", vertical);
    }

    public void OnMuvment(float horizontal, float vertical)
    {
        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
    }

    public void OnMuvHand( float vertical)
    {
        anim.SetFloat("VerHan", vertical);
    }
    public void OnJump()
    {
        anim.SetTrigger("Jump");    
    }
   
   public void OnSetBool(string nameBool, bool isUsing)
    {
        anim.SetBool(nameBool, isUsing);
    }
  
public void OnSetTrigger(string nameTriger)
    {
        anim.SetTrigger(nameTriger);
    }
}
