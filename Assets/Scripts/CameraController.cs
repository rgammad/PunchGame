using UnityEngine;


public class CameraController : MonoBehaviour
{

    public GameObject target;
    public float rotateSpeed = 5;
    private Vector3 offset;

    /*TODO: control camera's vertical rotation
     *      limit vertical rotation?
     *      
    */

    private void Start()
    {
        offset = target.transform.position - transform.position;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.transform.Rotate(0, horizontal, 0);

        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position - (rotation * offset);
        transform.LookAt(target.transform);
    }

}

