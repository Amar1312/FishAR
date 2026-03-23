using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UnlockPopupHomePanel : MonoBehaviour
{
    //public Button _unlockBtn, _cancleBtn;
    public TextMeshProUGUI _lockUnlockText;

    public int _fishUnlockPoint;
    public int _fishID;
    public FishDetail _fishDetail;

    private void OnEnable()
    {
        int Point = PointManager.Instance._point;
        string PointData = "Available Point : " + Point.ToString() + "\n Requried Point : " + _fishUnlockPoint.ToString();
        if (_fishUnlockPoint <= Point)
        {
            //_unlockBtn.gameObject.SetActive(true);
            _lockUnlockText.text = "Fish collected";
            UnLockBtnClick();
        }
        else
        {
            //_unlockBtn.gameObject.SetActive(false);
            _lockUnlockText.text = " You Can Not sufficient Point";
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //_unlockBtn.onClick.AddListener(UnLockBtnClick);
        //_cancleBtn.onClick.AddListener(CancleBtnClick);
    }

    void CancleBtnClick()
    {
        gameObject.SetActive(false);
    }

    void UnLockBtnClick()
    {
        PointManager _pointManager = PointManager.Instance;
        if (_fishUnlockPoint <= _pointManager._point)
        {
            _pointManager.Removepoint(_fishUnlockPoint);
            _pointManager.UnLockFishSave(_fishID);
            //_uiManager.OnlyDisplayPoint();
            //_uiManager.CheckAllFishLock();
            _fishDetail.UnLockDone();
            HomeSceneManager.Instance._fishDetailScript._addToCollectBtn.gameObject.SetActive(false);
            Invoke(nameof(Offpopup), 5f);
        }
    }

    void Offpopup()
    {
        gameObject.SetActive(false);
    }
}
