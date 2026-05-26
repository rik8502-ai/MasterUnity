using UnityEngine;
using System.Collections; //コルーチンを使うために必要


public class PlayerAttack : MonoBehaviour
{
    public enum WeaponType
    {
        Club,
        Fireball
    }
    public WeaponType currentWeapon = WeaponType.Club;
    
    [Header("近接攻撃の設定")]
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private float attackTime = 0.5f; //攻撃時間

    [Header("遠距離攻撃の設定")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireballAttacktime = 0.5f; //遠距離攻撃のクールダウン時間

    private bool isAttacking = false; 
    private Animator anim; //プレイヤーのアニメーターを入れる箱

    [SerializeField] private Player player; //Playerスクリプトを入れる箱
    
    void Start()
    {
        if (attackCollider != null) attackCollider.enabled = false;
        //プレイヤーのAnimatorを取得してanimに入れる
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //左クリックを押すかつ攻撃中じゃなければ
        if (Input.GetMouseButtonDown(0) && isAttacking == false)
        {
            if ( currentWeapon == WeaponType.Club)
            {
                StartCoroutine(ClubAttack());
            }
            else if (currentWeapon == WeaponType.Fireball)
            {
                StartCoroutine(FireballAttack());
            }
        }
    
    }

    private IEnumerator ClubAttack()
    {
        Debug.Log("近接攻撃");
        //攻撃中であるためtrueにする
        isAttacking = true;
        
        //攻撃用オブジェクトの当たり判定をONにする
        attackCollider.enabled = true;

        anim.SetInteger("WeaponType", 1); //武器の種類をアニメーターに伝える（1はクラブ）
        anim.SetTrigger("Attack"); //攻撃のトリガーを引く


        //指定時間待機
        yield return new WaitForSeconds(attackTime);

        //攻撃用のオブジェクトの当たり判定をOFFにする
        attackCollider.enabled = false;
        isAttacking = false;
    }

    private IEnumerator FireballAttack()
    {
        //攻撃中であるためtrueにする
        isAttacking = true;

        anim.SetInteger("WeaponType", 2); //武器の種類をアニメーターに伝える（2はファイアーボール）
        anim.SetTrigger("Attack");

        if (fireballPrefab != null && firePoint != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f; // Z軸を0に固定

            if (player != null)
            {
                player.FaceToTarget(mousePos.x); // プレイヤーをマウスの方向に向ける
            }

            Vector2 direction = mousePos - firePoint.position; // FirePointからマウス位置への方向を計算
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 角度を計算
            Instantiate(fireballPrefab, firePoint.position, Quaternion.Euler(0f, 0f, angle));
        }
        else
        {
            Debug.LogWarning("ファイアーボールのプレハブかFirePointが設定されていません！");
        }

        //指定時間待機
        yield return new WaitForSeconds(fireballAttacktime);

        isAttacking = false;
    }


}
