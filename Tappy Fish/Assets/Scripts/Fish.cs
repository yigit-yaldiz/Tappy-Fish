using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fish : MonoBehaviour
{
    [SerializeField] float _speed; //default 9

    public Vector2 Velocity => _rb.velocity;

    Rigidbody2D _rb;
    //const float _clampValue = 4.6f;
    int _angle;
    int _maxAngle = 20;
    int _minAngle = -60;

    bool _touchedTheGround;

    [SerializeField] Image _clickImage;
    [SerializeField] AudioSource _scoreSound;

    Animator _animator;
    AudioSource _swimSound;

    ParticleController _particleController;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _rb.gravityScale = 0;

        _animator = GetComponent<Animator>();
        _swimSound = GetComponent<AudioSource>();
        _particleController = GetComponent<ParticleController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.Instance.IsGameOver)
        {
            GameManager.IsGameStarted = true;
            _clickImage.gameObject.SetActive(false);
            _rb.gravityScale = 1;
            _rb.velocity = new Vector2(_rb.velocity.x, _speed);
            _swimSound.Play();
            //_particleController.Emit();
        }   
        if (_rb.velocity.y > 0 && _angle <= _maxAngle)
        {
            _angle += 4;
        }
        else if (_rb.velocity.y < -2.5f && _angle > _minAngle)
        {
            _angle -= 1;
        }

        if (!_touchedTheGround)
        {
            transform.rotation = Quaternion.Euler(0, 0, _angle);
        }

        //Vector3 pos = transform.position;
        //pos.y = Mathf.Clamp(transform.position.y, -_clampValue, _clampValue);
        //transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Score.Instance.IncreaseScore();
            _scoreSound.Play();
        }
        else if (collision.CompareTag("Column") && !GameManager.Instance.IsGameOver)
        {
            GameManager.Instance.GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (GameManager.Instance.IsGameOver)
            {
                GameOver();
            }
            else
            {
                GameManager.Instance.GameOver();
                GameOver();
            }
        }
    }

    void GameOver()
    {
        _touchedTheGround = true;
        transform.rotation = Quaternion.Euler(0, 0, -90);
        _animator.enabled = false;
    }
}
