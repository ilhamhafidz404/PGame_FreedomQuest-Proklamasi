using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;
    }

    public void OnVolumeChanged()
    {
        AudioListener.volume = volumeSlider.value;
    }

    public void SaveAndBack()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
