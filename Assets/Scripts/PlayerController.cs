using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float gravityModifier;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSfx;
    public AudioClip crashSfx;

    private Rigidbody rb;
    private InputAction jumpAction;
    private InputAction sprintAction; // Shift
    private bool isOnGround = true;
    private bool canDoubleJump = false; // ตัวแปร double Jump

    private Animator playerAnim;
    private AudioSource playerAudio;

    public bool gameOver = false;
    public bool isSprinting = false; // ตัวแปร Sprint

    public int maxHP = 3; //ตัวแปร HP
    private int currentHP;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Physics.gravity *= gravityModifier;

        jumpAction = InputSystem.actions.FindAction("Jump");
        sprintAction = InputSystem.actions.FindAction("Sprint");

        gameOver = false;
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpAction.triggered && !gameOver)
        {
            if (isOnGround)
            {
                Jump();
                isOnGround = false;
                canDoubleJump = true; // double jump
            }
            else if (canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
            }
        }

        isSprinting = sprintAction.IsPressed(); // Shift
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            canDoubleJump = false; // reset double jump
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            currentHP--;

            explosionParticle.Play();

            Destroy(collision.gameObject);

            // HP
            if (currentHP <= 0)
            {
                Debug.Log("Game Over!");
                gameOver = true;
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                dirtParticle.Stop();
                playerAudio.PlayOneShot(crashSfx);
            }
            else
            {
                playerAudio.PlayOneShot(crashSfx);
            }
        }
        else if (collision.gameObject.CompareTag("HealItem"))
        {
            currentHP++;
            explosionParticle.Play();
            Destroy(collision.gameObject);
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); // reset
        rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        playerAnim.SetTrigger("Jump_trig");
        dirtParticle.Stop();
        playerAudio.PlayOneShot(jumpSfx);
    }
}
