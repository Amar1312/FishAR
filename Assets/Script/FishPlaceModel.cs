using UnityEngine;

public class FishPlaceModel : MonoBehaviour
{
    public FishComponent _fishButtonScript;
    public FishRandomMovement _fishMovement;

    public Transform startPoint;   // Camera position
    public Transform targetPoint;  // Target position
    public float duration = 1.5f;  // Time to reach target
    public float height = 1f;      // Curve height
    public bool _jump;
    public GameObject _waterEffect;

    private bool isMoving = false;
    private float time = 0;
    private Vector3 lastPosition;

    private void OnEnable()
    {
        _fishMovement.enabled = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //AddPoint();
        startPoint = Camera.main.transform;
        targetPoint = this.transform;

        if (_jump)
        {
            StartMove();
        }
        else
        {
            TargetPointFound();
        }
    }


    public void AddPoint()
    {
        _fishButtonScript.AddPoint();
    }


    void Update()
    {
        if (isMoving)
        {
            time += Time.deltaTime;
            float t = time / duration;

            // Stop when reached
            if (t >= 1f)
            {
                _fishMovement.transform.position = targetPoint.position;
                TargetPointFound();
                isMoving = false;
                return;
            }

            // Linear position
            Vector3 linearPos = Vector3.Lerp(startPoint.position, targetPoint.position, t);

            // Add height (parabolic curve)
            float yOffset = height * 4 * (t - t * t);

            Vector3 currentPosition = new Vector3(
                linearPos.x,
                linearPos.y + yOffset,
                linearPos.z
            );

            // Apply position
            _fishMovement.transform.position = currentPosition;

            //  ROTATION PART
            Vector3 direction = currentPosition - lastPosition;

            if (direction != Vector3.zero)
            {
                _fishMovement.transform.rotation = Quaternion.LookRotation(direction);
            }

            lastPosition = currentPosition;
        }
    }

    // Call this once to start movement
    public void StartMove()
    {
        time = 0;
        isMoving = true;
        _fishMovement.transform.position = startPoint.position;
        lastPosition = startPoint.position;
    }

    void TargetPointFound()
    {
        _fishMovement.enabled = true;
        OnEffect();
    }

    public void OnEffect()
    {
        if (_waterEffect != null)
        {
            UIManager.Instance.OnFishInstantiate(true, this.transform);
            _waterEffect.SetActive(true);
            Invoke(nameof(OffEffect), 3f);
            AudioManager.Instance.PlayWaterSplash();
        }
    }

    void OffEffect()
    {
        _waterEffect.SetActive(false);
        UIManager.Instance.OnFishInstantiate(false, this.transform);
    }
}
