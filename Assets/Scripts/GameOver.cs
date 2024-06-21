using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private void Update()
    {
        if (_player == null)
        {
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                PlayerPrefs.SetInt("Records", 0);
            }

            IJunior.TypedScenes.MainMenu.Load();
        }
    }
}