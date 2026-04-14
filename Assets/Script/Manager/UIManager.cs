using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Mihir;
using System.Collections.Generic;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI _pointText;
    public List<FishComponent> _allPointFish;
    public GamePanel _gameScript;
    public UnlockPopupPanel _unlockPopupScript;
    public List<GameObject> _gamePanel;
    public List<GameObject> _selectedFish;

    [Space]
    [Header("Spawn")]
    public Transform _gamePanelContainer;
    public FishComponent _fishComponentPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DataContainerManager _dataManager = DataContainerManager.Instance;
        for (int i = 0; i < _dataManager._fishData.Count; i++)
        {
            FishComponent fishCom = Instantiate(_fishComponentPrefab, _gamePanelContainer);
            fishCom._fishDetail = _dataManager._fishData[i].fishData;
            fishCom._fishID = _dataManager._fishData[i].fishID;
            // Use fishUnlockCost so the cost is never overwritten by the runtime unlock flag
            fishCom._fishUnlockPoint = _dataManager._fishData[i].fishUnlockCost;
            fishCom._placeModel = _dataManager._fishData[i]._placeFishPrefab;

            _allPointFish.Add(fishCom);
        }

        DisplayPoint();
        CheckAllFishLock();
    }
    void Update()
    {
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);

        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        //        RaycastHit hit;

        //        if (Physics.Raycast(ray, out hit))
        //        {
        //            Debug.Log("Hit: " + hit.collider.gameObject.name);
        //            _selectedFish.Add(hit.collider.gameObject);

        //        }
        //    }
        //}
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name);
                _selectedFish.Add(hit.collider.gameObject);
            }
        }

    }
    public void DisplayPoint()
    {
        _pointText.text = PointManager.Instance._point.ToString();
        //CheckAllFishLock();
    }

    public void OnlyDisplayPoint()
    {
        _pointText.text = PointManager.Instance._point.ToString();
    }

    public void CheckAllFishLock()
    {
        for (int i = 0; i < _allPointFish.Count; i++)
        {
            _allPointFish[i].checkLock();
        }
    }

    public void OnOffGame(bool status)
    {
        for (int i = 0; i < _gamePanel.Count; i++)
        {
            _gamePanel[i].SetActive(status);
        }
    }

public void OnFishSelected()
    {

    }
}
