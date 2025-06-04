using UnityEngine;

public class TouchGroundController : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    CapsuleCollider2D cc;
    Animator anim;

    [SerializeField]
    public bool isGrounded = true;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];

    public bool IsGrounded { 
        get {
            return isGrounded;
        }
        private set {
            isGrounded = value;
            anim.SetBool("isGrounded", value);
        }  
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cc = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = cc.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
    }
}
