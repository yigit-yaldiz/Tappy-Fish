using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;

    public int ScoreCount => _score;

    int _score = 0;
    int _bestScore;

    #region Singleton Pattern
    public static Score Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public void IncreaseScore()
    {
        _score++;
        _scoreText.text= _score.ToString();
    }
}
