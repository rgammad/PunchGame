
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5;
    public float jumpPower = 5;
    private Vector3 movement;
    private bool isJumping = false;
    private Rigidbody rigid;

    private void Start()
    {
        isJumping = false;
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Jump"))
            isJumping = true;
        movement = new Vector3(horizontal, 0, vertical);
    }


    private void FixedUpdate()
    {
        transform.Translate(movement * Time.deltaTime * moveSpeed);
        if (isJumping)
        {
            rigid.AddForce(new Vector3(0, jumpPower, 0));
            isJumping = false;
        }
    }
}
