using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats playerStats;

    public GameObject player;
    public Text amountOfHealth;
    public Slider healthBar;


    public float health;
    public float maxHealth;

    [Header("iFrames")]
    public float IFramesDuration;
    public int numberOfFlashes;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (playerStats != null)
        {
            Destroy(playerStats);
        }
        else
        {
            playerStats = this;
        }
        DontDestroyOnLoad(this);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        health = maxHealth;
        SetHealth();
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        CheckDeath();
        SetHealth();
        StartCoroutine(Invunerability());
    }

    private void SetHealth()
    {
        healthBar.value = CalculateHealthPercentage();
        amountOfHealth.text = Mathf.Ceil(health).ToString() + " / " + Mathf.Ceil(maxHealth).ToString();
    }

    private void CheckOverheal()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            health = 0;
            Destroy(player);
            return;
        }
    }

    private float CalculateHealthPercentage()
    {
        return (health / maxHealth);
    }

    public void AddHealth(float value)
    {
        health += value;
        CheckOverheal();
        SetHealth();
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(IFramesDuration / (numberOfFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(IFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
}