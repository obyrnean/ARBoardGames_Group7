using UnityEngine;
using TMPro;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public TMP_Dropdown gameDropdown;

    public GameObject mainMenuPanel;
    public GameObject unavailablePanel;
    public GameObject instructionsPanel;

    [Header("Buttons")]
    public GameObject rulesButton;
    public GameObject exitButton;

    [Header("Game")]
    public GameObject chessBoardManager;

    [Header("Settings")]
    public float unavailableDisplayTime = 2f;

    void Start()
    {
        ResetToMainMenu();
    }

    // PLAY BUTTON
    public void OnPlayPressed()
    {
        string selectedGame = gameDropdown.options[gameDropdown.value].text;

        if (selectedGame == "Chess")
        {
            mainMenuPanel.SetActive(false);

            SpawnChessBoard();

            // Show instructions first
            instructionsPanel.SetActive(true);

            // Hide buttons until OK is pressed
            rulesButton.SetActive(false);
            exitButton.SetActive(false);
        }
        else
        {
            StartCoroutine(ShowUnavailable());
        }
    }

    // OK BUTTON (instructions)
    public void OnInstructionsOK()
    {
        instructionsPanel.SetActive(false);

        // Show buttons now
        rulesButton.SetActive(true);
        exitButton.SetActive(true);
    }

    // RULES BUTTON
    public void OnRulesPressed()
    {
        instructionsPanel.SetActive(true);
    }

    // EXIT BUTTON
    public void OnExitPressed()
    {
        ResetToMainMenu();
    }

    // SPAWN CHESS
    void SpawnChessBoard()
    {
        chessBoardManager.SetActive(true);

        Transform cam = Camera.main.transform;
        chessBoardManager.transform.position = cam.position + cam.forward * 1.5f;
    }


    // UNAVAILABLE FLOW
    IEnumerator ShowUnavailable()
    {
        mainMenuPanel.SetActive(false);
        unavailablePanel.SetActive(true);

        yield return new WaitForSeconds(unavailableDisplayTime);

        unavailablePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // RESET EVERYTHING
    void ResetToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        unavailablePanel.SetActive(false);
        instructionsPanel.SetActive(false);

        rulesButton.SetActive(false);
        exitButton.SetActive(false);

        chessBoardManager.SetActive(false);

        gameDropdown.value = 0;
    }
}
