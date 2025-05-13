using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public PlayerController player;

    public GameObject gameOverPanel;

    public AudioSource gameBGM;
    public Toggle musicToggle;

void Start()
    {
        bool musicOn = PlayerPrefs.GetInt("music", 1) == 1;
        
        if (musicToggle != null)
        {
            musicToggle.isOn = musicOn;
            musicToggle.onValueChanged.AddListener(OnToggleMusic);
        }

        if (gameBGM != null)
        {
            gameBGM.mute = !musicOn;
        }
    }

    void Update()
    {
        if (!player.gameOver)
        {
            scoreText.text = "Score: " + player.score;
        }
    }

     public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void OnPlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene("StartScenes");
    }

     public void OnExit()
    {
        Application.Quit();
    }

    public void OnToggleMusic(bool on)
    {
        if (gameBGM != null)
        {
            gameBGM.mute = !on;
            PlayerPrefs.SetInt("music", on ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}
