using UnityEngine;

public class GamePause : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    public static bool isGamePaused;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0;
        _panel.SetActive(true);
        AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1;
        _panel.SetActive(false);
        AudioListener.pause = false;
    }

    public void ExitToMainMenu()
    {
        ResumeGame();
        IJunior.TypedScenes.MainMenu.Load();
    }
}