using UnityEngine;
using UnityEngine.InputSystem;

public class JumpController : MonoBehaviour
{
    // BASIC COMPONENTS
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D cc;

    // 
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    public ContactFilter2D castFilter;

    [SerializeField] 
    private float jumpForce = 3f;

    // Preparation for Sfx
    public AudioSource jumpSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        cc = GetComponent<CapsuleCollider2D>();
    }

    public void onJump(InputAction.CallbackContext context)
    {
        if (context.started) {
            int hits = cc.Cast(Vector2.down, castFilter, groundHits, 0.1f);
            if (hits > 0) {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                anim.SetTrigger("Jump");
            }

            // Play SFX
            if(context.started){
                jumpSound.Play();
            } else if (context.canceled){
                jumpSound.Stop();
            }
        }

    }
}
