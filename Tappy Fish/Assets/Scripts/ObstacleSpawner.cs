using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject _obstacle;
    private float _timer;
    public float MaxTime;

    public float MaxY;
    public float MinY;
    float randomY;

    void SpawnObstacle()
    {
        randomY = Random.Range(MinY, MaxY);
        GameObject newObstacle = Instantiate(_obstacle,transform);
        newObstacle.transform.position = new Vector2(transform.position.x, randomY);
    }

    //timer 
    private void Update()
    {
        _timer += Time.deltaTime;
        
        if (_timer >= MaxTime && !GameManager.Instance.IsGameOver && GameManager.IsGameStarted)
        {
            SpawnObstacle();
            _timer = 0;
        }
    }
}
