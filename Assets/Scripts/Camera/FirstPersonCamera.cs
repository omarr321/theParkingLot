using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class is used to contorl a first person camera only
 * @Author Omar Radwan
 * @Version 1.0.1
 */
public class FirstPersonCamera : MonoBehaviour
{
    [Header("General")]
    public GameObject player;
    public float sensitivity = 1.0f;
    public bool invertY = false;

    // Update check for mouse input
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime, 0);
        if (invertY) {
            transform.Rotate(Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime, 0, 0);
        } else {
            transform.Rotate(-1 * Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime, 0, 0);
        }
        
        // This if/else is used so the player can not look all the way up or down
        // NOTE: If player moves very fast, this will break sometimes
        if (transform.eulerAngles.x < 300) {
            if (transform.eulerAngles.x > 30){
                transform.rotation = Quaternion.Euler(30, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            }
        } else {
            if (transform.eulerAngles.x < 330){
                transform.rotation = Quaternion.Euler(330, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            }
        }

        // This is so the player rotation matchs the camera
        player.transform.rotation = Quaternion.Euler(new Vector3(0, transform.localRotation.eulerAngles.y, 0));
        // This attachs the camera to the player and moves it with the player
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y+1, player.transform.position.z);
    }

    // LateUpdate reset the Z rotation so the camera can not turn up side down
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, 0));
    }
}
