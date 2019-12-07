using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerUI : MonoBehaviour
{
    public float m_DefaultLength = 5.0f;
    public GameObject m_Dot;
    public VRInputModule m_InputModule;

    private LineRenderer m_LineRenderer = null;

    public void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();//Getting LineREnderer
    }

    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        //Use default/distance so the line render doesn't go through the panel/buttons
        PointerEventData data = m_InputModule.GetData();
        //if not hitting anything, set to default length, else use the distance given by data
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? m_DefaultLength : data.pointerCurrentRaycast.distance; 

        //Raycast
        RaycastHit hit = CreateRaycast(targetLength);

        //Default end-point
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        //If hitting something, check if there is a collider
        if (hit.collider != null)
        {
            endPosition = hit.point;
        }

        //Set position of the end-point dot
        m_Dot.transform.position = endPosition;

        //Set position of the line renderer
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast(float length) {

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward); //Ray created, will be childed to the controller.
        Physics.Raycast(ray, out hit, m_DefaultLength); //send out raycast

        return hit;
    }
}
