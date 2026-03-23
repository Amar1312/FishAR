using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class LevelCollectionBox : MonoBehaviour
{
    public Scrollbar _collectScrollbar;
    public TextMeshProUGUI _collectText, _winText;

    public CategoryType category;
    public int _categoryData;
    public int _totalFish;
    public int _complatePoint;
    public List<CollectionFishComponent> _collectfish;
    public CircleFishCollection _circleFishPrefab;
    public GameObject _lastCirclePrefab;
    public Transform _imageContainer;


    private void OnEnable()
    {
        Invoke(nameof(CheckCollectedFish), 0.5f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _winText.text = "+ " + _complatePoint.ToString() + " XP";
    }

    void CheckCollectedFish()
    {
        MyCollectionPanel _mycollection = HomeSceneManager.Instance._myCollectionScript;
        int categoryFish = 0;
        _collectfish.Clear();
        for (int i = 0; i < _mycollection._collectedFish.Count; i++)
        {
            if (_mycollection._collectedFish[i]._fishDetail.category == category)
            {
                categoryFish++;
                _collectfish.Add(_mycollection._collectedFish[i]);
            }
        }
        float Data = (float)categoryFish / _totalFish;
        _collectScrollbar.size = Data;
        _collectText.text = "Collection( " + categoryFish.ToString() + " / " + _totalFish.ToString() + " ) ";

        if (!PointManager.Instance._categoryComplete.Contains(_categoryData))
        {
            if (categoryFish == _totalFish)
            {
                PointManager.Instance.SetCategoryData(_categoryData);
                PointManager.Instance.AddPoint(_complatePoint);
            }
        }
        CircleSpawn(categoryFish);
    }

    public void CircleSpawn(int collectFish)
    {
        foreach (Transform item in _imageContainer)
        {
            Destroy(item.gameObject);
        }

        for (int i = 0; i < _collectfish.Count; i++)
        {
            CircleFishCollection circle = Instantiate(_circleFishPrefab, _imageContainer);
            circle._fishDetail = _collectfish[i]._fishDetail;
        }
        if (collectFish != _totalFish)
        {
            Instantiate(_lastCirclePrefab, _imageContainer);
        }
    }

}
