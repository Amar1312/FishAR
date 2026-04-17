using Mihir;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI _pointText;
    public  List<FishComponent> _allPointFish;
    public GamePanel _gameScript;
    public UnlockPopupPanel _unlockPopupScript;
    public List<GameObject> _gamePanel;
    public List<GameObject> _selectedFish = new List<GameObject>();
    private Dictionary<GameObject, Color> _originalColors = new Dictionary<GameObject, Color>();
    public GameObject _pointAnimation,_tutorialPanel,_placeAllScollImage,_bottomBarImage;
    public GameObject[] panels;

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
        _pointAnimation.SetActive(false);
        if(PlayerPrefs.HasKey("OpenAppTime"))
        {
            int num =PlayerPrefs.GetInt("OpenAppTime");
            if(num == 1)
            {
                _tutorialPanel.SetActive(true);
                panels[0].SetActive(true);
            }
            else
            {
                _tutorialPanel.SetActive(false);
                
            }

        }
    

        DisplayPoint();
        CheckAllFishLock();
    }
    void Update()
    {
        // Mobile
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            HandleSelection(Input.GetTouch(0).position, Input.GetTouch(0).fingerId);
        }

#if UNITY_EDITOR
        // Mouse (Editor)
        if (Input.GetMouseButtonDown(0))
        {
            HandleSelection(Input.mousePosition, -1);
        }
#endif
    }


    void HandleSelection(Vector2 screenPosition, int fingerId)
    {
        // Ignore UI clicks
        if (IsPointerOverUI(fingerId))
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        // If nothing hit → clear selection
        if (!Physics.Raycast(ray, out RaycastHit hit))
        {
            ClearSelection();
        
            return;
        }

        GameObject hitObject = hit.collider.gameObject;
        Debug.Log($"Hit: {hitObject.name}");

        GameObject target = GetTarget(hitObject);

        if (target != null)
        {
            Debug.Log("Target game object: " + target.name);
            SelectTarget(target);
        }
    }

    bool IsPointerOverUI(int fingerId)
    {
        // Touch
        if (fingerId >= 0)
        {
            return EventSystem.current.IsPointerOverGameObject(fingerId);
        }

        // Mouse
        return EventSystem.current.IsPointerOverGameObject();
    }

    GameObject GetTarget(GameObject hitObject)
    {
        Debug.Log("Hit object name: " + hitObject.name);

        // Case 1: Fish child clicked
        if (hitObject.name == "Fish Movement" || hitObject.name == "Pond Water1")
        {
            return hitObject.transform.parent?.parent?.gameObject;
        }

        // Case 2: Container clicked
        if (hitObject.CompareTag("Container"))
        {
            return hitObject.transform.parent?.gameObject;
        }

        return null;
    }

    void SelectTarget(GameObject target)
    {
        // Toggle OFF if already selected
        if (_selectedFish.Contains(target))
        {
            RemoveHighlight(target);
            _selectedFish.Remove(target);

            Debug.Log($"Deselected: {target.name}");

            _bottomBarImage.SetActive(false);
            _placeAllScollImage.SetActive(true);

            return;
        }

        // Select new target
        ClearSelection();

        _selectedFish.Add(target);
        Debug.Log($"Selected: {target.name}");

        _placeAllScollImage.SetActive(false);
        _bottomBarImage.SetActive(true);

        HighlightFish(target);
    }

    void HighlightFish(GameObject target)
    {
        Renderer renderer = target.GetComponentInChildren<Renderer>();

        if (renderer != null)
        {
            if (!_originalColors.ContainsKey(target))
            {
                _originalColors[target] = renderer.material.color;
            }

            renderer.material.color = Color.yellow;
            Debug.Log($"Highlighted: {target.name}");
        }
    }

    void ClearSelection()
    {
        foreach (GameObject fish in _selectedFish)
        {
            RemoveHighlight(fish);
        }

        _selectedFish.Clear();
    }

    void RemoveHighlight(GameObject target)
    {
        Renderer renderer = target.GetComponentInChildren<Renderer>();

        if (renderer != null && _originalColors.ContainsKey(target))
        {
            renderer.material.color = _originalColors[target];
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
        for (int i = 0; i < _selectedFish.Count; i++)
        {
            Destroy(_selectedFish[i]);
        }

        _selectedFish.Clear();

    }



    public void OnFishInstantiate(bool status, Transform camTransform)
    {
        if (camTransform == null)
        {
            Debug.LogWarning("Camera transform is null");
            return;
        }

        Vector3 spawnPosition = camTransform.position + new Vector3(0,0.2f,0);

        _pointAnimation.transform.position = spawnPosition;

        
        _pointAnimation.transform.forward = Camera.main.transform.forward;

        _pointAnimation.SetActive(status);

        if (status)
        {
            
            PointManager.Instance.AddPoint(5);
            DisplayPoint();
        }
    }
    public void TutorialPanelOn(int panel)
    {
      
          for (int i = 0; i < panels.Length; i++)
                {
                    if (i == panel)
                    {
                        panels[i].SetActive(true);
                    }
                    else
                    {
                        panels[i].SetActive(false);
                    }
                }



    }
    public void TutorialPanelOff(int i)
    {

        if (i == 0)
        {
            _tutorialPanel.SetActive(true);
            panels[1].SetActive(true);
            panels[0].SetActive(false);
        }
        else if (i == 2)
        {
            _tutorialPanel.SetActive(false);
            for (int j = 0; j < panels.Length; j++)
            {
                panels[j].SetActive(false);
            }
        }
        else
        {
          
            panels[i].SetActive(false);
        }

    }

}
