using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static Vector2 bottomLeft;

    public List<Sprite> MedalList = new List<Sprite>();
    public static GameManager Instance { get; private set; }
    public bool IsGameOver => _isGameOver;

    #region Panel Items
    [SerializeField] GameObject _scoreBoardPanel;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _bestScoreText;
    [SerializeField] Image _medal;
    [SerializeField] Image _newText;
    #endregion

    int _bestScore;

    bool _isGameOver;
    public static bool IsGameStarted;

    AudioSource _deadSound;

    void Awake()
    {
        Instance = this;
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        _deadSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _bestScore = PlayerPrefs.GetInt("Bestscore");
        _bestScoreText.text = _bestScore.ToString();
        IsGameStarted = false;
    }

    public void GameOver()
    {
        _isGameOver = true;
        _deadSound.Play();
        _scoreBoardPanel.SetActive(true);

        _scoreText.text = Score.Instance.ScoreCount.ToString();

        if (Score.Instance.ScoreCount > _bestScore)
        {
            _bestScore = Score.Instance.ScoreCount;
            PlayerPrefs.SetInt("Bestscore", Score.Instance.ScoreCount);

            _newText.gameObject.SetActive(true);
            _bestScoreText.text = _bestScore.ToString();
        }
        else
        {
            _bestScore = PlayerPrefs.GetInt("Bestscore");
            _bestScoreText.text = _bestScore.ToString();
        }

        CheckMedalStatus();
    }
    
    void CheckMedalStatus()
    {
        if (Score.Instance.ScoreCount > 5)
        {
            _medal.sprite = MedalList[1];
        }
        else if (Score.Instance.ScoreCount > 10)
        {
            _medal.sprite = MedalList[2];
        }
        else if (Score.Instance.ScoreCount > 15)
        {
            _medal.sprite = MedalList[3];
        }
    }


    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
