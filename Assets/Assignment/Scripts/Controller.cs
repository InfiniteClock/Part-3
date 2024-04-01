using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public List<Vector2> path;
    public List<Vector2> tileLocations;
    public GameObject tilePrefab;
    public Button waveStartButton;
    public TMP_Text waveButtonText;
    public static Tile selectedTile;

    private int wave = 0;
    private void Start()
    {
        foreach (Vector2 t in tileLocations)
        {
            Instantiate(tilePrefab, t, Quaternion.identity);
        }
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
                    // Display UI for normal tower
                    break;
                case DefenseType.bomb:
                    // Display UI for bomb tower
                    break;
                case DefenseType.lightning:
                    // Display UI for lightning tower
                    break;
                case DefenseType.empty:
                    // Display UI for empty tile plot
                    break;
            }
        }
        else
        {
            // Display UI for nothing selected
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
        StartCoroutine(SpawnWave());
        waveStartButton.interactable = false;
        wave++;
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(5);
        waveStartButton.interactable = true;
    }
}
