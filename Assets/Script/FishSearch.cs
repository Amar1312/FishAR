using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class FishSearch : MonoBehaviour
{
    public Button _allSpeciesBtn, _koiFish, _fGoldFish,_goldFish,_normalFish, _likeBtn;
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
        _koiFish.onClick.AddListener(koiFshClick);
        _fGoldFish.onClick.AddListener(FGoldFishClick);
        _goldFish.onClick.AddListener(GoldFishClick);
        _normalFish.onClick.AddListener(NormalFish);
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

    void koiFshClick()
    {
        OnTagFish("KoiFish");
        OnSelectImage(1);
    }

    void FGoldFishClick()
    {
        OnTagFish("FGoldFish");
        OnSelectImage(2);
    }

    void GoldFishClick()
    {
        OnTagFish("GoldFish");
        OnSelectImage(3);
    }
    void NormalFish()
    {
        OnTagFish("NormalFish");
        OnSelectImage(4);
    }


    void OnAllFish()
    {
        for (int i = 0; i < _fishComponent.Count; i++)
        {
            _fishComponent[i].SetActive(true);
        }
    }

    void OnTagFish(string tag)
    {
        for (int i = 0; i < _fishComponent.Count; i++)
        {
            if (_fishComponent[i].TryGetComponent<FishDetail>(out FishDetail detail))
            {
                CategoryType type = detail._fishDetail.category;
                string typeString = type.ToString();
                if (typeString == tag)
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
        OnSelectImage(5);
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
