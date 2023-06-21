using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMovement : MonoBehaviour
{
    [SerializeField] float _speed;
    BoxCollider2D _box;
    float _groundWidth;
    float obstacleWidth;

    void Start()
    {
        if (gameObject.CompareTag("Ground"))
        {
            _box = GetComponent<BoxCollider2D>();
            _groundWidth = _box.size.x;
        }
        else if (gameObject.CompareTag("Obstacle"))
        {
            obstacleWidth = GameObject.FindGameObjectWithTag("Column").GetComponent<BoxCollider2D>().size.x;
        }
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameOver && GameManager.IsGameStarted)
        {
            transform.position = new Vector2(transform.position.x - _speed * Time.deltaTime, transform.position.y);
        }

        if (gameObject.CompareTag("Ground"))
        {
            if (transform.position.x <= -_groundWidth)
            {
                transform.position = new Vector2(transform.position.x + 2 * _groundWidth, transform.position.y);
            }
        }
        else if (gameObject.CompareTag("Obstacle"))
        {
            if (transform.position.x < GameManager.bottomLeft.x - obstacleWidth)
            {
                Destroy(gameObject);
            }
        }
    }
}
