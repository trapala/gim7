using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreText;

    int score;

    // Use this for initialisation
    void Start()
    {
        RefreshCountText();
    }

    // Update the score.
    public void AddPoints(short points)
    {
        score += points;
        RefreshCountText();
    }

    // Reproduce the score text.
    void RefreshCountText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
