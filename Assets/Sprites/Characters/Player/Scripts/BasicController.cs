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
    
    // 
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    public ContactFilter2D castFilter;

    [SerializeField] 
    private float jumpForce = 5f;

    // Preparation for Sfx
    public AudioSource walkSound;
    public AudioSource runSound;
    public AudioSource jumpSound;

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


    //

    // Tombol tekan ke kiri
    public void OnMoveLeftDown()
    {
        v2 = Vector2.left;
        IsMoving = true;
        spriteRenderer.flipX = true;
        if (!IsRunning) walkSound.Play();
    }

    // Tombol tekan ke kanan
    public void OnMoveRightDown()
    {
        v2 = Vector2.right;
        IsMoving = true;
        spriteRenderer.flipX = false;
        if (!IsRunning) walkSound.Play();
    }

    // Tombol lepas (baik kiri maupun kanan)
    public void OnMoveButtonUp()
    {
        v2 = Vector2.zero;
        IsMoving = false;
        walkSound.Stop();
        runSound.Stop();
    }

    public void OnRunButtonClick()
    {
        IsRunning = !IsRunning; // Toggle antara true/false

        // SFX
        if (IsRunning)
        {
            runSound.Play();
            walkSound.Stop();
        }
        else
        {
            runSound.Stop();
            if (IsMoving) walkSound.Play(); // Kembali ke jalan biasa kalau masih bergerak
        }
    }

    // Tombol lompat (jika pakai sistem lompat terpisah)
    public void OnJumpButtonDown()
    {
        // Cek apakah grounded menggunakan Cast ke bawah
        int hits = cc.Cast(Vector2.down, castFilter, groundHits, 0.1f);
        if (hits > 0)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");

            // Mainkan SFX lompat
            if (jumpSound != null)
            {
                jumpSound.Play();
            }
        }
    }

}
