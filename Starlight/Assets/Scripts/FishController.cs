using UnityEngine;
using System.Collections;

public class FishController : MonoBehaviour
{
    [HideInInspector] public bool facingRight = true;

    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public string groundTag = "GroundFish";

    [SerializeField]
    private string inputController = "fishHorizontal";

    private bool grounded;
    private Animator anim;
    private Rigidbody2D rb2d;


    // Use this for initialization
    //Edit=> Project Settings=> Input =>
    // Set the following up: 
    //fishHorizontal => negative Button = left arrow; postive Button = right arrow;
  
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;
        inputController = "fishHorizontal";
   
        groundTag = "Ground";
    }

    // Update is called once per frame
    void Update()
    {
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