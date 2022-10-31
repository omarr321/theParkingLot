using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class controls the player movenent
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    public Rigidbody rb;
    public float speed = 1.0f;
    public KeyCode forward = KeyCode.W;
    public KeyCode back = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    private SettingManager settingMan;
    void Start()
    {
        settingMan = GameObject.Find("SettingPersonal").GetComponent<SettingManager>();
        updateControls();
    }

    // Update checks for key input
    void Update()
    {
        if (Input.GetKey(forward)) {
            rb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(back))
        {
            rb.AddRelativeForce(Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetKey(left))
        {
           rb.AddRelativeForce(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(right))
        {
            rb.AddRelativeForce(Vector3.right * speed * Time.deltaTime);
        }
    }

    public void updateControls() 
    {
        forward = settingMan.getForward();
        back = settingMan.getBackward();
        left = settingMan.getLeft();
        right = settingMan.getRight();
    }
}
