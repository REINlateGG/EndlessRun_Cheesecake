using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 10f;
    private float leftBound = -15;
    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        float currentSpeed = speed;

        if (!playerController.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * currentSpeed);
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            ObstacleObjectPool.GetInstance().Return(gameObject);
        }

    }
}
