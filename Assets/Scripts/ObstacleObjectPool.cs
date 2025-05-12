using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectPool : MonoBehaviour
{
    public List<GameObject> obstaclePrefabs;
    public int initialSize = 10;

    private List<GameObject> pool = new List<GameObject>();
    private static ObstacleObjectPool instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public static ObstacleObjectPool GetInstance()
    {
        return instance;
    }

    void Start()
    {
        for (int i = 0; i < initialSize; i++)
        {
            CreateNewObstacle();
        }
    }

    private void CreateNewObstacle()
    {
        GameObject prefab = GetRandomObstaclePrefab();
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        obj.tag = "Obstacle";
        pool.Add(obj);
    }

    public GameObject Acquire()
    {
        if (pool.Count == 0)
        {
            CreateNewObstacle();
        }

        GameObject obj = pool[0];
        pool.RemoveAt(0);
        obj.SetActive(true);
        return obj;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        pool.Add(obj);
    }

    private GameObject GetRandomObstaclePrefab()
    {
        int index = Random.Range(0, obstaclePrefabs.Count);
        return obstaclePrefabs[index];
    }
}
