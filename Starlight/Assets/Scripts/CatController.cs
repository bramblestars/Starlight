using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (Input.GetButtonDown("Cancel"))
            {
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

    }

    void FixedUpdate()
    {
        float h = Input.GetAxis(inputController);

        anim.SetFloat("Speed", Mathf.Abs(h));

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (rb2d.velocity.y < 0)
        {
            Physics2D.gravity = new Vector2(0, -11.8F);
        }
        else
        {
            Physics2D.gravity = new Vector2(0, -8F);
        
        }
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

    private void OnTriggerEnter2D(Collider2D Collider)
    {
        if (Collider.gameObject.name.Contains("cloud"))
        {
            cloud cloudScript = Collider.gameObject.GetComponent<cloud>();
            Debug.Log(cloudScript.type);

            if (cloudScript.type== "button")
            {
                Debug.Log("Yes! Good!");
                cloudScript.act = true;
                cloudScript.interact();
            }
            else if (cloudScript.type == "bounce")
            { 

            /*if (cloudScript.act)
                {
                    rb2d.AddForce(new Vector2(0f, jumpForce));
                    Collider.gameObject.GetComponent<BoxCollider2D>().GetComponent<PhysicsMaterial2D>().bounciness = 1;
                }*/
            }
        }
    }

    private void OnTriggerStay2D(Collider2D Collider)
    {
        Debug.Log("In");
        if (Collider.gameObject.name.Contains("cloud"))
        {
            transform.parent = Collider.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D Collider)
    {
        if (Collider.gameObject.name.Contains("cloud"))
        {
            transform.parent = null;

        }
    }
}

