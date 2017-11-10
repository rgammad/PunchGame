using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
public class MouseLook : MonoBehaviour
{
   

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public static PerspectiveSetting mLpersp = PerspectiveSetting.FirstPerson;

    public static float mLsensitivityY = 15f;
    public static float mLsensitivityX = 15f;
           
    public static float mLminimumX = -360f;
    public static float mLmaximumX = 360f;
    public static float mLminimumY = -360f;
    public static float mLmaximumY = 360f;

    [SerializeField]
    GameObject cameraHolder;

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
       
        _ChangeCameraPerspective();
        _MouseLook();

    }

    private void _MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        switch (axes)
        {
            case RotationAxes.MouseXAndY:
                rotationX += mouseX * mLsensitivityX * Time.deltaTime;
                rotationY += mouseY * mLsensitivityY * Time.deltaTime;
                rotationX = ClampAngle(rotationX, mLminimumX, mLmaximumX);
                rotationY = ClampAngle(rotationY, mLminimumY, mLmaximumY);
                Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
                transform.localRotation = originalRotation * xQuaternion * yQuaternion;
                break;
            case RotationAxes.MouseX:
                rotationX += mouseX * mLsensitivityX * Time.deltaTime;
                rotationX = ClampAngle(rotationX, mLminimumX, mLmaximumX);
                xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                transform.localRotation = originalRotation * xQuaternion;
                break;
            case RotationAxes.MouseY:
                rotationY += mouseY * mLsensitivityY * Time.deltaTime;
                rotationY = ClampAngle(rotationY, mLminimumY, mLmaximumY);
                yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
                transform.localRotation = originalRotation * yQuaternion;
                break;
            default:
                break;
        }
    }

    private void _ChangeCameraPerspective()
    {
        switch (mLpersp)
        {
            case PerspectiveSetting.FirstPerson:
                cameraHolder.transform.position = GameObject.Find("FirstPersonHolder").transform.position;
                cameraHolder.transform.rotation = GameObject.Find("ThirdPersonHolder").transform.rotation;
                break;
            case PerspectiveSetting.ThirdPerson:
                cameraHolder.transform.position = GameObject.Find("ThirdPersonHolder").transform.position;
                cameraHolder.transform.rotation = GameObject.Find("ThirdPersonHolder").transform.rotation;
                break;
            default:
                break;
        }
    }
}