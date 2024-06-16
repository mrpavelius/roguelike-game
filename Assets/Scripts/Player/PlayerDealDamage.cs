using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDealDamage : MonoBehaviour
{
    public GameObject projectile;
    public float minDamage;
    public float maxDamage;
    public float projectileForce;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 myPos = transform.position;
            Vector2 direction = (mousePosition - myPos).normalized;
            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            spell.GetComponent<PlayerProjectile>().damage = Random.Range(minDamage, maxDamage);       
        }
    }
}