using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This Class spawns car on the tile.
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class carSpawn : MonoBehaviour
{
    private int x;
    private int y;
    public GameObject[] carList;
    public static float spawnChance = 33.00f;

    // Attempts to gen a car in the parking slot.
    // NOTE: This gameObject destory itself when the car is done gernerating
    // @Parms int x : The x cord of the tile
    // @Parms int y : The y cord of the tile
    public void genCar(int x, int y) {
        this.x = x;
        this.y = y;

        if (Random.Range(0.0f,100.0f) <= spawnChance){
            GameObject carTemp = Instantiate(this.carList[Random.Range(0, this.carList.Length)]);
            carTemp.transform.localScale = new Vector3(0.70f, 0.70f, 0.70f);
            carTemp.transform.position = this.gameObject.transform.position;
            carTemp.transform.rotation = this.gameObject.transform.rotation;
            if (Random.Range(0,2) == 1) {
                carTemp.transform.Rotate(new Vector3(0, 180, 0));
            }
            carTemp.transform.Rotate(new Vector3(0, Random.Range(-15.0f, 15.0f), 0));
            carTemp.transform.name = "(" + x + "," + y + ")-" + this.gameObject.transform.name + "-Car";
            carTemp.transform.localScale += new Vector3(.75f, .75f, .75f);
        }
        Destroy(this.gameObject);
    }
}