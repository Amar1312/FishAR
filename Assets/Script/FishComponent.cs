using UnityEngine;
using UnityEngine.UI;

public class FishComponent : MonoBehaviour
{
    public Button _meBtn;
    public GameObject _placeModel;
    public bool _addPoint;
    public bool _everyTime;
    public int _pointAmountPlace;
    private bool _pointGet;
    public int _fishID;

    public bool _unlock;
    public int _fishUnlockPoint;
    public FishAllDetail _fishDetail;

    public Button _lockBtn;
    public Image _fishBtnImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _meBtn.onClick.AddListener(MeBtnClick);
        _lockBtn.onClick.AddListener(LockBtnClick);
        if (_fishDetail.fishImage != null && _fishBtnImage != null)
        {
            _fishBtnImage.sprite = _fishDetail.fishImage;
        }
    }

    void MeBtnClick()
    {
        UIManager.Instance._gameScript.TapToPlace(_placeModel, this);
    }

    // Call Every Time When Model Place in AR
    public void AddPoint()
    {
        if (!_addPoint)
        {
            return;
        }

        PointManager _pointManager = PointManager.Instance;
        UIManager _uiManager = UIManager.Instance;

        if (_everyTime)
        {
            _pointManager.AddPoint(_pointAmountPlace);
        }
        else
        {
            if (!_pointGet)
            {
                _pointManager.AddPoint(_pointAmountPlace);
                _pointGet = true;
            }

        }
        _uiManager.DisplayPoint();
    }

    public void checkLock()
    {
        UIManager _uiManager = UIManager.Instance;
        PointManager _pointManager = PointManager.Instance;
        if (_unlock)
        {
            return;
        }

        if (_pointManager._unLockFishID.Contains(_fishID))
        {
            UnLockDone();
        }
        else
        {
            Debug.Log("Fish is Lock");
        }
    }
    void LockBtnClick()
    {
        UIManager _uiManager = UIManager.Instance;
        _uiManager._unlockPopupScript._fishID = _fishID;
        _uiManager._unlockPopupScript._fishComponent = this;
        _uiManager._unlockPopupScript._fishUnlockPoint = _fishUnlockPoint;
        _uiManager._unlockPopupScript._fishDetail = _fishDetail;

        _uiManager._unlockPopupScript.gameObject.SetActive(true);
        _uiManager.OnOffGame(false);
    }

    public void UnLockDone()
    {
        UIManager _uiManager = UIManager.Instance;
        _unlock = true;
        _lockBtn.gameObject.SetActive(false);
        _uiManager.OnlyDisplayPoint();
    }
}
