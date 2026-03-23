using UnityEngine;
using UnityEngine.UI;

public class ScrollVisibilityManager : MonoBehaviour
{
    public RectTransform viewport;
    public RectTransform content;

    public ScrollItemVisibility[] items;

    void Start()
    {
        items = content.GetComponentsInChildren<ScrollItemVisibility>();
    }

    public void OnScroll()
    {
        foreach (var item in items)
        {
            item.CheckVisibility(viewport);
        }
    }
}