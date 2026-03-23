using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class FishSearch : MonoBehaviour
{
    public Button _allSpeciesBtn, _saltwaterBtn, _freshwaterBtn, _likeBtn;
    public List<GameObject> _selectBtnImage;
    public List<GameObject> _fishComponent;
    public TMP_InputField _searchInputField;
    private List<string> namesList = new List<string>();

    private void OnEnable()
    {
        _searchInputField.text = "";
        AllSpeciesBtnClick();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _allSpeciesBtn.onClick.AddListener(AllSpeciesBtnClick);
        _saltwaterBtn.onClick.AddListener(SaltwaterBtnClick);
        _freshwaterBtn.onClick.AddListener(FreshwaterBtnClick);
        _searchInputField.onValueChanged.AddListener(OnSearchValueChanged);
        _likeBtn.onClick.AddListener(LikeBtnClick);

        for (int i = 0; i < _fishComponent.Count; i++)
        {
            if(_fishComponent[i].TryGetComponent<FishDetail>(out FishDetail _detail))
            {
                namesList.Add(_detail._fishDetail.fishName);
            }
        }
    }

    void AllSpeciesBtnClick()
    {
        OnAllFish();
        OnSelectImage(0);
    }

    void SaltwaterBtnClick()
    {
        OnTagFish(tagType.Saltwater);
        OnSelectImage(1);
    }

    void FreshwaterBtnClick()
    {
        OnTagFish(tagType.Freshwater);
        OnSelectImage(2);
    }

    void OnAllFish()
    {
        for (int i = 0; i < _fishComponent.Count; i++)
        {
            _fishComponent[i].SetActive(true);
        }
    }

    void OnTagFish(tagType tag)
    {
        for (int i = 0; i < _fishComponent.Count; i++)
        {
            if (_fishComponent[i].TryGetComponent<FishDetail>(out FishDetail detail))
            {
                tagType type = detail._fishDetail.tag;
                if (type == tag)
                {
                    _fishComponent[i].SetActive(true);
                }
                else
                {
                    _fishComponent[i].SetActive(false);
                }
            }
        }
    }

    void LikeBtnClick()
    {
        OnSelectImage(3);
        for (int i = 0; i < _fishComponent.Count; i++)
        {
            if (_fishComponent[i].TryGetComponent<FishDetail>(out FishDetail detail))
            {
                bool like = detail._fishDetail.fevarit;
                if (like)
                {
                    _fishComponent[i].SetActive(true);
                }
                else
                {
                    _fishComponent[i].SetActive(false);
                }
            }
        }
    }

    void OnSelectImage(int Index)
    {
        for (int i = 0; i < _selectBtnImage.Count; i++)
        {
            _selectBtnImage[i].SetActive(false);
        }
        _selectBtnImage[Index].SetActive(true);
    }

    void OnSearchValueChanged(string text)
    {
        text = text.ToLower();

        for (int i = 0; i < namesList.Count; i++)
        {
            if (namesList[i].ToLower().Contains(text))
            {
                _fishComponent[i].SetActive(true);
            }
            else
            {
                _fishComponent[i].SetActive(false);
            }
        }
    }
}
