using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwhitchBodyType : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // 領域の中にプレイヤーが入ってきたら、オブジェクトのボディタイプを変更
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }


}
