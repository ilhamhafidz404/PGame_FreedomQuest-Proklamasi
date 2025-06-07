using UnityEngine;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    // BASIC COMPONENTS
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D cc;

    // 
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    public ContactFilter2D castFilter;

    // Preparation for Sfx
    public AudioSource attackSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        cc = GetComponent<CapsuleCollider2D>();
    }

    public void onAttack(InputAction.CallbackContext context){
        if (context.started) {
            anim.SetTrigger("Attack");

            // Play SFX
            if(context.started){
                attackSound.Play();
            } else if (context.canceled){
                attackSound.Stop();
            }
        } 
    }

}
