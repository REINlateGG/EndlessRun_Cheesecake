using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUI : MonoBehaviour
{
    public GameObject optionPanel;
    public GameObject playHistoryPanel;

    public Toggle musicToggle;
    public AudioSource bgm;

    public TextMeshProUGUI[] scoreTexts;

    void Start()
    {
        bool musicOn = PlayerPrefs.GetInt("music", 1) == 1;

        musicToggle.isOn = musicOn;
        bgm.mute = !musicOn;
    }

    public void OnPlay()
    {
        SceneManager.LoadScene("PlayScenes");
    }

    public void OnOption()
    {
        optionPanel.SetActive(true);
        playHistoryPanel.SetActive(false);
    }

    public void OnPlayHistory()
    {
        playHistoryPanel.SetActive(true);
        optionPanel.SetActive(false);

        // โหลดคะแนน
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            int score = PlayerPrefs.GetInt("score" + i, 0);
            scoreTexts[i].text = $"Round: {i + 1}: {score}";
        }
    }

    public void OnExit()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }

        public void OnToggleMusic(bool on)
    {
        if (bgm != null)
        {
            bgm.mute = !on;
            PlayerPrefs.SetInt("music", on ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public void OnBack()
    {
        optionPanel.SetActive(false);
        playHistoryPanel.SetActive(false);
    }
}
