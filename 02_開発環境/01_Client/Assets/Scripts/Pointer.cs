using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Pointer : MonoBehaviour
{
    public GameObject playerObject;

    [SerializeField, Header("”¼Œa")]
    float radius;
    public static bool destroy = false;


    [SerializeField] GameObject wire;
    [SerializeField] float WireSpeed;
    [SerializeField] LayerMask targetLayer;
    public float playerhight = 0.35f;
    static Vector3 playerPos;
    private bool wireMax = false;



    public Vector2 mousePos, worldPos;
    // Start is called before the first frame update
    void Start()
    {
        destroy = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y + playerhight, playerObject.transform.position.z);
        if (destroy) {

                Destroy(this.gameObject);
              
                return;

        }


        Move();

    }

    void Move()
    {
        transform.position = playerPos;
        mousePos = Input.mousePosition;

        worldPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x,mousePos.y));


        transform.position = Vector2.MoveTowards(
            transform.position,
            worldPos,
            radius);

        Vector3 dir = -(playerPos - transform.position);

       transform.rotation = Quaternion.FromToRotation(Vector3.up,dir);

       //transform.LookAt(playerObject.transform);


    }



    public static Vector3 getPlayerPos()
    {
        return playerPos;

    }

    private void ShotWireAction(Vector2 vec)
    {
        if(Wire.isRotating)
        {
            return;
        }
        wire.transform.localScale = new Vector3(wire.transform.localScale.x,wire.transform.localScale.y+WireSpeed,0);
        if(wire.transform.localScale.y > 10 )
        {
            wire.transform.localScale = new Vector3(0.03f, 0.1f, 1);
            wireMax = true;
        }

    }


}
