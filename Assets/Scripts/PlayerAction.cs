using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    float h, v;
    float speed = 3f;
    bool isHorizonMove;

    public GameManager manager;
    Vector3 dirVec;
    GameObject scanObject;

    Rigidbody2D rigid;
    Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Movement Value
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");

        // Check moving horizontally
        if (hDown || vUp) isHorizonMove = true;
        else if (vDown || hUp) isHorizonMove = false;

        // Direction
        if (vDown && v == 1) dirVec = Vector3.up;
        if (vDown && v == -1) dirVec = Vector3.down;
        if (hDown && h == 1) dirVec = Vector3.right;
        if (hDown && h == -1) dirVec = Vector3.left;

        // Animation
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
        {
            anim.SetBool("isChange", false);
        }

        // Scan Object
        if(Input.GetButtonDown("Jump") && scanObject != null)
        {
            manager.Action(scanObject);
        }
    }
    private void FixedUpdate()
    {
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.linearVelocity = moveVec * speed;

        // Ray
        Debug.DrawRay(rigid.position, dirVec*0.7f, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if(rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }
}
