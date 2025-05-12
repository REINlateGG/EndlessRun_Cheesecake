using UnityEngine;
using UnityEngine.UI;

public class GameBGMManager : MonoBehaviour
{
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

    void OnToggleMusic(bool on)
    {
        if (gameBGM != null)
        {
            gameBGM.mute = !on;
        }

        PlayerPrefs.SetInt("music", on ? 1 : 0);
        PlayerPrefs.Save();
    }
}
