using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragandDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas playerUICanvas;
    private CanvasGroup invCanvasGroup;
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        invCanvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        invCanvasGroup.alpha = .6f;
        invCanvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / playerUICanvas.scaleFactor;
    }
        public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        invCanvasGroup.alpha = 1f;
        invCanvasGroup.blocksRaycasts = true;

    }    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPOinterDown");
    }
}
