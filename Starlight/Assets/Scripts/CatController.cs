using UnityEngine;

public class CatController : MonoBehaviour
{

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 500f;
    public string groundTag = "Ground";

    [SerializeField]
    private string inputController= "horizontal";

    private bool grounded;
    private Animator anim;
    private Rigidbody2D rb2d;


    // Use this for initialization
    //Edit=> Project Settings=> Input =>
    // Set the following up: 
        //horizontal => negative Button = a; postive Button = d;
        //jump => postivie Button = w; negative Button = w;
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;
        inputController = "horizontal";
        jumpForce = 500f;
        groundTag = "Ground";
    }

    // Update is called once per frame
    void Update()
    {
        jump |= (Input.GetButtonDown("Jump") && grounded);
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis(inputController);

        anim.SetFloat("Speed", Mathf.Abs(h));

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (jump)
        {
            anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    //set all platforms to Ground tag; if Ground tag doesn't exist, add in Ground tag
    private void OnCollisionStay2D(Collision2D collision)
    {
     
       grounded |= collision.collider.gameObject.CompareTag(groundTag);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded &= !collision.collider.gameObject.CompareTag(groundTag);

    }
}

