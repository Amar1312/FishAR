using UnityEngine;
using UnityEngine.UI;

public class ScrollItemVisibility : MonoBehaviour
{
    public RectTransform viewport;   // Assign ScrollView Viewport
    private Image targetImage;        // Child image to toggle

    RectTransform itemRect;

    //void Start()
    //{
    //    itemRect = GetComponent<RectTransform>();
    //    targetImage = GetComponent<Image>();
    //}
    void Awake()
    {
        itemRect = GetComponent<RectTransform>();
        targetImage = GetComponent<Image>();
    }

    //void LateUpdate()
    //{
    //    CheckVisibility();
    //}

    //void CheckVisibility()
    //{
    //    Vector3[] itemCorners = new Vector3[4];
    //    Vector3[] viewCorners = new Vector3[4];

    //    itemRect.GetWorldCorners(itemCorners);
    //    viewport.GetWorldCorners(viewCorners);

    //    // Check if item is inside vertical bounds
    //    bool isVisible =
    //        itemCorners[2].y > viewCorners[0].y &&  // Top > Bottom of viewport
    //        itemCorners[0].y < viewCorners[2].y;    // Bottom < Top of viewport

    //    // Enable / Disable Image
    //    targetImage.enabled = isVisible;
    //}

    public void CheckVisibility(RectTransform viewport)
    {
        Vector3[] itemCorners = new Vector3[4];
        Vector3[] viewCorners = new Vector3[4];

        itemRect.GetWorldCorners(itemCorners);
        viewport.GetWorldCorners(viewCorners);

        bool isVisible =
            itemCorners[2].y > viewCorners[0].y &&
            itemCorners[0].y < viewCorners[2].y;

        targetImage.enabled = isVisible;
    }
}