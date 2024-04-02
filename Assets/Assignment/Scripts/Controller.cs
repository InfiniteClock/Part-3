using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Controller : MonoBehaviour
{
    public static Controller instance { get; private set; }

    public GameObject normalEnemy;
    public GameObject fastEnemy;
    public GameObject slowEnemy;

    public List<Vector2> path;                      // Holds the path for the enemies to follow
    public List<Vector2> tileLocations;             // Holds the locations of every defense tile
    public GameObject emptyUI;                      // Holds the UI for an empty tile
    public GameObject normalUI;                     // Holds the UI for a normal tower
    public GameObject bombUI;                       // Holds the UI for a bomb tower
    public GameObject lightningUI;                  // Holds the UI for a lightning tower
    public GameObject noUI;                         // Holds the UI for no selection
    private GameObject currentUI;                   // Holds the currently selected UI

    public TMP_Text normalStats;                    //These can be implemented in feature 3
    public TMP_Text bombStats;                      //These can be implemented in feature 3
    public TMP_Text lightningStats;                 //These can be implemented in feature 3
    public TMP_Text normalUpgrade;                  //These can be implemented in feature 3
    public TMP_Text bombUpgrade;                    //These can be implemented in feature 3
    public TMP_Text lightningUpgrade;               //These can be implemented in feature 3
    public TMP_Text hp;                             // Text value of current health
    public TMP_Text money;                          // Text value of current money

    public GameObject tilePrefab;                   // The prefab for the Tile object that can hold defenses
    public Button waveStartButton;                  // The button for starting a wave
    public TMP_Text waveButtonText;                 // The text of the wave start button
    public static Tile selectedTile;                // The currently selected tile

    private Coroutine adjustingFunds;
    public static int wave { get; private set; }    // The current wave
    public static int health { get; private set; }  // The current health
    public static int funds { get; private set; }   // The current money
    private void Start()
    {
        instance = this;
        foreach (Vector2 t in tileLocations)
        {
            Instantiate(tilePrefab, t, Quaternion.identity);
        }
        currentUI = noUI;
        funds = 0;
        health = 100;
        wave = 0;
        AdjustMoney(200);
        LoseHP(0);
    }
    private void Update()
    {
        // Temporary measure for unselecting tiles
        if (Input.GetMouseButtonDown(1) && selectedTile != null)
        {
            selectedTile.SelectThis(false);
            selectedTile = null;
        }

        if (selectedTile != null)
        {
            switch (selectedTile.defense) 
            {
                case DefenseType.normal:
                    SetUI(normalUI);
                    break;
                case DefenseType.bomb:
                    SetUI(bombUI);
                    break;
                case DefenseType.lightning:
                    SetUI(lightningUI);
                    break;
                case DefenseType.empty:
                    SetUI(emptyUI);
                    break;
            }
        }
        else
        {
            SetUI(noUI);
        }

        if (waveStartButton.interactable == true)
        {
            waveButtonText.text = "Start Next Wave";
        }
        else
        {
            waveButtonText.text = ("Wave: " + wave);
        }
    }

    public static void SetSelectedTile(Tile tile)
    {
        if (selectedTile != null)
        {
            selectedTile.SelectThis(false);
        }
        selectedTile = tile;
        selectedTile.SelectThis(true);
    }
    public void OnWaveStart()
    {
        wave++;
        StartCoroutine(SpawnWave());
        waveStartButton.interactable = false;
    }

    IEnumerator SpawnWave()
    {
        StartCoroutine(SpawnEnemies());
        yield return new WaitForSeconds(5+wave);
        waveStartButton.interactable = true;
    }
    IEnumerator SpawnEnemies()
    {
        int numbEnemies = 0;
        while (numbEnemies < wave * 10)
        {
            switch (wave)
            {
                case 1:
                    CreateEnemy(normalEnemy);
                    break;
                case 2:
                    CreateEnemy(fastEnemy);
                    break;
                case 3:
                    if (numbEnemies % 5 == 1)
                    {
                        CreateEnemy(slowEnemy);
                    }
                    else
                    {
                        CreateEnemy(normalEnemy);
                    }
                    break;
                case 4:
                    if (numbEnemies % 3 == 1)
                    {
                        CreateEnemy(fastEnemy);
                    }
                    else
                    {
                        CreateEnemy(normalEnemy);
                    }
                    break;
                case 5:
                    if (numbEnemies % 5 == 1)
                    {
                        CreateEnemy(slowEnemy);
                    }
                    else if (numbEnemies % 3 == 1)
                    {
                        CreateEnemy(fastEnemy);
                    }
                    else
                    {
                        CreateEnemy(normalEnemy);
                    }
                    break;
                case 6:
                    if (numbEnemies % 5 == 1)
                    {
                        CreateEnemy(slowEnemy);
                        CreateEnemy(fastEnemy);
                    }
                    else
                    {
                        CreateEnemy(fastEnemy);
                    }
                    break;
                case 7:
                    if (numbEnemies % 7 == 1)
                    {
                        CreateEnemy(slowEnemy);
                        CreateEnemy(fastEnemy);
                        CreateEnemy(normalEnemy);
                    }
                    else if (numbEnemies % 4 == 1)
                    {
                        CreateEnemy(fastEnemy);
                        CreateEnemy(normalEnemy);
                    }
                    else
                    {
                        CreateEnemy(normalEnemy);
                    }
                    break;
                case 8:
                    CreateEnemy(fastEnemy);
                    CreateEnemy(normalEnemy);
                    break;
                case 9:
                    CreateEnemy(slowEnemy);
                    yield return null;
                    CreateEnemy(fastEnemy);
                    break;
                case 10:
                    if (numbEnemies % 10 == 1)
                    {
                        CreateEnemy(slowEnemy);
                        yield return null;
                        CreateEnemy(slowEnemy);
                        yield return null;
                        CreateEnemy(slowEnemy);
                    }
                    if (numbEnemies % 5 == 1)
                    {
                        CreateEnemy(fastEnemy);
                        yield return null;
                        CreateEnemy(normalEnemy);
                    }
                    CreateEnemy(fastEnemy);
                    break;
                default:
                    int rand = Random.Range(1, 3);
                    if (rand == 1)
                    {
                        CreateEnemy(normalEnemy);
                    }
                    if (rand == 2)
                    {
                        CreateEnemy(fastEnemy);
                    }
                    if (rand == 3)
                    {
                        CreateEnemy(slowEnemy);
                    }
                    break;

            }
            numbEnemies++;
            yield return new WaitForSeconds(1f - (wave/20f));   // Enemies spawn faster with each wave, up to twice as fast on wave 10, and even faster after that breaks on wave 21
        }
    }
    private void CreateEnemy(GameObject enemy)
    {
        GameObject temp = Instantiate(enemy, path[0], Quaternion.identity);
        //temp.GetComponent<Enemy>().path = path;
    }
    private void SetUI(GameObject UI)
    {
        if (currentUI != UI)
        {
            currentUI.SetActive(false);
            currentUI = UI;
            currentUI.SetActive(true);
        }
    }

    public void OnPurchaseTower(DefenseType type)
    {
        // Feature 3, call build function through selectedTile, decrease money
    }
    public void OnUpgradeTower()
    {
        // Feature 3, call upgrade function through selectedTile, decrease money
    }

    public static void LoseHP(int value)
    {
        health -= value;
        instance.hp.text = health.ToString();
        if (health <= 0)
        {
            // game over
        }
    }
    public static bool AdjustMoney(int value)
    {
        if (funds + value < 0)
        {
            // Feedback coroutine to make money flash red?
            return false;
        }
        else
        {
            if (instance.adjustingFunds != null) 
            {

                instance.StopCoroutine(instance.adjustingFunds);
            }
            instance.adjustingFunds = instance.StartCoroutine(instance.AlterFunds(value));
            return true;
        }
        
    }
    IEnumerator AlterFunds(int value)
    {
        int temp = funds;
        funds += value;
        while (temp < funds)
        {
            temp++;
            money.text = temp.ToString();
            yield return null;
        }
        while (temp > funds)
        {
            temp--;
            money.text = temp.ToString();
            yield return null;
        }
    }
}
