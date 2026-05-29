using UnityEngine;
using System.Collections; 

public class EnemyAttack : MonoBehaviour
{
    //攻撃オブジェクト
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private float attackTime = 0.5f;
    [SerializeField] private float attackInterval = 2f;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private Animator anim;
    void Start()
    {
        //最初は攻撃用のオブジェクトの当たり判定をOFFにする
        attackCollider.enabled = false;
    }

    void Update()
    {
        //攻撃中じゃなければ
        if (!isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        //攻撃中であるためtrueにする
        isAttacking = true;
        //攻撃用オブジェクトの当たり判定をONにする
        attackCollider.enabled = true;
        //敵の攻撃アニメーションON
        anim.SetBool("Attack" , true);
        //指定時間待機
        yield return new WaitForSeconds(attackTime);
        //敵の攻撃アニメーションOFF
        anim.SetBool("Attack", false);
        //攻撃用のオブジェクトの当たり判定をOFFにする
        attackCollider.enabled = false;
        //攻撃間隔時間の分待機
        yield return new WaitForSeconds(attackInterval);
        //攻撃が終わったためfalseにする
        isAttacking = false;
    }


}
