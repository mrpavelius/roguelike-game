using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f; // �������� �������� �����
    public float stoppingDistance = 1f; // ����������, �� ������� ���� �����������

    private Transform player; // ������ �� ������ ������
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isPlayerInRange = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isPlayerInRange && player != null)
        {
            // ���������� ����������� � ������
            movement = (player.position - transform.position).normalized;
        }
        else
        {
            movement = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (isPlayerInRange && player != null && Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            // ������� �����, ���� �� ������ stoppingDistance
            rb.MovePosition((Vector2)transform.position + (moveSpeed * Time.fixedDeltaTime * movement));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}