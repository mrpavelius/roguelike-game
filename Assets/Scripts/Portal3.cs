using IJunior.TypedScenes;
using UnityEngine;

public class Portal3 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            PlayerPrefs.SetInt("Records", 3);
            GameEnd.Load();
        }
    }
}