using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishDetail : MonoBehaviour
{
    public Button _unLockBtn, _lockBtn;
    public Button _likeBtn;
    public Image _likeImage;
    public Sprite _likeSprite, _unlikesprite;
    public FishAllDetail _fishDetail;

    public GameObject _unLockAllObj;
    public GameObject _lockAllObj;
    public int _fishID;
    public int _fishUnlockPoint;
    public TextMeshProUGUI _fishName;
    public Image _fishImage;

    private void OnEnable()
    {
        PointManager _pointManager = PointManager.Instance;
        if (_pointManager._unLockFishID.Contains(_fishID) || _fishID == 0)
        {
            UnLockDone();
        }
        else
        {
            LockOn();
        }

        if (_pointManager._likeFishID.Contains(_fishID))
        {
            _fishDetail.fevarit = true;
            _likeImage.sprite = _likeSprite;
        }
        else
        {
            _fishDetail.fevarit = false;
            _likeImage.sprite = _unlikesprite;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _unLockBtn.onClick.AddListener(UnLockBtnClick);
        _lockBtn.onClick.AddListener(LockBtnClick);
        _likeBtn.onClick.AddListener(LikeBtnClick);

        _fishName.text = _fishDetail.fishName;
        _fishImage.sprite = _fishDetail.fishImage;
    }

    void UnLockBtnClick()
    {
        HomeSceneManager.Instance._fishDetailScript._fishId = _fishID;
        HomeSceneManager.Instance._fishDetailScript._fishUnlockPoint = _fishUnlockPoint;
        HomeSceneManager.Instance._fishDetailScript._fishDetail = _fishDetail;
        HomeSceneManager.Instance.FishDetailPanelOn();
    }

    void LikeBtnClick()
    {
        _fishDetail.fevarit = !_fishDetail.fevarit;
        bool like = _fishDetail.fevarit;
        if (like)
        {
            _likeImage.sprite = _likeSprite;
            PointManager.Instance.AddLikeData(_fishID);
        }
        else
        {
            _likeImage.sprite = _unlikesprite;
            PointManager.Instance.RemoveLikeData(_fishID);
        }
    }

    public void UnLockDone()
    {
        _unLockAllObj.SetActive(true);
        _lockAllObj.SetActive(false);
    }

    public void LockOn()
    {
        _unLockAllObj.SetActive(false);
        _lockAllObj.SetActive(true);
    }

    void LockBtnClick()
    {
        UnlockPopupHomePanel _popup = HomeSceneManager.Instance._unlockScript;
        _popup._fishID = _fishID;
        _popup._fishUnlockPoint = _fishUnlockPoint;
        _popup._fishDetail = this;
        HomeSceneManager.Instance._fishDetailScript._fishId = _fishID;
        HomeSceneManager.Instance._fishDetailScript._fishUnlockPoint = _fishUnlockPoint;
        HomeSceneManager.Instance._fishDetailScript._fishDetail = _fishDetail;
        HomeSceneManager.Instance.FishDetailPanelOn();
        //_popup.gameObject.SetActive(true);
    }

}
