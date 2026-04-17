using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class FishDetailPanel : MonoBehaviour
{
    public Button _backBtn;
    public Button _arBtn, _homeBtn, _aquariumBtn, _profileBtn,/*_viewInARBtn,*/ _addToCollectBtn;
    public int _fishId;
    public int _fishUnlockPoint;
    public TextMeshProUGUI _unlockPointText;
    public FishAllDetail _fishDetail;

    public TextMeshProUGUI _fishNameText;
    public TextMeshProUGUI _fishDetailText;
    public TextMeshProUGUI _funfactText;
    public TextMeshProUGUI _availabilityText;
    public TextMeshProUGUI _fishTypeText;
    public TextMeshProUGUI _fishOptimalTempText;
    public TextMeshProUGUI _fishMaxSizeText;
    public Image _fishImage;
    public GameObject _fishModel;
    private FishSpawnData _currentFishData;
    GameObject fishInstantiate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        UserData user = UserSaveManager.Load();

        if (user.fishFullSaveData.unlockedFishIDs.Contains(_fishId))
        {
            _addToCollectBtn.gameObject.SetActive(false);
            _fishModel.SetActive(true);
            _fishImage.gameObject.SetActive(false);
        }
        else
        {
            _addToCollectBtn.gameObject.SetActive(true);
            _fishModel.SetActive(false);
            _fishImage.gameObject.SetActive(true);
        }

        _unlockPointText.text = _fishUnlockPoint.ToString();

        _fishImage.sprite = _fishDetail.fishImage;
        _fishNameText.text = _fishDetail.fishName;
        _fishDetailText.text = _fishDetail.fishDetail;
        _fishTypeText.text = _fishDetail.fishType;
        _funfactText.text = _fishDetail.funFact;
        _availabilityText.text = _fishDetail.availability;
        _fishOptimalTempText.text = _fishDetail.fishoptimalTemperature;
        _fishMaxSizeText.text = _fishDetail.fishMaxSize;


        var fishData = DataContainerManager.Instance._fishData
           
     .Find(x => x.fishID == _fishId);

        _currentFishData = fishData;

        if (fishData == null || fishData._placeFishPrefab == null)
        {
            Debug.LogError("FishData or prefab missing for ID: " + _fishId);
            return;
        }

        if (_fishModel == null)
        {
            Debug.LogError("_fishModel is NULL!");
            return;
        }

        // ✅ STORE transform first
        GameObject fishInstantiate = Instantiate(
    fishData._placeFishDetailPrefab) as GameObject;
        fishInstantiate.transform.SetParent(_fishModel.transform, false);



    }
    void Start()
    {
        _arBtn.onClick.AddListener(ArBtnClick);
        //_viewInARBtn.onClick.AddListener(ViewInARBtnClick);
        _addToCollectBtn.onClick.AddListener(AddToCollectBtnClick);
        _aquariumBtn.onClick.AddListener(AquariumBtnClick);
        _backBtn.onClick.AddListener(BackBtnClick);
        _profileBtn.onClick.AddListener(ProfileBtnClick);
        _homeBtn.onClick.AddListener(HomeBtnClick);
    }

    void ArBtnClick()
    {
        SceneManager.LoadScene(1);
    }

    void ViewInARBtnClick()
    {
        SceneManager.LoadScene(1);
    }

    void HomeBtnClick()
    {
        HomeSceneManager.Instance._homePanelScript.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void ProfileBtnClick()
    {
        HomeSceneManager.Instance._profileScript.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void BackBtnClick()
    {
        HomeSceneManager.Instance._allFishScript.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void AddToCollectBtnClick()
    {

        HomeSceneManager.Instance._unlockScript.gameObject.SetActive(true);
        Invoke("EnableModal",2f);
    }
    void EnableModal()
    {
        _fishModel.SetActive(true);
        _fishImage.gameObject.SetActive(false);
    }

    void AquariumBtnClick()
    {
        HomeSceneManager.Instance._myCollectionScript.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        HomeSceneManager.Instance._unlockScript.gameObject.SetActive(false);
        foreach (Transform child in _fishModel.transform)
        {
            Destroy(child.gameObject);
        }

    }
}
