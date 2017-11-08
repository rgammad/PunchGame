
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5;
    public float jumpHeight = 5;


    private Vector3 movement;
    private bool isJumping = false;
    private bool canJump = false;
    private Rigidbody rigid;

    private void Start()
    {
        isJumping = false;
        rigid = GetComponentInChildren<Rigidbody>();
        rigid.freezeRotation = true;
    }

    private void Update()
    {
        //Debug.DrawRay(transform.position, Vector3.down * 1.05f);
        _pgJumpInput();
        _pgMoveInput();
    }

    private void FixedUpdate()
    {
        _pgMoveHandler();
        _pgJumpHandler();
    }

    //Input functions
    private void _pgMoveInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement = new Vector3(horizontal, 0, vertical);
    }
    private void _pgJumpInput()
    {
        if (Input.GetButtonDown("Jump") && _CheckCanJump())
            isJumping = true;
    }



    //Input handler functions
    private void _pgMoveHandler()
    {
        transform.Translate(movement * Time.deltaTime * moveSpeed);
    }
    private void _pgJumpHandler()
    {
        if (isJumping)
        {
            rigid.velocity = new Vector3(0, jumpHeight, 0);
            isJumping = false;
        }
    }


    //helper functions

    private bool _CheckCanJump()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.05f) && hit.collider.CompareTag("ground"))
            return true;
        return false;
    }
}
