using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class controls the world generation
 * @Author Omar Radwan
 * @Version 1.0.0
 */
public class WorldGenController : MonoBehaviour
{
    [Header("General")]
    public GameObject[] tiles;
    public int tilesSpawnedRadius = 5;
    public int tileRadius = 10;
    public PlayerControlLock playerLock;
    public LoadingScreen loadingScreenSc;

    private int centerTileX = 0;
    private int centerTileY = 0;
    private int seed;


    private int m_frameCounter = 0;
    private float m_timeCounter = 0.0f;
    private float m_lastFramerate = 0.0f;
    public float m_refreshTime = 0.25f;
    public int targetFrameRate = 144;
    public TMPro.TextMeshProUGUI FPS_Counter;

    public GameObject loadingScreen;
    public PlayerManager playerMan;
    private SettingManager settingMan;
    public AutoSavingScript autoSave;

    private WorldManager worldMan;

    // Locks the player and set defualt values as well as the seed.
    void Start()
    {
        worldMan = GameObject.Find("LoadSetting").GetComponent<WorldManager>();
        settingMan = GameObject.Find("SettingPersonal").GetComponent<SettingManager>();

        loadingScreen.SetActive(true);
        playerLock.lockPlayer(this);
        playerLock.disableCam(this);
        playerLock.disableMovement(this);
        playerMan.setEnabledHealth(false);
        playerMan.setEnabledHydro(false);
        playerMan.setEnabledSat(false);
        seed = worldMan.getSeed();
        Debug.Log("Seed: " + seed.ToString());
        this.changeCord(worldMan.getCurrX(), worldMan.getCurrY());
        StartCoroutine(initStart());
    }

    // Updates the FPS
    void Update()
    {
        if (m_timeCounter < m_refreshTime) {
            m_timeCounter += Time.deltaTime;
            m_frameCounter++;
        } else {
            //This code will break if you set your m_refreshTime to 0, which makes no sense.
            m_lastFramerate = (float)m_frameCounter/m_timeCounter;
            m_frameCounter = 0;
            m_timeCounter = 0.0f;
        }
        FPS_Counter.text = "FPS: " + Mathf.RoundToInt(m_lastFramerate);
    }

    // Init the map for loading.
    // NOTE: This method also reenables the player and lets the player play
    public IEnumerator initStart()
    {
        yield return StartCoroutine(updateMap());
        yield return new WaitForSeconds(1);
        playerLock.enableCam(this);
        playerLock.enableMovement(this);
        playerLock.unlockPlayer(this);
        playerMan.setEnabledHealth(true);
        playerMan.setEnabledHydro(true);
        playerMan.setEnabledSat(true);
        playerMan.addHealth(100);
        playerMan.addHydration(100);
        playerMan.addSaturation(100);
        autoSave.setSaveInterval(settingMan.getAutoSave());
        autoSave.startSaveLoop();
        loadingScreen.SetActive(false);
    }

    // Changes the current player cord
    // @Parms int x : The tile x cord
    // @Parms int y : The tile y cord
    public void changeCord(int x, int y)
    {
        //if the cord has not changed, it does not update the map
        if (this.centerTileX == x && this.centerTileY == y)
        {
            return;
        }
        this.centerTileX = x;
        this.centerTileY = y;
        StartCoroutine(updateMap());
    }

    // Updates the map so new tiles are spwaned and old ones are culled
    private IEnumerator updateMap()
    {
        //Gets the start cord and end cord of tiles that needs to be spawned
        int startCord = (int) Mathf.Floor(tilesSpawnedRadius/2) * tileRadius;
        int xCordStart = centerTileX * tileRadius - startCord;
        int yCordStart = centerTileY * tileRadius - startCord;
        int xCordEnd = xCordStart + tilesSpawnedRadius * tileRadius;
        int yCordEnd = yCordStart + tilesSpawnedRadius * tileRadius;
        
        // Loops thought each tile
        for (int x = xCordStart; x < xCordEnd; x = x + tileRadius) {
            for (int y = yCordStart; y < yCordEnd; y = y + tileRadius) {
                // If the tile exist, it does not spawn it in
                if (GameObject.Find("(" + x/tileRadius + "," + y/tileRadius + ")") == null) {
                    // Compute the Hash  of the tile and seeds the Random Generator
                    int[] hashIn = new int[] {seed, x, y};
                    var hash = Hash128.Compute(hashIn).ToString();
                    Random.InitState(hash.GetHashCode());
                    int tile = Random.Range(0, tiles.Length);
                    GameObject temp = Instantiate(tiles[tile], new Vector3(x, 0, y), Quaternion.identity);
                    Quaternion currRotate = new Quaternion();
                    currRotate.eulerAngles = new Vector3(0, Random.Range(0,4)*90, 0);
                    temp.transform.rotation = currRotate;
                    temp.name = "(" + x/tileRadius + "," + y/tileRadius + ")";   
                    temp.transform.Find("Parking Spaces");
                    carSpawn[] _cars = temp.transform.Find("Parking Spaces").GetComponentsInChildren<carSpawn>();
                    
                    // Attemps to gen car in parking spaces
                    foreach(carSpawn car in _cars){
                        yield return null;
                        car.genCar(x/tileRadius, y/tileRadius);
                    }
                }
                if (loadingScreen.activeInHierarchy) {
                    loadingScreenSc.incurStep();
                }
            }
        }
        StartCoroutine(cullTiles());
    }

    // Goes thught all the tiles in the world and cull ones out of range
    private IEnumerator cullTiles()
    {
        char[] delimiterChars = {',','(',')'};
        int currX = this.centerTileX;
        int currY = this.centerTileY;
        GameObject[] currentTiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach(GameObject tile in currentTiles) {
            // Checks at least one tile per frame before release control back to Unity
            yield return null;
            string name = tile.name;
            string[] temp = name.Split(delimiterChars);
            int tempX = int.Parse(temp[1]);
            int tempY = int.Parse(temp[2]);
            // If the tile falls out side a 7x7 grid from the player, it gets destoryed
            if (Mathf.Abs(tempX - currX) > tilesSpawnedRadius/2 + 2 || Mathf.Abs(tempY - currY) > tilesSpawnedRadius/2 + 2) {
                StartCoroutine(tile.GetComponent<clearTile>().clearCars(tempX, tempY));
                Destroy(tile);
            }
        }
    }
}
