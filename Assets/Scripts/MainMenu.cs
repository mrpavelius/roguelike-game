using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _confirmationWindow;

    [SerializeField]
    private Button _playButton;

    [SerializeField]
    private Button _viewRecordsButton;

    [SerializeField]
    private Button _exitButton;

    public void StartGame()
    {
        Game.Load();
    }

    public void ViewRecords()
    {
        Records.Load();
    }

    public void QuitGame()
    {
        _playButton.interactable = false;
        _viewRecordsButton.interactable = false;
        _exitButton.interactable = false;
        _confirmationWindow.SetActive(true);
    }

    public void CloseApplication()
    {
        Application.Quit();
    }

    public void BackToGame()
    {
        _playButton.interactable = true;
        _viewRecordsButton.interactable = true;
        _exitButton.interactable = true;
        _confirmationWindow.SetActive(false);
    }
}