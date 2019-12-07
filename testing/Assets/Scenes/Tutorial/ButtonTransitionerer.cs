using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTransitionerer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Color32 m_NormalColor = Color.white;
    public Color32 m_HoverColor = Color.gray;
    public Color32 m_DownColor = Color.white;

    public bool next = false;
    public bool continueButton = false;
    public bool done = false;
    public bool back = false;

    private Image m_Image = null;

    private void Awake()
    {
        m_Image = GetComponent<Image>();
    }

    public void OnPointerEnter (PointerEventData eventData)
    {
        m_Image.color = m_HoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_Image.color = m_NormalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_Image.color = m_DownColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_Image.color = m_HoverColor;

        if (this.gameObject.name == "Next Button")
            next = true;
        else if (this.gameObject.name == "Continue Button")
            continueButton = true;
        else if (this.gameObject.name == "Done Button ")
        {
            done = true;
        }
        else if (this.gameObject.name == "Back Button")
            back = true;
            
    }

    public bool nextT()
    {
        return next;
    }
    
    public bool continueT()
    {
        return continueButton;
    }
         
    public bool doneT()
    {
        return done;
    }

    public bool backT()
    {
        return back;
    }

}
