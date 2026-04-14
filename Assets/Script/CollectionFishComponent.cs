using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CollectionFishComponent : MonoBehaviour
{
    public FishAllDetail _fishDetail;
    public Image _fishImage;
    public int _fishId;
    public int _fishUnlockPoint;
    public List<GameObject> temp = new List<GameObject>();

    public TextMeshProUGUI _fishNameText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _fishNameText.text = _fishDetail.fishName;
        _fishImage.sprite = _fishDetail.fishImage;
        for (int i = 0; i < temp.Count; i++)
        {
            Debug.Log(temp[i].gameObject.name);
            if (_fishDetail.availability == temp[i].gameObject.name)
            {
                Debug.Log("tep on" + temp[i].gameObject.name);
                temp[i].gameObject.SetActive(true);
            }
            else
            {
                temp[i].gameObject.SetActive(false);
            }
        }
    }

}
