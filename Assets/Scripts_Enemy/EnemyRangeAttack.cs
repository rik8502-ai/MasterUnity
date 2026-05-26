using UnityEngine;
using System.Collections;

public class EnemyRangeAttack : MonoBehaviour
{
    [Header("発射する弾（矢）のプレハブ")]
    [SerializeField] private GameObject arrowPrefab;

    [Header("弾を発射する位置")]
    [SerializeField] private Transform arrowPoint;

    [Header("攻撃アニメーションの長さ（弾が出るまでの時間）")]
    [SerializeField] private float attackTime = 0.5f;

    [Header("攻撃間隔")]
    [SerializeField] private float attackInterval = 2f;

    [Header("左右反転させるTransform")]
    [SerializeField] private Transform enemyTransform;

    private bool isAttacking = false;

    [Header("敵のAnimator")]
    [SerializeField] private Animator anim;

    [SerializeField] private Player player;

    [Header("攻撃する範囲")]
    [SerializeField] private float attackRange = 10f;

    void Update()
    {
        //プレイヤーと自分の距離を計算する
        float distance = Vector2.Distance(transform.position, player.transform.position);
        
        if (distance <= attackRange )
        {
            FaceToPlayer();
            if (!isAttacking)
            {
            StartCoroutine(Attack());
            }
        }
        //範囲外に出たら何もしない（見失う）
    }

    private void FaceToPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            
            if (player.transform.position.x > transform.position.x)
            {
                // プレイヤーが右にいる場合、右を向く
                enemyTransform.rotation = Quaternion.Euler(0, 0, 0);
                //Debug.Log("右を向く");
            }
            else if (player.transform.position.x < transform.position.x)
            {
                // プレイヤーが左にいる場合、左を向く
                enemyTransform.rotation = Quaternion.Euler(0, 180, 0);
                //Debug.Log("左を向く");
            }
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;

        // 敵の攻撃アニメーションON
        if (anim != null) anim.SetBool("Attack", true);

        // 矢を撃つモーションに合わせて少し待機（振りかぶり時間など）
        yield return new WaitForSeconds(attackTime);

        // 矢（プレハブ）を生成して飛ばす
        if (arrowPrefab != null && arrowPoint != null)
        {
            Instantiate(arrowPrefab, arrowPoint.position, arrowPoint.rotation);
        }

        // 敵の攻撃アニメーションOFF
        if (anim != null) anim.SetBool("Attack", false);

        // 攻撃間隔時間の分待機
        yield return new WaitForSeconds(attackInterval);

        isAttacking = false;
    }

    // --- ★プロのテクニック：エディタ上で索敵範囲を「見える化」する ---
    private void OnDrawGizmosSelected()
    {
        // 赤色の線で設定する
        Gizmos.color = Color.red;
        // 自分の位置を中心に、attackRangeの広さの円を描く
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}