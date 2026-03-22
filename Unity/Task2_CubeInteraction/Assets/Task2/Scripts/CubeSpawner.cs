using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    private ARRaycastManager raycastManager;
    private GameObject spawnedCube;
    private Vector3 defaultSpawnPosition;
    private bool cubeSpawned = false;

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (cubeSpawned) return;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                spawnedCube = Instantiate(cubePrefab, hitPose.position, hitPose.rotation);
                defaultSpawnPosition = hitPose.position;
                cubeSpawned = true;
            }
        }
    }

    public void ResetCube()
    {
        if (spawnedCube != null)
        {
            spawnedCube.transform.position = defaultSpawnPosition;
            spawnedCube.transform.rotation = Quaternion.identity;
        }
    }
}
