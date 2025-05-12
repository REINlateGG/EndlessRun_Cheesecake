using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("PlayScenes");
    }
    public void ExitGame()
    {
        
    }
}
