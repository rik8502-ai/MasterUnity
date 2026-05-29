using UnityEngine;

public class WaveGimmick : MonoBehaviour
{
    [Header("移動スピード")]
    [SerializeField] private float speed = 2f;

    [Header("中心からの移動幅（片道分の距離）")]
    [SerializeField] private float amplitude = 3f;

    [Header("Y軸移動ですか？（X軸移動ならOFF）")]
    [SerializeField] private bool moveOnYAxis = true;

    private Vector3 startPosition;

    void Start()
    {
        // ゲーム開始時の初期位置（中心地点）を記憶しておく
        startPosition = transform.position;
    }

    void Update()
    {
        // ★ Mathf.Sin は、時間が経つと「-1 ～ 1」の間をフワフワと往復する値を返します
        // それに amplitude（振幅）をかけることで、「-3 ～ 3」のような滑らかな往復になります
        if (moveOnYAxis)
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * speed) * amplitude;
            transform.position = new Vector3(startPosition.x, newY, startPosition.z);
        }
        else
        {
            float newX = startPosition.x + Mathf.Sin(Time.time * speed) * amplitude;
            transform.position = new Vector3(newX, startPosition.y, startPosition.z);
        }
    }
}
