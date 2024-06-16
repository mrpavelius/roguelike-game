using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionCollectable : MonoBehaviour
{
    public float healthValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerStats>().AddHealth(healthValue);
            gameObject.SetActive(false);
        }
    }
}