using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipUI : MonoBehaviour
{

    // creating a singleton
    public static ToolTipUI Instance { get; private set; }

    [SerializeField] private RectTransform canvasRectTransform;

    private RectTransform rectTransform;
    private TextMeshProUGUI textMeshPro;
    // getting the background to resize
    private RectTransform backgroundRectTransform;
    private ToolTipTimer toolTipTimer;

    private void Awake()
    {
        Instance = this;
        // Setting Values from Objects in editor
        rectTransform = GetComponent<RectTransform>();
        textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();

        Hide();
    }

    private void Update()
    {
        FollowMouse();

        // hiding the tooltip after timer ends
        if (toolTipTimer != null)
        {
            toolTipTimer.timer -= Time.deltaTime;
            if (toolTipTimer.timer <= 0)
            {
                Hide();
            }
        }
    }

    private void FollowMouse()
    {
        // Here it works kinda odd due to the scaling of our Canvas to screen size
        // we are fixing by adding canvasRectTransform
        Vector2 anchorPosition = Input.mousePosition / canvasRectTransform.localScale.x; // screen space

        // if we hover to screen edges the tooltip dissapears
        // displaying tooltip on screen at all times below

        // Right check
        if (anchorPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchorPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }

        // Top check
        if (anchorPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchorPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchorPosition;
    }


    private void SetText(string toolTipText)
    {
        textMeshPro.SetText(toolTipText);

        // setting backgorund size, sometime this function 
        // does not get called properly so we need to force an update
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        // this goes right to the border
        // we are going to add some padding
        Vector2 padding = new Vector2(8, 8); // double the offset
        backgroundRectTransform.sizeDelta = textSize + padding;

    }

    public void Show(string toolTipText, ToolTipTimer toolTipTimer = null)
    {
        this.toolTipTimer = toolTipTimer;
        gameObject.SetActive(true);
        SetText(toolTipText);
        FollowMouse();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public class ToolTipTimer
    {
        public float timer;
    }
}


