using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{

    public Animator anim;
    public enum moveDirection { UP, DOWN, LEFT, RIGHT }

    private Stats stats;
    
    [Tooltip("Max dog moveSpeed")]
    [SerializeField] private float maxSpeed = 1;
    private float moveSpeed;
    [SerializeField] public Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Stats>();
        anim = GetComponent<Animator>();

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
        if(pos.x <= GameStatusManager.minX && moveDir.x < 0)
        {
            moveDir = Vector3.Reflect(moveDir, Vector3.right);
        }
        if (pos.x >= GameStatusManager.maxX && moveDir.x > 0)
        {
            moveDir = Vector3.Reflect(moveDir, Vector3.left);
        }
        if (pos.y <= GameStatusManager.minY && moveDir.y < 0)
        {
            moveDir = Vector3.Reflect(moveDir, Vector3.up);
        } 
        if (pos.y >= GameStatusManager.maxY && moveDir.y > 0)
        {
            moveDir = Vector3.Reflect(moveDir, Vector3.down);
        }

        print(GetMoveDir(moveDir));
        if (GetMoveDir(moveDir) == moveDirection.UP)
        {
            anim.SetBool("isMovingDown", false);
            anim.SetBool("isMovingUp", true);
        }
        else if (GetMoveDir(moveDir) == moveDirection.DOWN)
        {
            anim.SetBool("isMovingUp", false);
            anim.SetBool("isMovingDown", true);
            
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
