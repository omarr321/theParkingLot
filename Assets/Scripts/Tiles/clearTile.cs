using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This Class clears the cars off of tiles
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class clearTile : MonoBehaviour
{
    private int xCord;
    private int yCord;
    public int carNum;
    
    // Clears cars from the tile given
    // @Parms int x : The X cord of the tile
    // @Parms int y : The Y cord of the tile
    // @Return IEnumerator : Returns an IEnumerator that allows me to put this into a coroutine
    public IEnumerator clearCars(int x, int y)
    {
        this.xCord = x;
        this.yCord = y;

        Debug.Log("Clearing cars from (" + this.xCord + ", " + this.yCord + ")");

        for (int i = 0; i < carNum; i++) {
            GameObject temp = GameObject.Find("(" + xCord + "," + yCord + ")-" + i + "-Car");
            if (temp != null) {
                Debug.Log("Found Car: " + temp.transform.name);
                Destroy(temp);
            }
            // This is so that it will atlest 1 car per frame and wait until the next frame to clear another car
            yield return null;
        }
    }
}
