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

    // �̈�̒��Ƀv���C���[�������Ă�����A�I�u�W�F�N�g�̃{�f�B�^�C�v��ύX
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }


}
