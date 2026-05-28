using UnityEngine;

public class EnemyMoveState : MonoBehaviour
{

    //列挙型
    private enum State
    {
        Patrol,
        Chase,
    }

    //State型の変数に[Patrol]を代入　最初はパトロール状態からスタートする
    private State currentState = State.Patrol;

    //ターゲット
    private Transform target;

    //検知する距離
    [SerializeField] private float detectDistance = 3.0f;

    //巡回速度
    [SerializeField] private float patrolSpeed = 1.0f;

    //追跡速度
    [SerializeField] private float chaseSpeed = 3.0f;

    [Header("左右反転させるTransform")]
    [SerializeField] private Transform enemyTransform;

    //デフォルトのenemyTransformの回転
    private Quaternion defaultRotation;
    private Rigidbody2D rb;
    private int moveDirection = 1; // 1:右, -1:左
    private Vector3 lastPosition;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //Playerタグのオブジェクトを探してTransformを取得
        target = GameObject.FindGameObjectWithTag("Player").transform;
        defaultRotation = enemyTransform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    //物理演算を使うメソッドを呼び出すときにはFixedUpdateを使う
    private void FixedUpdate()
    {
        UpdateState();
    }

    private void CheckDistance()
    {
        //このオブジェクトとプレイヤーとの距離を計算
        float distance = Vector2.Distance(transform.position, target.position);

        //検知可能距離より近づいたら
        if (distance <= detectDistance)
        {
            //追跡状態に遷移
            currentState = State.Chase;
        }
        else
        {
            //パトロール状態に遷移
            currentState = State.Patrol;
        }
    }

    //現在のステートに応じた行動
    private void UpdateState()
    {
        switch (currentState)
        {
            //currentStateの状態がpatrolの時
            case State.Patrol:
                Patrol();
                break;
            //chaseの時
            case State.Chase:
                Chase();
                break;
        }
    }

    //巡回する
    private void Patrol()
    {
        //右向き
        if (moveDirection > 0)
        {
            rb.linearVelocityX = patrolSpeed;
            enemyTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
        //左向き
        else if (moveDirection < 0)
        {
            rb.linearVelocityX = -patrolSpeed;
            enemyTransform.rotation = Quaternion.Euler(0, 180, 0);
        }        
        // ★ Vector3.Distance ではなく、X座標の引き算（絶対値）に変更！
        float moveDistanceX = Mathf.Abs(transform.position.x - lastPosition.x);

        // 左右の移動距離が 0.01 未満なら「壁にブロックされている」と判定
        if (moveDistanceX < 0.01f)
        {
            moveDirection *= -1; // 1と-1をひっくり返す
        }
        
        lastPosition = transform.position;
    }

    //追跡する
    private void Chase()
    {
        //Sign:プラスかマイナスを返す
        float direction = Mathf.Sign(target.position.x - transform.position.x);

        //directionが1(右)ならマイナスをつけて左右反転
        moveDirection = (int)direction;

        if (moveDirection > 0)
        {
            enemyTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveDirection < 0)
        {
            enemyTransform.rotation = Quaternion.Euler(0, 180, 0);
        }

        //方向に従って進む
        rb.linearVelocityX = direction * chaseSpeed;
        
        
    }

    public void FlipDirection()
    {
        if (currentState == State.Patrol) moveDirection *= -1; // 1と-1をひっくり返す
    }

}
