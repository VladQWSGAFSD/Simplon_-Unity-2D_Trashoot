using TMPro; 
using UnityEngine;

public class ScoreManager : MonoBehaviour, IScore
{
    public static ScoreManager Instance;
    public int score = 0;

    [SerializeField] private TMP_Text scoreText; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        UpdateScoreUI();
    }

    public void IncrementScore(int amount)
    {
        score += amount;
        //Debug.Log("incrementing score by: " + amount + ", new score: " + score);

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            return;
        }
    }


}
