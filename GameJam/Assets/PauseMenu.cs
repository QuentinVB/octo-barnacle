using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUi;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){

            if(gameIsPaused){
                Resume();
            }else{
                Paused();
            }
        }
    }

    void Paused(){

        //afficher le menu pause
        pauseMenuUi.SetActive(true);
        //arreter le temps 
        Time.timeScale = 0;
        //changer le status du jeu
        gameIsPaused = true;

    }


    public void Resume(){

        //enlever le menu pause
        pauseMenuUi.SetActive(false);
        //reprendre le temps 
        Time.timeScale = 1;
        //changer le status du jeu
        gameIsPaused = false;

    }

    public void LoadMainMenu(){
     SceneManager.LoadScene("MainMenu",LoadSceneMode.Single);
    }
}
