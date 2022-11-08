using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData) => EventSystem.current.SetSelectedGameObject(gameObject);
    public void OnPointerExit(PointerEventData eventData)
    {
        if(EventSystem.current.currentSelectedGameObject == gameObject) 
            EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnPointerClick(PointerEventData eventData) => EventSystem.current.SetSelectedGameObject(gameObject);
}