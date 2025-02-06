using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Wire : MonoBehaviour
{
    public static bool isRotating = false; // 円運動中かどうか
    private Vector3 velocity = Vector3.zero; // 現在の速度
    private Vector3 acceleration = Vector3.zero; // 現在の加速度
    public float radius; // 円運動の半径
    public float angularSpeed; // 円運動の角速度（ラジアン/秒）
    public float mass ; // オブジェクトの質量
    [SerializeField] GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block") // 壁のタグが"Wall"の場合
        {
            if (isRotating == true)
            {
               //return;
            }
            isRotating = true; // 円運動を開始

            // spring設定
            collision.gameObject.AddComponent<SpringJoint2D>();
            collision.gameObject.GetComponent<SpringJoint2D>().autoConfigureConnectedAnchor = false;
            collision.gameObject.GetComponent<SpringJoint2D>().anchor = player.transform.position;
            collision.gameObject.GetComponent<SpringJoint2D>().connectedBody = player.transform.gameObject.GetComponent<Rigidbody2D>();
            collision.gameObject.GetComponent<SpringJoint2D>().connectedAnchor = collision.transform.position;
            collision.gameObject.GetComponent<SpringJoint2D>().dampingRatio = 100;
 
            player.AddComponent<SpringJoint2D>();
            player.GetComponent<SpringJoint2D>().connectedBody = collision.transform.gameObject.GetComponent<Rigidbody2D>();
            player.GetComponent<SpringJoint2D>().anchor = collision.transform.position;
            player.GetComponent<SpringJoint2D>().connectedAnchor = player.transform.position;
            player.GetComponent<SpringJoint2D>().dampingRatio = 100;

        }
        else
        {
            isRotating = false ;
            Destroy(player.GetComponent<SpringJoint2D>());
            Destroy(collision.gameObject.GetComponent<SpringJoint2D>());
        }
    }

    void Update()
    {
        //if (isRotating)
        //{
        //    // 現在の位置から基点への方向を計算
        //    Vector3 directionToPivot = (pivotPoint - player.transform.position).normalized;

        //    // 向心力に基づく加速度を計算
        //    float centripetalForce = (angularSpeed * angularSpeed) * radius * mass; // F = m * ω² * r
        //    acceleration = directionToPivot * centripetalForce / mass; // a = F / m

        //    // 速度を加速度で更新
        //    player.GetComponent<Rigidbody2D>().velocity += (Vector2)acceleration * Time.deltaTime;

        //    // 位置を速度で更新
        //    // transform.position += velocity * Time.deltaTime;

        
    }
}
