using IJunior.TypedScenes;
using UnityEngine;

public class Portal2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            PlayerPrefs.SetInt("Records", 2);
            Level3.Load();
        }
    }
}