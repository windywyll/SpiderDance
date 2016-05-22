using UnityEngine;
using System.Collections;

public class BasicMovement : MonoBehaviour {

    Vector2 direction;
    bool lookingRight = true;
    public bool isGrounded, topColl, rightColl, leftColl;
    float gravMod = 1.0f;
    [SerializeField]
    private Animator animator;

    private GameObject idleChar;

    public bool leftStick = false;
    public bool rightStick = false;

    [SerializeField]
    float height = 1.0f;
    [SerializeField]
    float width = 1.0f;
    [SerializeField]
    float speed = 15.0f;
    [SerializeField]
    float gravity = 5;
    [SerializeField]
    float weight = 1;

    void OnTriggerEnter2D(Collider2D col)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, col.transform.position - transform.position, 3.0f);
        
        if (hit.collider != null && col.tag != "Player")
        {
            if (col.tag == "Floor" || col.tag == "Platform" || col.tag == "Door" || col.tag == "Acid")
            {
                if (hit.point.y <= transform.position.y + height / 2.0f && hit.point.y >= transform.position.y + height * 0.4f)
                    topColl = true;

                if (hit.point.y >= transform.position.y - height / 2.0f && hit.point.y <= transform.position.y - height * 0.4f)
                    isGrounded = true;

                if (hit.point.x <= transform.position.x + width / 2.0f && hit.point.x >= transform.position.x + width * 0.4f && (hit.point.y <= transform.position.y + (height * 0.4f) || hit.point.y >= transform.position.y + (height * 0.4f)))
                    rightColl = true;

                if (hit.point.x >= transform.position.x - width / 2.0f && hit.point.x <= transform.position.x - width * 0.4f && (hit.point.y <= transform.position.y + (height * 0.4f) || hit.point.y >= transform.position.y + (height * 0.4f)))
                    leftColl = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag != "Player")
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, height);

            if (hit.collider != null)
            {
                if (hit.point.y > transform.position.y + height / 2.0f)
                    topColl = false;
            }
            else
                topColl = false;

            hit = Physics2D.Raycast(transform.position, -transform.up, height);

            if (hit.collider != null)
            {
                if (hit.point.y < transform.position.y - height / 2.0f)
                    isGrounded = false;
            }
            else
                isGrounded = false;

            hit = Physics2D.Raycast(transform.position, transform.right, height);

            if (hit.collider != null)
            {
                if (hit.point.x > transform.position.x + width / 2.0f)
                    rightColl = false;
            }
            else
                rightColl = false;

            hit = Physics2D.Raycast(transform.position, -transform.right, height);

            if (hit.collider != null)
            {
                if (hit.point.x > transform.position.x - width / 2.0f)
                    leftColl = false;
            }
            else
                leftColl = false;
        }
    }

    // Use this for initialization
    void Start () {
        string childname = gameObject.name + "_Idle";
        idleChar = transform.FindChild(childname).gameObject;
        direction = Vector2.zero;
        isGrounded = false;
        topColl = false;
        rightColl = false;
        leftColl = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float dirX = 0.0f;
        if (leftStick)
            dirX = Input.GetAxis("L_XAxis_0") * Time.deltaTime * speed;

        if(rightStick)
            dirX = Input.GetAxis("R_XAxis_0") * Time.deltaTime * speed;

        float dirY = 0.0f;

        if (rightColl && dirX > 0.0f)
            dirX = 0.0f;

        if (leftColl && dirX < 0.0f)
            dirX = 0.0f;

        if ((dirX > 0.0f || dirX < 0.0f) && animator != null)
        {
            animator.SetBool("Run", true);
            //idleChar.SetActive(false);
        }
        else
        {
            if (dirX == 0.0f && animator != null)
            {
                animator.SetBool("Run", false);
                //idleChar.(true);
            }
        }

        if(!isGrounded)
            dirY = -gravity * gravMod * weight * Time.deltaTime;

        direction.x = dirX;
        direction.y = dirY;

        if ((direction.x > 0 && !lookingRight) || (direction.x < 0 && lookingRight))
            Flip();

        transform.Translate(direction);
    }

    void Flip()
    {
        lookingRight = !lookingRight;
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }

    public void modifyGravity(float modifyer)
    {
        gravMod = modifyer;
    }

    public float getHeight()
    {
        return height;
    }

    public bool getIsGroundedState()
    {
        return isGrounded;
    }
}
