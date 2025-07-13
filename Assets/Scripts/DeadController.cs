using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadController : MonoBehaviour
{
    public string nextSceneName = "MainMenu";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
