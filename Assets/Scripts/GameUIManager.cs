using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public PlayerController player;

    void Update()
    {
        if (!player.gameOver)
        {
            scoreText.text = "Score: " + player.score;
        }
    }
}
