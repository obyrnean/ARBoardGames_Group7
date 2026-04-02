// ARMenuManager.cs
// Adapted from MenuManager.cs (Ane's original) for Meta Quest VR headset.
// Do NOT rename or overwrite MenuManager.cs — this is a separate file.

using UnityEngine;
using TMPro;
using System.Collections;

public class ARMenuManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Dropdown gameDropdown;
    public GameObject mainMenuCanvas;
    public GameObject unavailableCanvas;
    public GameObject instructionsCanvas;
    public GameObject inGameCanvas;

    [Header("Game")]
    public GameObject chessBoardManager;

    [Header("Settings")]
    public float unavailableDisplayTime = 2f;

    [Header("VR Spawn Settings")]
    [Tooltip("How far in front of the user the chessboard spawns (metres).")]
    public float spawnDistance = 3f;

    [Header("Instructions Z Position")]
    [Tooltip("Z position for instructions canvas when visible (in front of chess board).")]
    public float instructionsFrontZ = 1.0f;

    void Start()
    {
        mainMenuCanvas.SetActive(true);
        unavailableCanvas.SetActive(false);
        instructionsCanvas.SetActive(false);
        inGameCanvas.SetActive(false);
        chessBoardManager.SetActive(false);
    }

    // ── PLAY BUTTON ──────────────────────────────────────────────────────────
    public void OnPlayPressed()
    {
        string selectedGame = gameDropdown.options[gameDropdown.value].text;

        if (selectedGame == "Chess")
        {
            mainMenuCanvas.SetActive(false);
            SpawnChessBoard();

            // Bring instructions canvas in front of the chess board before showing it.
            Vector3 pos = instructionsCanvas.transform.position;
            pos.z = instructionsFrontZ;
            instructionsCanvas.transform.position = pos;

            instructionsCanvas.SetActive(true);
            inGameCanvas.SetActive(true);
        }
        else
        {
            StartCoroutine(ShowUnavailable());
        }
    }

    // ── SPAWN CHESS BOARD ─────────────────────────────────────────────────────
    void SpawnChessBoard()
    {
        chessBoardManager.SetActive(true);

        Transform viewpoint = GetVRViewpoint();

        if (viewpoint != null)
        {
            chessBoardManager.transform.position = viewpoint.position
                + viewpoint.forward * spawnDistance;

            // Keep the board upright — only rotate around Y so it faces the user.
            Vector3 flatForward = new Vector3(viewpoint.forward.x, 0f, viewpoint.forward.z).normalized;
            if (flatForward != Vector3.zero)
                chessBoardManager.transform.rotation = Quaternion.LookRotation(flatForward);
        }
    }

    // ── VR VIEWPOINT HELPER ───────────────────────────────────────────────────
    Transform GetVRViewpoint()
    {
        // Unity 6 compatible — FindObjectOfType is obsolete, use FindAnyObjectByType.
        OVRCameraRig ovrRig = FindAnyObjectByType<OVRCameraRig>();
        if (ovrRig != null && ovrRig.centerEyeAnchor != null)
            return ovrRig.centerEyeAnchor;

        // Fallback: standard Camera.main (works in editor play mode).
        if (Camera.main != null)
            return Camera.main.transform;

        return null;
    }

    // ── UNAVAILABLE MESSAGE ───────────────────────────────────────────────────
    IEnumerator ShowUnavailable()
    {
        unavailableCanvas.SetActive(true);
        yield return new WaitForSeconds(unavailableDisplayTime);
        unavailableCanvas.SetActive(false);
    }

    // ── EXIT TO MENU ──────────────────────────────────────────────────────────
    public void OnExitToMenu()
    {
        instructionsCanvas.SetActive(false);
        inGameCanvas.SetActive(false);
        chessBoardManager.SetActive(false);
        mainMenuCanvas.SetActive(true);
        gameDropdown.value = 0;
    }
}