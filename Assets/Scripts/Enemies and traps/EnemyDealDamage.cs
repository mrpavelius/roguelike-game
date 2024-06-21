using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDealDamage : MonoBehaviour
{
    public GameObject projectile;
    public GameObject player;
    public float minDamage;
    public float maxDamage;
    public float projectileForce;
    public float cooldown;
    private Animator animator;

    public float damage;

    [SerializeField] private AudioClip _projectileSoundClip;

    private AudioSource _audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Vector2.Distance(transform.position, player.transform.position) >= 1)
        {
            collision.GetComponent<PlayerStats>().DealDamage(damage);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(shootPlayer());
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    private IEnumerator shootPlayer()
    {
        yield return new WaitForSeconds(cooldown);
        if (player != null)
        {
            _audioSource.clip = _projectileSoundClip;
            _audioSource.Play();
            GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 myPos = transform.position;
            Vector2 enemyPos = player.transform.position;
            Vector2 direction = (enemyPos - myPos).normalized;
            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            spell.GetComponent<EnemyProjectile>().damage = Random.Range(minDamage, maxDamage);
            StartCoroutine(shootPlayer());
            animator.SetTrigger("Attack");
        }
    }
}