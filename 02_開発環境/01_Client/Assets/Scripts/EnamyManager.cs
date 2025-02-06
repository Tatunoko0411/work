using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnamyManager : MonoBehaviour
{
    [SerializeField] LayerMask blockLayer;
    [SerializeField] LayerMask EnemyLayer;
    [SerializeField] GameObject deathEfect;
    [SerializeField] GameObject Player;
    [SerializeField] AudioClip DethSE;
    AudioSource audioSorce;
    public enum DIRECTION_TYPE
    {
        STOP,
        RIGHT,
        LEFT,
    }

    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;

    Rigidbody2D rb2D;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        audioSorce = GetComponent<AudioSource>();
        direction = DIRECTION_TYPE.LEFT;
    }

    // Update is called once per frame
    private void Update()
    {
        if(!isGuround()||isWall()||isEnemy())
        {
            ChangeDirection();
        }
    }

    private void FixedUpdate()
    {
        switch (direction)
        {
            case DIRECTION_TYPE.STOP:
                speed = 0;
                break;
            case DIRECTION_TYPE.RIGHT:
                speed = 3;
                transform.localScale = new Vector3(-3, 3, 3);
                break;
            case DIRECTION_TYPE.LEFT:
                speed = -3;
                transform.localScale = new Vector3(3, 3, 3);
                break;

        }

        rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
    }

    bool isGuround()
    {
        Vector3 startVec = transform.position - transform.right * 0.166f * transform.localScale.x;
        Vector3 endVec = startVec - transform.up * 0.5f;
        Debug.DrawLine(startVec, endVec);
        return Physics2D.Linecast(startVec,endVec,blockLayer);
    }

    bool isWall()
    {
        Vector3 startVec = transform.position - transform.right * 0.166f * transform.localScale.x;
        Vector3 endVec = startVec + transform.right * 0.05f;
        Debug.DrawLine(startVec, endVec);
        return Physics2D.Linecast(startVec, endVec, blockLayer);
    }

    bool isEnemy()
    {
        Vector3 startVec = transform.position - transform.right * 0.15f * transform.localScale.x;
        Vector3 endVec = startVec + transform.right * 0.05f;
        Debug.DrawLine(startVec, endVec);
        return Physics2D.Linecast(startVec, endVec, EnemyLayer);
    }

    void ChangeDirection()
    {
        if (direction == DIRECTION_TYPE.RIGHT)
        {
            direction = DIRECTION_TYPE.LEFT;
        }
        else if (direction == DIRECTION_TYPE.LEFT)
        {
            direction = DIRECTION_TYPE.RIGHT;
        }
    }

    public void DestroyEnemy()
    {

        Instantiate(deathEfect,this.transform.position,this.transform.rotation);
        Destroy(this.gameObject);
    }

}
