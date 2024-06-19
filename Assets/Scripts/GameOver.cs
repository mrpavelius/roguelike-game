using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private void Update()
    {
        if (_player == null)
        {
            int levels = PlayerPrefs.GetInt("Records");
            PlayerPrefs.SetInt("Records", levels++);
            Debug.Log("Игра окончена");
        }
    }
}