using UnityEngine;
using TMPro; // ★TextMeshProを使うために必要！

public class DamageText : MonoBehaviour
{
    [Header("文字のコンポーネント")]
    [SerializeField] private TextMeshPro textMesh;

    [Header("上に移動するスピード")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("文字が消えるまでの時間")]
    [SerializeField] private float lifeTime = 1f;

    [Header("透明になるスピード")]
    [SerializeField] private float fadeSpeed = 2f;

    private Color textColor;

    // ★外部（プレイヤーの攻撃スクリプトなど）から呼ばれるセットアップ処理
    public void Setup(int damageAmount)
    {
        // 渡されたダメージの数字を文字としてセットする
        textMesh.text = damageAmount.ToString();
        
        // 現在の色を保存しておく（後で透明にするため）
        textColor = textMesh.color;

        // 指定秒数後に自分自身を削除する
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // 毎フレーム、少しずつ上へ移動する
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);

        // アルファ値（透明度）を少しずつ減らしてフェードアウトさせる
        textColor.a -= fadeSpeed * Time.deltaTime;

        textMesh.color = textColor;
    }
}