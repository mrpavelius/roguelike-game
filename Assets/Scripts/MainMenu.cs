using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        Game.Load();
    }

    public void ViewRecordsButton()
    {
        Records.Load();
    }

    public void ExitButton()
    {
        Exit.Load();
    }
}