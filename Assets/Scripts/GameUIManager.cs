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
        // โหลดค่าที่บันทึกไว้จาก PlayerPrefs
        bool musicOn = PlayerPrefs.GetInt("music", 1) == 1;

        // ตั้งสถานะ Toggle และ BGM
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
        SceneManager.LoadScene("StartScenes"); // เปลี่ยนชื่อให้ตรงกับซีนของเธอ
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
