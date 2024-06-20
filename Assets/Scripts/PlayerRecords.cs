using UnityEngine;
using TMPro;

public class PlayerRecords : MonoBehaviour
{
    [SerializeField] private TMP_Text _worldsPassed;

    private void Start()
    {
        _worldsPassed.text = PlayerPrefs.GetInt("Records").ToString();
    }

    public void BackToMainMenu()
    {
        IJunior.TypedScenes.MainMenu.Load();
    }
}