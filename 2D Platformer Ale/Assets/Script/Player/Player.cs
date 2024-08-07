using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    Life life;
    PlatformChild platformChild;


    [Header ("Stats")]
    [SerializeField] float speed;
    [SerializeField] float jump;
    [SerializeField] float deathDelay;

    [Header("Dash")]
    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] float dashTime;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashCooldown;

    [Header("HUD")]
    [SerializeField] TextMeshProUGUI coinsTxt;
    [SerializeField] TextMeshProUGUI healthTxt;

    [Header("Groundcheck")]
    [SerializeField] Vector3 boxOffset;
    [SerializeField] float boxWidth;
    [SerializeField] float boxHeight;
    [SerializeField] LayerMask groundLayer;

    [Header("Audio")]
    [SerializeField] GameObject dashSFX;
    [SerializeField] GameObject jumpSFX;
    [SerializeField] GameObject coinSFX;
    [SerializeField] GameObject healingSFX;


    float jumpTimer;
    float gravity;
    int coins;

    public bool isGrounded;
    bool canDoubleJump;
    bool isDead;
    bool flipped;
    bool isDashing;
    bool canDash = true;

    Vector3 respawnPosition;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        life = GetComponent<Life>();
        platformChild = GetComponent<PlatformChild>();

        gravity = rb.gravityScale;
        respawnPosition = transform.position;
        if (PlayerPrefs.HasKey("Checkpoint x"))
        {
            float x = PlayerPrefs.GetFloat("Checkpoint x");
            float y = PlayerPrefs.GetFloat("Checkpoint y");
            transform.position = new Vector3(x, y, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        coinsTxt.text = coins.ToString();
        //healthTxt.text = "health: " + life.GetHealth();

        GroundCheck();

        if (life.GetHealth() <= 0 && !isDead)
        {
            StartCoroutine(Death());
        }

        if (isDead)
        {
            return;
        }

        Dash();

        if (isDashing)
        {
            return;
        }

        Movement();
        Flip();
        Jump();
    }

    private void Dash()
    {
        if (Input.GetButtonDown("Fire3") && canDash)
        {
            if (!flipped)
            {
                rb.velocity = Vector2.right * dashSpeed;
            }
            else
            {
                rb.velocity = Vector2.left * dashSpeed;
            }

            //SFX
            dashSFX.SetActive(false);
            dashSFX.SetActive(true);

            isDashing = true;
            canDash = false;
            rb.gravityScale = 0;
            trailRenderer.emitting = true;
            life.invulnerable = true;
            StartCoroutine(DashRoutine());
        }
    }

    IEnumerator DashRoutine()
    {
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        rb.gravityScale = gravity;
        rb.velocity = Vector2.zero;
        trailRenderer.emitting = false;
        life.invulnerable = false;
        yield return new WaitForSeconds(dashCooldown);
        yield return new WaitUntil(() => isGrounded == true);

        canDash = true;
    }

    private IEnumerator Death()
    {
        rb.velocity = Vector2.zero;
        Debug.Log("Player death");
        isDead = true;
        animator.SetTrigger("isDead");
        yield return new WaitForSeconds(deathDelay);
        SceneManager.LoadScene(1);
        //animator.ResetTrigger("isDead");
        /*life.Heal(5);
        animator.SetTrigger("Revive");
        transform.position = respawnPosition;
        isDead = false;*/
    }

    void Flip()
    {
        if (rb.velocity.x < 0 && !flipped)
        {
            flipped = true;
            transform.Rotate(0,180,0);
            //transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (rb.velocity.x > 0 && flipped)
        {
            flipped = false;
            transform.Rotate(0,180,0);
            //transform.localScale = new Vector3(1, 1, 1);
        }
    }
    private void Jump()
    {
        jumpTimer += Time.deltaTime;
        animator.SetFloat("JumpTimer", jumpTimer);

        if (Input.GetButtonDown("Jump") && (isGrounded || canDoubleJump))
        {
            jumpTimer = 0;
            rb.velocity = new Vector2(rb.velocity.x, jump);
            if (!isGrounded)
            {
                canDoubleJump = false;
            }

            animator.SetTrigger("Jump");
            jumpSFX.SetActive(false);
            jumpSFX.SetActive(true);

        }
    }

    private void Movement()
    {
        //Debug.Log("Player movement");
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y) + Vector2.right * platformChild.speed;

        animator.SetFloat("xVel", Mathf.Abs(rb.velocity.x));
    }

    private void GroundCheck()
    {
        if (Physics2D.BoxCast(transform.position + boxOffset, new Vector2(boxWidth, boxHeight), 0, Vector2.down, boxHeight, groundLayer).collider != null)
        {
            isGrounded = true;
            canDoubleJump = true;
            platformChild.speed = 0;
        }
        else
        {
            isGrounded = false;
        }

        animator.SetBool("isGrounded", isGrounded);
    }

    public void GetCoin(int value)
    {
        coins += value;
        coinSFX.SetActive(false);
        coinSFX.SetActive(true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position + boxOffset, new Vector2(boxWidth, boxHeight * 2));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            Debug.Log("Checkpoint");
            respawnPosition = collision.transform.position;
            PlayerPrefs.SetFloat("Checkpoint x", collision.transform.position.x);
            PlayerPrefs.SetFloat("Checkpoint y", collision.transform.position.y);
        }
    }

}
