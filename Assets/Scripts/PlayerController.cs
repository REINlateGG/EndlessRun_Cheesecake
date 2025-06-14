using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float gravityModifier;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSfx;
    public AudioClip crashSfx;
    public AudioClip HealSfx;

    private Rigidbody rb;
    private InputAction jumpAction;
    private bool isOnGround = true;
    private bool canDoubleJump = false;

    private Animator playerAnim;
    private AudioSource playerAudio;

    public bool gameOver = false;
    

    public int maxHP = 3;
    public int currentHP;
    public Slider hpSlider;

    //score
    public float moveSpeed = 10f;
    private float runTime = 0f;
    public int score = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    void Start()
    {
        Physics.gravity = new Vector3(0, -9.81f * gravityModifier, 0);

        jumpAction = InputSystem.actions.FindAction("Jump");

        gameOver = false;
        currentHP = maxHP;

        hpSlider.maxValue = maxHP;
        hpSlider.value = currentHP;

    }

    void Update()
    {
        if (jumpAction.triggered && !gameOver)
        {
            if (isOnGround)
            {
                Jump();
                isOnGround = false;
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
            }
        }

        if (!gameOver)
        {
            runTime += Time.deltaTime;
            float distance = runTime * moveSpeed;
            score = Mathf.FloorToInt(distance);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            canDoubleJump = false;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            currentHP--;
            explosionParticle.Play();
            hpSlider.value = currentHP;
            Destroy(collision.gameObject);

            // HP
            if (currentHP <= 0)
            {
                gameOver = true;
                SaveScore(score);
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                dirtParticle.Stop();
                playerAudio.PlayOneShot(crashSfx);
                GameObject.Find("GameUIManager").GetComponent<GameUIManager>().ShowGameOver();
            }
            else
            {
                playerAudio.PlayOneShot(crashSfx);
            }
        }
        else if (collision.gameObject.CompareTag("HealItem"))
        {
            if (currentHP < maxHP)
            {
                currentHP++;
                hpSlider.value = currentHP;
                playerAudio.PlayOneShot(HealSfx);
            }
            else
            {
                playerAudio.PlayOneShot(crashSfx);
            }

            explosionParticle.Play();
            Destroy(collision.gameObject);
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        playerAnim.SetTrigger("Jump_trig");
        dirtParticle.Stop();
        playerAudio.PlayOneShot(jumpSfx);
    }

    void SaveScore(int newScore)
    {
        List<int> history = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            history.Add(PlayerPrefs.GetInt("score" + i, 0));
        }

        history.Insert(0, newScore);
        if (history.Count > 5) history.RemoveAt(5);
        for (int i = 0; i < history.Count; i++)
        {
            PlayerPrefs.SetInt("score" + i, history[i]);
        }
        
        PlayerPrefs.Save();

    }

}
