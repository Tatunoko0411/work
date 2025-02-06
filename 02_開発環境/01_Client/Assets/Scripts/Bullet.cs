using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject pointerPrefab;
    [SerializeField] float speed;

   public LineRenderer line; // LineRendererコンポーネントを受ける変数
    int count; // 線の頂点の数

    Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>(); // LineRendererコンポーネントを取得

    }

    // Update is called once per frame
    void Update()
    {
        count += 1; // 頂点数を１つ増やす
        line.positionCount = count; // 頂点数の更新
        line.SetPosition(count - 1, transform.position); // オブジェクトの位置情報をセット
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {

            EnamyManager enemy = collision.gameObject.GetComponent<EnamyManager>();
                //敵を削除
            enemy.DestroyEnemy();
            Destroy(this.gameObject);
        }
    }
}
