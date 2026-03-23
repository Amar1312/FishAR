using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    [SerializeField] private Transform CoinImage;
    //[SerializeField] private Transform coinBg;
    [SerializeField] private Vector2[] initialPos;
    [SerializeField] private Quaternion[] initialRotation;
    [SerializeField] private GameObject[] _coins;
    [SerializeField] private int coinsAmount;
    //UiPanelManager _uiManager;
    [SerializeField] bool _islocalmove;

    void Start()
    {
        if (coinsAmount >= transform.childCount)
        {
            coinsAmount = transform.childCount;
        }

        initialPos = new Vector2[coinsAmount];
        initialRotation = new Quaternion[coinsAmount];

        for (int i = 0; i < coinsAmount; i++)
        {
            initialPos[i] = transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition;
            initialRotation[i] = transform.GetChild(i).GetComponent<RectTransform>().rotation;
        }
        CountCoins();
    }

    private void OnEnable()
    {

    }


    //[Button]
    public void CountCoins()
    {
        var delay = 0f;

        for (int i = 0; i < coinsAmount; i++)
        {
            _coins[i].transform.DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);


            if (_islocalmove)
            {
                Vector3 targetWorldPos = CoinImage.position;
                Vector3 localTarget = _coins[i].transform.parent.InverseTransformPoint(targetWorldPos);
                _coins[i].transform.DOLocalMove(localTarget, 0.8f)
                    .SetDelay(delay + 0.5f).SetEase(Ease.InBack);

            }
            else
            {

                _coins[i].transform.DOMove(CoinImage.position, 0.8f)
                    .SetDelay(delay + 0.5f).SetEase(Ease.InBack);
            }


            _coins[i].transform.DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.5f)
                .SetEase(Ease.Flash);


            _coins[i].transform.DOScale(0f, 0.3f).SetDelay(delay + 1.5f).SetEase(Ease.OutBack);

            delay += 0.1f;

        }

        Invoke(nameof(CoinResetPostion), 3);
    }

    void CoinResetPostion()
    {
        //_uiManager.UpdateScore();
        for (int i = 0; i < _coins.Length; i++)
        {
            _coins[i].GetComponent<RectTransform>().anchoredPosition = initialPos[i];
        }
    }


}