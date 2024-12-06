using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public float sizeMap;

    [Header("Obstaculos")]
    public GameObject[] obstacles;
    public Vector2 numberOfObstacles;
    public List<GameObject> newObstacles;

    [Header("Coletaveis")]
    public GameObject[] collectablesCan;
    public Vector2 numberOfCollectablesCan;
    public List<GameObject> newCollectablesCan;

    // Start is called before the first frame update
    void Start()
    {
        PositionObstacle();
        PositionObstacleMap();

        PositionCoinsCan();
        PositionCoinCanMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PositionObstacleMap()
    {
        for (int i = 0; i < newObstacles.Count; i++)
        {
            float posZMin = (sizeMap / newObstacles.Count) + (sizeMap / newObstacles.Count) * i;
            float posZMax = (sizeMap / newObstacles.Count) + (sizeMap / newObstacles.Count) * i + 1;
            newObstacles[i].transform.localPosition = new Vector3(0, 0, Random.Range(posZMin, posZMax));
            newObstacles[i].SetActive(true);
            if (newObstacles[i].GetComponent<ChangeLane>() != null)
            {
                newObstacles[i].GetComponent<ChangeLane>().PositionLane();
            }
        }
    }

    void PositionObstacle()
    {
        int newNumberOfObstacles = (int)Random.Range(numberOfObstacles.x, numberOfObstacles.y);

        for (int i = 0; i < newNumberOfObstacles; i++)
        {
            newObstacles.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform));
            newObstacles[i].SetActive(false);
        }
    }

    void PositionCoinsCan()
    {
        int newNumberOfCoinsCan = (int)Random.Range(numberOfCollectablesCan.x, numberOfCollectablesCan.y);

        for (int i = 0; i < newNumberOfCoinsCan; i++)
        {
            newCollectablesCan.Add(Instantiate(collectablesCan[Random.Range(0, collectablesCan.Length)], transform));
            newCollectablesCan[i].SetActive(false);
        }
    }

    void PositionCoinCanMap()
    {
        float minZPos = 10f;
        for (int i = 0; i < newCollectablesCan.Count; i++)
        {
            float maxZpos = minZPos + 5f;
            float randomZpos = Random.Range(minZPos, maxZpos);
            newCollectablesCan[i].transform.localPosition = new Vector3(transform.position.x, transform.position.y, randomZpos);
            newCollectablesCan[i].SetActive(true);
            newCollectablesCan[i].GetComponent<ChangeLane>().PositionLane();
            minZPos = maxZpos + 1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = new Vector3(0, 0, transform.position.z + sizeMap * 2);            
            //Invoke("PositionMap", 4f);
            PositionObstacleMap();
            PositionCoinCanMap();
        }
    }
}
