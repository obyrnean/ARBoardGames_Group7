// InstructionsUI_AR.cs
// Do NOT overwrite Ane's original InstructionsUI.cs — this is a separate file.
// Attach this to InstructionsCanvas instead of InstructionsUI.cs.

using UnityEngine;

public class InstructionsUI_AR : MonoBehaviour
{
    public GameObject instructionsCanvas;
    public GameObject gameplayCanvas; // assign GameplayCanvas here

    [Tooltip("Z position when instructions are visible (in front of chess board).")]
    public float frontZ = 1.5f;

    [Tooltip("Z position when instructions are hidden (behind chess board).")]
    public float backZ = 4.0f;

    public void OnOKPressed()
    {
        // Hide instructions, show gameplay buttons
        Vector3 pos = instructionsCanvas.transform.position;
        pos.z = backZ;
        instructionsCanvas.transform.position = pos;

        instructionsCanvas.SetActive(false);
        gameplayCanvas.SetActive(true);
    }

    public void ShowInstructions()
    {
        // Hide gameplay buttons, show instructions in front
        gameplayCanvas.SetActive(false);

        Vector3 pos = instructionsCanvas.transform.position;
        pos.z = frontZ;
        instructionsCanvas.transform.position = pos;

        instructionsCanvas.SetActive(true);
    }

    public void HideInstructions()
    {
        Vector3 pos = instructionsCanvas.transform.position;
        pos.z = backZ;
        instructionsCanvas.transform.position = pos;

        instructionsCanvas.SetActive(false);
        gameplayCanvas.SetActive(true);
    }
}