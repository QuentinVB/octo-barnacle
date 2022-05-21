using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;
    Resolution[] resolutions; 

    public void Start(){

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
        }

        resolutionDropdown.AddOptions(options);
    }

    public void SetVolume(float volume){
    //    Debug.Log("volume du jeu : " + volume);
    audioMixer.SetFloat("volume", volume);
    }

    public void SetFullScreen(bool isFullScreen){
        Screen.fullScreen = isFullScreen;
    }

}
