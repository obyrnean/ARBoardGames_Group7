using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PieceGrabber : MonoBehaviour
{
    public float grabDistance = 0.1f;

    private GameObject grabbedPiece = null;
    private Vector3 grabOffset;
    private Camera arCamera;

    void Start()
    {
        arCamera = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 midpoint = (touch0.position + touch1.position) / 2;
            float pinchDistance = Vector2.Distance(touch0.position, touch1.position);
            bool isPinching = pinchDistance < 100f;

            Ray ray = arCamera.ScreenPointToRay(midpoint);
            RaycastHit hit;

            if (isPinching && grabbedPiece == null)
            {
                if (Physics.Raycast(ray, out hit, grabDistance * 100))
                {
                    GameObject target = hit.collider.gameObject;
                    // Only grab pieces, not the board
                    if (target.name.Contains("King") || 
                        target.name.Contains("Queen") ||
                        target.name.Contains("Rook") ||
                        target.name.Contains("Bishop") ||
                        target.name.Contains("Knight") ||
                        target.name.Contains("Pawn"))
                    {
                        grabbedPiece = target;
                        grabOffset = grabbedPiece.transform.position - ray.GetPoint(hit.distance);
                    }
                }
            }

            if (!isPinching)
            {
                grabbedPiece = null;
            }

            if (grabbedPiece != null)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    grabbedPiece.transform.position = ray.GetPoint(hit.distance) + grabOffset;
                }
            }
        }
        else
        {
            grabbedPiece = null;
        }
    }
}