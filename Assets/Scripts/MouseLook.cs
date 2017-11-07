using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0, MouseX = 1, MouseY = 2
    }
    public enum PerspectiveSetting
    {
        FirstPerson = 0, ThirdPerson = 1
    }

    /* Put script on both player and player camera
     * select MouseXAndY to control rotation w/ mouse x & y input
     * select MouseY to control rotation w/ mouse y input only
     * select MouseX to control rotation w/ mouse x input only
     */

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public PerspectiveSetting persp = PerspectiveSetting.FirstPerson;
    public float sensitivityX = 15f;
    public float sensitivityY = 15f;

    public float minimumX = -360f;
    public float maximumX = 360f;
    public float minimumY = -360f;
    public float maximumY = 360f;

    [SerializeField]
    static GameObject cameraHolder;

    private float rotationX = 0f;
    private float rotationY = 0f;
    private Quaternion originalRotation;

    void Start()
    {
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
        originalRotation = transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraHolder = GameObject.Find("CameraHolder");
        switch (persp)
        {
            case PerspectiveSetting.FirstPerson:
                cameraHolder.transform.localPosition = new Vector3(0, 0.95f, 0);
                cameraHolder.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case PerspectiveSetting.ThirdPerson:
                cameraHolder.transform.localPosition = new Vector3(0, 3, -3);
                cameraHolder.transform.rotation = Quaternion.Euler(20, 0, 0);
                break;
            default:
                break;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        switch (axes)
        {
            case RotationAxes.MouseXAndY:
                rotationX += mouseX * sensitivityX * Time.deltaTime;
                rotationY += mouseY * sensitivityY * Time.deltaTime;
                rotationX = ClampAngle(rotationX, minimumX, maximumX);
                rotationY = ClampAngle(rotationY, minimumY, maximumY);
                Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
                transform.localRotation = originalRotation * xQuaternion * yQuaternion;
                break;
            case RotationAxes.MouseX:
                rotationX += mouseX * sensitivityX * Time.deltaTime;
                rotationX = ClampAngle(rotationX, minimumX, maximumX);
                xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                transform.localRotation = originalRotation * xQuaternion;
                break;
            case RotationAxes.MouseY:
                rotationY += mouseY * sensitivityY * Time.deltaTime;
                rotationY = ClampAngle(rotationY, minimumY, maximumY);
                yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
                transform.localRotation = originalRotation * yQuaternion;
                break;
            default:
                break;
        }
    }
}