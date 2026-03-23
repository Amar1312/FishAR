using System;
using UnityEngine;
using TMPro;

public class HomeSceneManager : MonoBehaviour
{
    public static HomeSceneManager Instance;

    public HomePanel _homePanelScript;
    public AllFishPanel _allFishScript;
    public FishDetailPanel _fishDetailScript;
    public UnlockPopupHomePanel _unlockScript;
    public MyCollectionPanel _myCollectionScript;
    public MyProfilePanel _profileScript;

    [Space]
    [Header("Daily Point")]
    public int _dailyPoint;
    public GameObject _dailyPopup;
    public GameObject _coinAnimationPanel;
    public TextMeshProUGUI _dailyMessage;
    public AudioClip _dailyClip;

    [Space]
    [Header("Spawn Data")]
    public FishSearch _fishSearch;
    public Transform _explorePanelContainer;
    public FishDetail _explorePanelPrefab;

    public Transform _collectionContainer;
    public CollectionFishComponent _collectionPanelPrefab;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DataContainerManager _dataManager = DataContainerManager.Instance;
        _fishSearch._fishComponent.Clear();
        _myCollectionScript._allFishComponent.Clear();
        for (int i = 0; i < _dataManager._fishData.Count; i++)
        {
            FishDetail fishdetail = Instantiate(_explorePanelPrefab, _explorePanelContainer);
            fishdetail._fishDetail = _dataManager._fishData[i].fishData;
            fishdetail._fishID = _dataManager._fishData[i].fishID;
            fishdetail._fishUnlockPoint = _dataManager._fishData[i]._fishUnlockPoint;
            _fishSearch._fishComponent.Add(fishdetail.gameObject);

            CollectionFishComponent fishcolle = Instantiate(_collectionPanelPrefab, _collectionContainer);
            fishcolle._fishDetail = _dataManager._fishData[i].fishData;
            fishcolle._fishId = _dataManager._fishData[i].fishID;
            fishcolle._fishUnlockPoint = _dataManager._fishData[i]._fishUnlockPoint;
            _myCollectionScript._allFishComponent.Add(fishcolle);
        }

        Invoke(nameof(CheckDailyCall), 2f);
    }

    public void FishDetailPanelOn()
    {
        _fishDetailScript.gameObject.SetActive(true);
        _allFishScript.gameObject.SetActive(false);
    }

    public void MyCollectionPanelOn()
    {
        _myCollectionScript.gameObject.SetActive(true);
        _fishDetailScript.gameObject.SetActive(false);
    }


    void CheckDailyCall()
    {
        string lastDate = PlayerPrefs.GetString("LastDailyCallDate");

        string todayDate = DateTime.Now.ToString("yyyyMMdd");

        if (lastDate != todayDate)
        {
            DailyMethod();

            PlayerPrefs.SetString("LastDailyCallDate", todayDate);
        }
    }

    void DailyMethod()
    {
        Debug.Log("Daily Method Called");
        _dailyMessage.text = "Congrats! " + _dailyPoint.ToString() + " Daily points added!";
        PointManager.Instance.AddPoint(_dailyPoint);
        _coinAnimationPanel.SetActive(true);
        _dailyPopup.SetActive(true);
        Invoke(nameof(OffDailyPopup), 5f);

        AudioHomeManager.Instance.PlayAudio(_dailyClip);
    }

    void OffDailyPopup()
    {
        _dailyPopup.SetActive(false);
        _coinAnimationPanel.SetActive(false);
    }

    private void OnDisable()
    {
        OffDailyPopup();
    }
}
