using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //追いかける対象
    [SerializeField] private Transform target;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;  
    [SerializeField] private float maxY;
    

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        //ターゲットがnullじゃなければ
        if (target != null)
        {
            //Clamp
            float x = Mathf.Clamp(target.position.x, minX, maxX);
            float y = Mathf.Clamp(target.position.y, minY, maxY);

            //カメラの位置をターゲットの位置に合わせる
            transform.position = new Vector3(x, y, transform.position.z);
            
        
        }
    }
}
