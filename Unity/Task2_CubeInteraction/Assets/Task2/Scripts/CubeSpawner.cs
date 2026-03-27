using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CubeSpawner : MonoBehaviour
{
    public GameObject chessBoardPrefab;
    public GameObject[] chessPiecePrefabs;

    private ARRaycastManager raycastManager;
    private GameObject spawnedBoard;
    private List<GameObject> spawnedPieces = new List<GameObject>();
    private List<Vector3> defaultPositions = new List<Vector3>();
    private bool boardSpawned = false;

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (boardSpawned) return;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                spawnedBoard = Instantiate(chessBoardPrefab, hitPose.position, hitPose.rotation);
                boardSpawned = true;
            }
        }
    }

    public void ResetPieces()
    {
        for (int i = 0; i < spawnedPieces.Count; i++)
        {
            if (spawnedPieces[i] != null)
                spawnedPieces[i].transform.position = defaultPositions[i];
        }
    }
}