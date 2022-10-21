using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class controls a camera in a 3d space
 * @Author Omar Radwan
 * @Version 1.1.0
 */
public class CamMovementScript : MonoBehaviour
{
    [Header("Camera")]
    public float sensitivity = 1.0f;
    
    [Header("Movement")]
    [SerializeField]
    public float speed = 1.0f;
    public KeyCode forward = KeyCode.W;
    public KeyCode back = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode active = KeyCode.LeftShift;
    public bool alwaysActive = false;

    // Update check for mouse and camera inputs
    void Update()
    {
        if (Input.GetKey(active) || alwaysActive){
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime, 0);
            transform.Rotate(-1 * Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime, 0, 0);

            if (Input.GetKey(forward)) {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            if (Input.GetKey(back))
            {
                transform.Translate(Vector3.back * speed * Time.deltaTime);
            }
            if (Input.GetKey(left))
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            if (Input.GetKey(right))
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
    }
    
    // LateUpdate reset the Z rotation so the camera can not turn up side down
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, 0));
    }
}
