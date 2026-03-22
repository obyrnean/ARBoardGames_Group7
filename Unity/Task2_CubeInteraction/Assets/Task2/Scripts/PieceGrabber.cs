using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PieceGrabber : MonoBehaviour
{
    public float pinchThreshold = 0.02f;
    public float grabDistance = 0.05f;

    private bool isGrabbing = false;
    private GameObject grabbedObject;
    private Vector3 grabOffset;

    private ARCameraManager arCameraManager;

    void Start()
    {
        arCameraManager = FindFirstObjectByType<ARCameraManager>();
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch thumb = Input.GetTouch(0);
            Touch index = Input.GetTouch(1);

            float pinchDistance = Vector2.Distance(thumb.position, index.position);
            Vector2 midpoint = (thumb.position + index.position) / 2;

            Ray ray = Camera.main.ScreenPointToRay(midpoint);
            RaycastHit hit;

            bool isPinching = pinchDistance < pinchThreshold * Screen.dpi;

            if (isPinching && !isGrabbing)
            {
                if (Physics.Raycast(ray, out hit, grabDistance * 100))
                {
                    grabbedObject = hit.collider.gameObject;
                    grabOffset = grabbedObject.transform.position - ray.GetPoint(hit.distance);
                    isGrabbing = true;
                }
            }

            if (!isPinching)
            {
                isGrabbing = false;
                grabbedObject = null;
            }

            if (isGrabbing && grabbedObject != null)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    grabbedObject.transform.position = ray.GetPoint(hit.distance) + grabOffset;
                }
            }
        }
        else
        {
            isGrabbing = false;
            grabbedObject = null;
        }
    }
}