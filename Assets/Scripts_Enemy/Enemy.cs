using UnityEngine;

public class Enemy : MonoBehaviour
{
    //komento
    //移動スピード
    [SerializeField] private float moveSpeed = 5.0f;

    private Rigidbody2D rb;

    [SerializeField] Transform enemyTransform;

    //現在の回転
    [SerializeField] private Quaternion currentRotation;

    public int moveDirection = 1; // 1:右, -1:左
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Walk();
    }

    private void Walk()
    {
        rb.linearVelocityX = moveDirection * moveSpeed;
        if (moveDirection > 0)
        {
            enemyTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveDirection < 0)
        {
            enemyTransform.rotation = Quaternion.Euler(0, 180, 0);
        }

        
    }

    public void FlipDirection()
    {
        moveDirection *= -1; // 1と-1をひっくり返す
    }
}
