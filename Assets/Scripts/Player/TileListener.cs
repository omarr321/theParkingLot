using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class is used to update the map as the player moves around
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class TileListener : MonoBehaviour
{
    public GameObject gameController;
    private int tileRadius;

    void Start()
    {
        this.tileRadius = gameController.GetComponent<WorldGenController>().tileRadius;
        //Repeats the calcTile method every 3 seconds
        InvokeRepeating("calcTile", 0, 3.0f);
    }

    // This will set the updated player tile position in the worldGen controller
    private void calcTile()
    {
        float playerX = Mathf.Round(transform.position.x  / tileRadius);
        float playerY = Mathf.Round(transform.position.z  / tileRadius);
        gameController.GetComponent<WorldGenController>().changeCord((int) playerX, (int) playerY);
    }
}