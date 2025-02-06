using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] LayerMask blockLayer;
    [SerializeField] GameManager gameManager;


    public GameObject pointerObject;

    [SerializeField, Header("îºåa")]
    float radius;

    Animator animator;

    public enum DIRECTION_TYPE
    {
        STOP,
        RIGHT,
        LEFT,
    }

    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;

    Rigidbody2D rb2D;
    float speed;
    public float junpPower;
    public bool isDeath = false;
    private bool isGoal = false;
    private float xx;
    private Vector2 tempVelo;
    //SE
    [SerializeField] AudioClip getItemSE;
    [SerializeField] AudioClip jumpSE;
    [SerializeField] AudioClip stampSe;
    [SerializeField] AudioClip shotSE;

    AudioSource audioSorce;

    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletKnockback;
    public static int limitBullet;

    [SerializeField] List<GameObject> Pose;

    [SerializeField] Text timeText;
    public static float time = 0;

    private bool isPose=false;

    // Start is called before the first frame update
    void Start()
    {

        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSorce = GetComponent<AudioSource>();
        time = 0;
        limitBullet = 0;
        animator.SetInteger("Bullet", 5);
    }

    // Update is called once per frame
    private void Update()
    {
        

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePose();
        }

        if (isDeath == true|| isGoal == true||isPose==true)
        {
            return;
        }


        time += Time.deltaTime;
        timeText.text = time.ToString("F2");

        //Move();

        float x = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(x));


        if (x == 0)
        {
            direction = DIRECTION_TYPE.STOP;
        }
        else if (x > 0 && x > xx || x == 1)
        {
            direction = DIRECTION_TYPE.RIGHT;
        }
        else if (x < 0 && x < xx || x == -1) {


            direction = DIRECTION_TYPE.LEFT;
        }
        else
        {

            direction = DIRECTION_TYPE.STOP;
        }
        xx = x;

        if (isGuround())
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x - rb2D.velocity.x * 0.05f, rb2D.velocity.y);



            if (Input.GetKeyDown("space"))
            {
                jump();
            }
            limitBullet = 5;
        }
        else
        {

        }

        if (Input.GetMouseButtonDown(0) && limitBullet > 0)
        {
            Shot();
        }

    }

    private void FixedUpdate()
    {
        if (isDeath == true || isGoal == true || isPose == true)
        {
            return;
        }


        switch (direction)
        {
            case DIRECTION_TYPE.STOP:
                speed = 0;
                break;
            case DIRECTION_TYPE.RIGHT:
                speed = 30;
                transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
                break;
            case DIRECTION_TYPE.LEFT:
                speed = -30;
                transform.localScale = new Vector3(-0.08f, 0.08f, 0.08f);
                break;

        }



        rb2D.AddForce(new Vector2(speed, rb2D.velocity.y));

        if (rb2D.velocity.x >= 4)
        {
            rb2D.velocity = new Vector2(4, rb2D.velocity.y);
            
        }
        if (rb2D.velocity.x <= -4)
        {
            rb2D.velocity = new Vector2(-4, rb2D.velocity.y);
            
        }
        if (rb2D.velocity.y <= -22)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x,-22);
            
        }
    }

    void jump()
    {
        rb2D.AddForce(Vector2.up * junpPower);
        audioSorce.PlayOneShot(jumpSE);

    }

    public bool isGuround()
    {
        Vector3 leftStartpoint = transform.position - Vector3.right * 0.1f;
        Vector3 rightStartpoint = transform.position + Vector3.right * 0.1f;
        Vector3 endPoint = transform.position - Vector3.up * 0.1f;
        Debug.DrawLine(leftStartpoint, endPoint);
        Debug.DrawLine(rightStartpoint, endPoint);

        return Physics2D.Linecast(leftStartpoint, endPoint, blockLayer)
            || Physics2D.Linecast(rightStartpoint, endPoint, blockLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            PlayerDeath();
            gameManager.GameOver();
            Debug.Log("ÉQÅ[ÉÄÉIÅ[ÉoÅ[");
        }
        if (collision.gameObject.tag == "Finish")
        {
            isGoal = true;
            rb2D.velocity = Vector3.zero;
            gameManager.GameClear();
            Debug.Log("ÉQÅ[ÉÄÉNÉäÉA");
            Initiate.Fade("ResultScene", Color.black, 1.0f);
        }
        if (collision.gameObject.tag == "Item")
        {
            audioSorce.PlayOneShot(getItemSE);
            collision.gameObject.GetComponent<ItemManager>().GetItem();
        }
        if (collision.gameObject.tag == "Enemy")
        {

            EnamyManager enemy = collision.gameObject.GetComponent<EnamyManager>();

            //è„Ç©ÇÁì•ÇÒÇæÇÁ
            if (this.transform.position.y + 0.05f > enemy.transform.position.y)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
                jump();
                //ìGÇçÌèú
                enemy.DestroyEnemy();
            }
            else
            {
                //â°Ç©ÇÁÇ‘Ç¬Ç©ÇÈ
                gameManager.GameOver();
                PlayerDeath();
            }
        }
    }

    void PlayerDeath()
    {

        isDeath = true;
        rb2D.velocity = new Vector2(0, 0);
        speed = 0;
        rb2D.AddForce(Vector2.up * junpPower * 0.4f);
        animator.Play("PlayerDeathAnimation");
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        Destroy(boxCollider);
        gameManager.GameOver();
        Pointer.destroy = true;
    }

    void Shot()
    { 

        Vector2 playerPos = new Vector3(transform.position.x, transform.position.y + pointerObject.GetComponent<Pointer>().playerhight, transform.position.z);

        GameObject newbullet = Instantiate(bullet,
            playerPos,
            Quaternion.identity); //íeÇê∂ê¨
        Vector2 angle = (((Vector2)pointerObject.transform.position - (Vector2)playerPos) * bulletSpeed).normalized;
        newbullet.GetComponent<Rigidbody2D>().velocity = angle * bulletSpeed; //ÉLÉÉÉâÉNÉ^Å[Ç™å¸Ç¢ÇƒÇ¢ÇÈï˚å¸Ç…íeÇ…óÕÇâ¡Ç¶ÇÈ

        Vector2 angleKnock = -(((Vector2)pointerObject.transform.position - (Vector2)playerPos));

        if(angleKnock.y >0)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
        }
        rb2D.AddForce(angleKnock * bulletKnockback);

        Debug.Log(-angle * bulletKnockback);
        Destroy(newbullet, 0.3f); //2ïbå„Ç…íeÇè¡Ç∑
        audioSorce.PlayOneShot(shotSE);
        limitBullet--;


    }

    public static float GetTime()
    {
        return time;
    }

    public void ChangePose()
    {
        if (isPose)
        {
            isPose = false;
            for(int i=0;i<Pose.Count;i++)
            {
                Pose[i].SetActive(false);
            }
            rb2D.bodyType = RigidbodyType2D.Dynamic;
            rb2D.velocity = tempVelo;

        }
        else if (!isPose)
        {
            isPose = true;
            for (int i = 0; i < Pose.Count; i++)
            {
                Pose[i].SetActive(true);
               
            }
            rb2D.bodyType = RigidbodyType2D.Kinematic;
            tempVelo = rb2D.velocity;
            rb2D.velocity = Vector2.zero;
        }
    }
}
