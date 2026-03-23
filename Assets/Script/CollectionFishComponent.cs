using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectionFishComponent : MonoBehaviour
{
    public FishAllDetail _fishDetail;
    public Image _fishImage;
    public int _fishId;
    public int _fishUnlockPoint;

    public TextMeshProUGUI _fishNameText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _fishNameText.text = _fishDetail.fishName;
        _fishImage.sprite = _fishDetail.fishImage;
    }

}
