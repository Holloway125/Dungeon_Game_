using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{

    [SerializeField]
    private Camera _uiCamera;

    private TextMeshPro _tooltipText;
    private RectTransform _backgroundRectTransform;
    
    private void Awake()
    {
        _backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        _tooltipText = transform.Find("Text").GetComponent<TextMeshPro>();
        ShowToolTip("Hi Loser!");
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, _uiCamera, out localPoint);
        transform.localPosition = localPoint;
    }

    private void ShowToolTip(string tooltipString)
    {
        _tooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(_tooltipText.preferredWidth + textPaddingSize * 2f, _tooltipText.preferredHeight + textPaddingSize * 2f);
        _backgroundRectTransform.sizeDelta = backgroundSize;

        gameObject.SetActive(true);
    }

    private void HideToolTip()
    {
        gameObject.SetActive(false);
    }
}
