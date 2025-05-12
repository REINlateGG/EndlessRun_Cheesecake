using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Vector3 spawnPos = new Vector3(25, 0, 0);
    public float startDelay = 2f;
    public float repeatRate = 2f;

    public GameObject healItemPrefab;         // ไอเทมเพิ่ม HP
    private PlayerController playerController;

    void Start()
    {
        InvokeRepeating("SpawnThing", startDelay, repeatRate);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnThing()
    {
        if (playerController == null || playerController.gameOver) return;

        float rand = Random.value;

        if (rand < 0.2f) // 20% Spawn Heal Item
        {
            Instantiate(healItemPrefab, spawnPos, Quaternion.identity);
        }
        else // 80% Spawn Obstacle
        {
            GameObject obj = ObstacleObjectPool.GetInstance().Acquire();
            obj.transform.position = spawnPos;
            obj.transform.rotation = Quaternion.identity;
        }
    }
}
