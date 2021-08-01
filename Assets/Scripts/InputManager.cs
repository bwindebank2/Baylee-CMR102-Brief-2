using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

using UnityEngine.XR.ARSubsystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera arCam;
    [SerializeField] private ARRaycastManager _raycastManager;

    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private Touch touch;
    private Pose pose;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        touch = Input.GetTouch(0);

        if (Input.touchCount <= 0 || touch.phase != TouchPhase.Began)
            return;

        if (IsPointerOverUI(touch)) return;

        Ray ray = arCam.ScreenPointToRay(touch.position);
        if (_raycastManager.Raycast(ray, _hits))
        {
            Pose pose = _hits[0].pose;
            Instantiate(DataHandler.Instance.GetFurniture(), pose.position, pose.rotation);
        }

        //Instantiate(DataHandler.Instance.GetFurniture(), pose.position, pose.rotation);

    }

    bool IsPointerOverUI(Touch touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.position.x, touch.position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    //if (Input.GetMouseButtonDown(0))
    //{
    //    Ray ray = arCam.ScreenPointToRay(Input.mousePosition);
    //    if (_raycastManager.Raycast(ray, _hits))
    //    {
    //        Pose pose = _hits[0].pose;
    //        Instantiate(DataHandler.Instance, pose.position, pose.rotation);
    //    }
    //}

}

