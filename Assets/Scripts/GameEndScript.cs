using UnityEngine;

public class GameEndScript : MonoBehaviour
{
    public void BackToMainMenu()
    {
        IJunior.TypedScenes.MainMenu.Load();
    }
}