
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed = 5;
    private Vector3 movement;
    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement = new Vector3(horizontal, 0, vertical);
    }
    private void FixedUpdate()
    {
        transform.Translate(movement * Time.deltaTime * moveSpeed);
    }
}
