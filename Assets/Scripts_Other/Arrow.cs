using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("弾の飛ぶスピード")]
    [SerializeField] private float speed = 12f; // 時間ではなく、スピードに戻しました！
    
    [Header("攻撃力")]
    [SerializeField] private int damage = 1;
    
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // 【注意】Rigidbody2Dの Gravity Scale は「1」にして重力をONにしておいてください
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && rb != null)
        {
            // 1. プレイヤーとゴブリンの「距離」を測る
            float distance = Vector2.Distance(transform.position, player.transform.position);

            // 2. 距離とスピードから、「何秒で到達するか」を自動計算する（これが大正解の処理！）
            float autoTime = distance / speed;

            // 3. 割り出した時間を使って、放物線の初速を計算する
            Vector2 initialVelocity = CalculateVelocity(transform.position, player.transform.position, autoTime);
            
            // 4. 計算した初速をセット！
            rb.linearVelocity = initialVelocity;
        }
        else if (rb != null)
        {
            rb.linearVelocity = transform.right * speed;
        }

        Destroy(gameObject, 5f);
    }

    // 毎フレーム、飛んでいる方向（落ちている方向）に矢の先端を向ける
    private void FixedUpdate()
    {
        if (rb != null && rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    // 放物線の初速を割り出す計算式
    private Vector2 CalculateVelocity(Vector2 start, Vector2 target, float time)
    {
        Vector2 distance = target - start;
        float gravity = Physics2D.gravity.y * rb.gravityScale; 

        float velocityX = distance.x / time;
        // 重力で落ちる分を計算して、少し上向きに撃ち出す
        float velocityY = (distance.y - 0.5f * gravity * time * time) / time;

        return new Vector2(velocityX, velocityY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHP playerHP = collision.GetComponent<PlayerHP>();
            if (playerHP != null) playerHP.TakeDamage(damage);
            
            Destroy(gameObject);
        }
        // 壁（Ground）に当たった時の処理などを追加してもOKです
    }
}