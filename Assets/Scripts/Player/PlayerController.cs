using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float dashRange;
    public float speed;
    private Vector2 direction;
    private Vector2 targetPos;
    private Animator animator;

    private enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    };

    private Direction facing = Direction.DOWN;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        TakeInput();
        Move();
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (direction.x != 0 || direction.y != 0)
        {
            SetAnimatorMovement(direction);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }

    private void TakeInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
            facing = Direction.UP;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
            facing = Direction.LEFT;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
            facing = Direction.DOWN;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
            facing = Direction.RIGHT;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            Vector2 currentPos = transform.position;
            targetPos = Vector2.zero;
            if (facing == Direction.UP)
            {
                targetPos.y = 1;
            }
            else if (facing == Direction.DOWN)
            {
                targetPos.y = -1;
            }
            else if (facing == Direction.RIGHT)
            {
                targetPos.x = 1;
            }
            else if (facing == Direction.LEFT)
            {
                targetPos.x = -1;
            }
            transform.Translate(targetPos * dashRange);
        }
    }

    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);
        animator.SetFloat("xDir", direction.x);
        animator.SetFloat("yDir", direction.y);
    }
}