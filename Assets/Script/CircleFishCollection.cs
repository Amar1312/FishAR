using UnityEngine;
using UnityEngine.UI;

public class CircleFishCollection : MonoBehaviour
{
    public FishAllDetail _fishDetail;

    public Image _fishImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(SetImage), 0.2f);
    }

    void SetImage()
    {
        _fishImage.sprite = _fishDetail.fishImage;
    }
   
}
