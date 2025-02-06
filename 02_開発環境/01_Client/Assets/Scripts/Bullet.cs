using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject pointerPrefab;
    [SerializeField] float speed;

   public LineRenderer line; // LineRenderer�R���|�[�l���g���󂯂�ϐ�
    int count; // ���̒��_�̐�

    Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>(); // LineRenderer�R���|�[�l���g���擾

    }

    // Update is called once per frame
    void Update()
    {
        count += 1; // ���_�����P���₷
        line.positionCount = count; // ���_���̍X�V
        line.SetPosition(count - 1, transform.position); // �I�u�W�F�N�g�̈ʒu�����Z�b�g
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
                //�G���폜
            enemy.DestroyEnemy();
            Destroy(this.gameObject);
        }
    }
}
