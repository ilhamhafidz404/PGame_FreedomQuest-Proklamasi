using UnityEngine;
using UnityEngine.InputSystem;

public class BasicController : MonoBehaviour
{

    // BASIC COMPONENTS
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D cc;

    // Preparation for Walk & Run Character
    Vector2 v2;

    [SerializeField]
    private bool isMoving = false;
    public bool IsMoving
    {
        get { return isMoving; }
        set { 
            isMoving = value; 
            anim.SetBool("isMoving", value);
        }
    }

    [SerializeField]
    private bool isRunning = false;
    public bool IsRunning {
        get { return isRunning; }
        set {
            isRunning = value;
            
            anim.SetBool("isRunning", value);
        }
    }

    public float walkSpeed = 5f;
    public float runSpeed = 8f;

    public float currentSpeed {
        get {
            float speed = 0f;
            if(isRunning && IsMoving){
                speed = runSpeed;
            } else if(IsMoving) {
                speed = walkSpeed;
            } else {
                speed = 0f;
            }

            return speed;
        }
    }

    // Preparation for Sfx
    public AudioSource walkSound;
    public AudioSource runSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        cc = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(v2.x * currentSpeed, rb.linearVelocity.y);
    }


    // Listen Input Move
    public void onMove(InputAction.CallbackContext context){
        v2 =  context.ReadValue<Vector2>();

        IsMoving = v2 != Vector2.zero;

        if (v2.x != 0) {
            spriteRenderer.flipX = v2.x < 0;
        }

        // Play SFX
        if(context.started){
            walkSound.Play();
        } else if (context.canceled || IsRunning){
            walkSound.Stop();
        }
    }

    // Listen Input Move + Run
    public void onRunning(InputAction.CallbackContext context){
        if (context.started){
            IsRunning = true;

            //
            runSound.Play();
            walkSound.Stop();
        } else if (context.canceled){
            IsRunning = false;
            
            //
            runSound.Stop();
        }
    }
}
