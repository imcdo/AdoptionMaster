using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    enum moveDirection { UP, DOWN, LEFT, RIGHT }

    float maxX;
    float minX;
    float maxY;
    float minY;

    private Stats stats;
    
    [Tooltip("Max dog moveSpeed")]
    [SerializeField] private float maxSpeed = 0.5f;
    private float moveSpeed;
    [SerializeField] public Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Stats>();

        Camera main = Camera.main;

        var topLeft = main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        var bottomRight = main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0));

        minX = topLeft.x;
        maxX = bottomRight.x;

        minY = bottomRight.y;
        maxY = topLeft.y;

       // Debug.Log(minX + " " + maxX + " " + minY + " " + maxY);

        // determine moveSpeed for the dog
        moveSpeed = maxSpeed * (.5f + (stats.energy / 2));
        Debug.Assert(moveSpeed < maxSpeed);

        // give a random init move direction
        moveDir = Random.onUnitSphere;


    }

    private static moveDirection GetMoveDir(Vector3 vec)
    {
        vec.Normalize();
        if(Mathf.Abs(vec.x) > Mathf.Abs(vec.y))
        {
            // Left or right
            return (vec.x > 0) ? moveDirection.LEFT : moveDirection.RIGHT;
        }
        else
        {
            // Up or down
            return (vec.y > 0) ? moveDirection.UP : moveDirection.DOWN;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if(pos.x <= minX && moveDir.x < 0)
        {
            moveDir = Vector3.Reflect(moveDir, Vector3.right);
        }
        if (pos.x >= maxX && moveDir.x > 0)
        {
            moveDir = Vector3.Reflect(moveDir, Vector3.left);
        }
        if (pos.y <= minY && moveDir.y < 0)
        {
            moveDir = Vector3.Reflect(moveDir, Vector3.up);
        } 
        if (pos.y <= minY && moveDir.y > 0)
        {
            moveDir = Vector3.Reflect(moveDir, Vector3.down);
        }
    }

    // dog wander arround the screen
    public void Wander()
    {
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag.Equals("Wall"))
        {
            Debug.Log("was a wall");

            Debug.DrawRay(transform.position, moveDir.normalized);

            RaycastHit2D[] hits = new RaycastHit2D[2];
            
            if (0 != Physics2D.Raycast(transform.position, moveDir, new ContactFilter2D(), hits))
            {
                Debug.Log("Point of contact: " + hits[0].point);
            }
            else
            {
                Debug.Log("oof");
            }
            Vector3 normal = hits[0].normal;
            moveDir = Vector3.Reflect(moveDir, normal);
        }
    }
}
