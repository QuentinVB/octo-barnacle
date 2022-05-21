using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    public GameObject settingsWindow;

    public void StartGame(){
    //    SceneManager.LoadScene(levelToLoad);
    SceneManager.LoadScene("TestLevel",LoadSceneMode.Single);
    }

    public void SettingsButton(){
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsButton(){
        settingsWindow.SetActive(false);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
