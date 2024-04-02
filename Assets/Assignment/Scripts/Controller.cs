using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Controller : MonoBehaviour
{
    public static Controller instance { get; private set; }

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
        foreach (Vector2 t in tileLocations)
        {
            Instantiate(tilePrefab, t, Quaternion.identity);
        }
        currentUI = noUI;
        funds = 200;
        health = 20;
        wave = 0;
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
            //Spawn enemies feature 2
            yield return new WaitForSeconds(1f - (wave/20f));   // Enemies spawn faster with each wave, up to twice as fast on wave 10
        }
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
        funds = value;
        while (funds < value)
        {
            temp++;
            money.text = temp.ToString();
            yield return null;
        }
        while (funds > value)
        {
            temp--;
            money.text = temp.ToString();
            yield return null;
        }
    }
}
