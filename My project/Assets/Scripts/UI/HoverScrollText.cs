using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverScrollText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // I hate my life
    private RectTransform textRect;
    private TMP_Text textLabel;
    private RectTransform maskRect;
    private float scrollSpeed = 50f;
    private float pauseAtStart = 0.3f;

    private bool isHovering;
    private float scrollOffset;
    private float scrollDistance;
    private float pauseTimer;

    private void Awake()
    {
        textLabel = GetComponentInChildren<TMP_Text>();
        textRect = textLabel.rectTransform;
        maskRect = GetComponent<RectTransform>();
    }
    void Start()
    {
        ResetScroll();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        pauseTimer = pauseAtStart;
        ScrollDistance();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        ResetScroll();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHovering || scrollDistance <= 0f) return;
        if (pauseTimer > 0f)
        {
            pauseTimer -= Time.deltaTime;
            return;
        }
        scrollOffset += scrollSpeed * Time.deltaTime;
        float wrap = Mathf.Repeat(scrollOffset, scrollDistance);
        textRect.anchoredPosition = new Vector2(-wrap, textRect.anchoredPosition.y);
    }

    private void ScrollDistance()
    {
        float textWidth = textLabel.GetPreferredValues().x;
        float maskWidth = maskRect.rect.width;
        scrollDistance = Mathf.Max(0f, textWidth - maskWidth);
        scrollOffset = 0f;
    }
    private void ResetScroll()
    {
        scrollOffset = 0f;
        pauseTimer = 0f;
        if (textRect != null)
        {
            textRect.anchoredPosition = new Vector2(0f, textRect.anchoredPosition.y);
        }
    }


}
