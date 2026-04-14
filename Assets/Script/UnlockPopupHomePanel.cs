using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UnlockPopupHomePanel : MonoBehaviour
{
    public TextMeshProUGUI _lockUnlockText;

    public int _fishUnlockPoint;
    public int _fishID;
    public FishDetail _fishDetail;

    private void OnEnable()
    {
        UserData user = UserSaveManager.Load();
        if (_fishUnlockPoint <= user.points)
        {
            _lockUnlockText.text = "Fish collected";
            UnLockBtnClick();
        }
        else
        {
            _lockUnlockText.text = "You do not have sufficient points";
        }
    }

    void Start()
    {
    }

    void CancleBtnClick()
    {
        gameObject.SetActive(false);
    }

    void UnLockBtnClick()
    {
        PointManager _pointManager = PointManager.Instance;

        // Re-read fresh user to get the current point balance
        UserData user = UserSaveManager.Load();
        if (_fishUnlockPoint <= user.points)
        {
            // Removepoint saves internally — do NOT re-save user after this
            _pointManager.Removepoint(_fishUnlockPoint);
            _pointManager.UnLockFishSave(_fishID);

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

