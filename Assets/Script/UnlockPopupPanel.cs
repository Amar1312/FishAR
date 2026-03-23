using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnlockPopupPanel : MonoBehaviour
{
    public Button _unlockBtn, _cancleBtn;
    public TextMeshProUGUI _unLockPointText;

    public int _fishUnlockPoint;
    public int _fishID;
    public FishComponent _fishComponent;
    public GameObject _unlockPopup;
    public TextMeshProUGUI _popupText;
    public FishAllDetail _fishDetail;

    public Image _fishImage;
    public TextMeshProUGUI _fishName;

    private void OnEnable()
    {
        int Point = PointManager.Instance._point;
        string PointData = "Available Point : " + Point.ToString() + "\n Requried Point : " + _fishUnlockPoint.ToString();
        if (_fishUnlockPoint <= Point)
        {
            //_unlockBtn.gameObject.SetActive(true);
            //_lockUnlockText.text = PointData + " \n \n You Can Unlock Fish";
            _popupText.text = "Fish collected";
        }
        else
        {
            //_unlockBtn.gameObject.SetActive(false);
            _popupText.text = " You Can Not sufficient Point";
            //_lockUnlockText.text = PointData + " \n \n You Can Not Unlock Fish \n Point is not sufficient";
        }

        if (PointManager.Instance._unLockFishID.Contains(_fishID))
        {
            _unlockBtn.gameObject.SetActive(false);
        }
        else
        {
            _unlockBtn.gameObject.SetActive(true);
        }

        _unLockPointText.text = _fishUnlockPoint.ToString();
        if (_fishDetail.fishImage != null)
        {
            _fishImage.sprite = _fishDetail.fishImage;
        }
        if (!string.IsNullOrWhiteSpace(_fishDetail.fishName))
        {
            _fishName.text = _fishDetail.fishName;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _unlockBtn.onClick.AddListener(UnLockBtnClick);
        _cancleBtn.onClick.AddListener(CancleBtnClick);
    }

    void CancleBtnClick()
    {
        UIManager.Instance.OnOffGame(true);
        gameObject.SetActive(false);
    }

    void UnLockBtnClick()
    {
        PointManager _pointManager = PointManager.Instance;
        UIManager _uiManager = UIManager.Instance;
        if (_fishUnlockPoint <= _pointManager._point)
        {
            _pointManager.Removepoint(_fishUnlockPoint);
            _pointManager.UnLockFishSave(_fishID);
            _uiManager.OnlyDisplayPoint();
            //_uiManager.CheckAllFishLock();
            _fishComponent.UnLockDone();
            _unlockBtn.gameObject.SetActive(false);
        }
        _unlockPopup.SetActive(true);
        Invoke(nameof(Offpopup), 5f);
    }

    void Offpopup()
    {
        _unlockPopup.SetActive(false);
    }

    private void OnDisable()
    {
        Offpopup();
    }
}
