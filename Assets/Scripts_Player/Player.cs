using UnityEngine;

public class Player : MonoBehaviour
{
    //移動スピード
    [SerializeField] private float moveSpeed = 10.0f;
    //ジャンプの力
    [SerializeField] private float jumpForce = 15.0f;

    //プレイヤーの足元にある子オブジェクト
    [SerializeField] private Transform groundCheck;
    //地面をチェックする円の半径
    [SerializeField] private float groundCheckRadius = 0.1f;
    //地面のレイヤー
    [SerializeField] private LayerMask groundLayer;
    //地面にいるかどうか
    private bool isGrounded = false;

    //プレイヤーのRigidBodyを入れる箱
    private Rigidbody2D rb;

    //プレイヤーのAnimatorを入れる箱
    [SerializeField] private Animator anim;



    // ゲーム開始時一回呼ばれる
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Walk();
        Jump();
    }

    private void Walk()
    {
        //入力を取得
        float direction = Input.GetAxis("Horizontal");

        //Rigidbodyに速度を設定
        rb.linearVelocityX = direction * moveSpeed;

        // 左を向く場合
        if (direction > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        // 右を向く場合
        else if (direction < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        //歩行をしているときは
        if (direction != 0)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }

    private void Jump()
    {
        //OverlapCircleで地面にいるかどうかをチェック
        if(Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //スペースキーかWキーが押されていて、地面にいるとき
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded == true)
        {
            //上方向に力を加える
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void FaceToTarget(float targetX)
    {
        if (targetX > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (targetX < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    
}
