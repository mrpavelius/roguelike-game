using IJunior.TypedScenes;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            PlayerPrefs.SetInt("Records", 1);
            Level2.Load();
        }
    }
}