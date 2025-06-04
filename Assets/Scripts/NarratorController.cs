using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NarratorController : MonoBehaviour
{
    public Text narrationText; 
    public string[] narrationSlides;
    private int currentSlide = 0;

    void Start()
    {
        ShowSlide();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            currentSlide++;
            if (currentSlide < narrationSlides.Length)
            {
                ShowSlide();
            }
            else
            {
                SceneManager.LoadScene("PlayScene");
            }
        }
    }

    void ShowSlide()
    {
        narrationText.text = narrationSlides[currentSlide];
    }
}
