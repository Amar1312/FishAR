using UnityEngine;

public class FishRandomMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;
    public float reachThreshold = 0.2f;

    [Header("Bounds Area")]
    public Vector3 areaCenter = Vector3.zero;
    public Vector3 areaSize = new Vector3(10, 5, 10);

    [Header("Obstacle Detection")]
    public float rayDistance = 1.5f;
    public LayerMask obstacleLayers;

    private Vector3 targetPos;

    public GameObject _parentObject;
    public Vector3 _offset;

    [Header("Restricted Area Layer")]
    public LayerMask restrictedLayer;   // layer fish should NOT enter
    public float spawnCheckRadius = 0.5f;
    public float boundaryDetectDistance = 1f;

    bool spawnedInsideRestricted;
    Collider restrictedCollider;

    private void OnEnable()
    {
        //ChooseNewTarget();
        //areaCenter = _parentObject.transform.position + _offset;
        CheckSpawnPosition();
    }

    void Start()
    {
        ChooseNewTarget();
    }

    void Update()
    {
        KeepInsideBounds();   // 🔹 Always enforce boundary

        Vector3 moveDir = (targetPos - transform.position).normalized;

        // ---------- Obstacle Check ----------
        RaycastHit hit;
        if (Physics.Raycast(transform.position, moveDir, out hit, rayDistance, obstacleLayers))
        {
            Vector3 avoidDir = Vector3.Reflect(moveDir, hit.normal).normalized;

            Vector3 newTarget = transform.position + avoidDir * 3f;
            targetPos = ClampInsideArea(newTarget); // 🔹 Clamp target
        }


        // ---------- Area Check ----------
        RaycastHit hit1;
        if (Physics.Raycast(transform.position, moveDir, out hit1, boundaryDetectDistance, restrictedLayer))
        {
            // Change direction so fish doesn't enter object
            Vector3 avoidDir = Vector3.Reflect(moveDir, hit1.normal).normalized;

            Vector3 newTarget = transform.position + avoidDir * 3f;
            targetPos = ClampInsideArea(newTarget);
        }

        // ---------- Boundary Rule ----------
        if (restrictedCollider != null)
        {
            bool currentlyInside = IsInsideCollider(restrictedCollider, transform.position);

            if (spawnedInsideRestricted && !currentlyInside)
            {
                // Fish trying to leave → push back inside
                Vector3 dir = (areaCenter - transform.position).normalized;
                targetPos = transform.position + dir * 3f;
            }

            if (!spawnedInsideRestricted && currentlyInside)
            {
                // Fish trying to enter → push outside
                Vector3 dir = (transform.position - restrictedCollider.bounds.center).normalized;
                targetPos = transform.position + dir * 3f;
            }
        }

        // ---------- Movement ----------
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        Vector3 direction = (targetPos - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 2f);
        }

        if (Vector3.Distance(transform.position, targetPos) < reachThreshold)
        {
            ChooseNewTarget();
        }
    }

    void ChooseNewTarget()
    {
        float x = Random.Range(-areaSize.x / 2f, areaSize.x / 2f);
        float y = Random.Range(-areaSize.y / 2f, areaSize.y / 2f);
        float z = Random.Range(-areaSize.z / 2f, areaSize.z / 2f);

        targetPos = areaCenter + new Vector3(x, y, z);
        targetPos = ClampInsideArea(targetPos); // 🔹 Clamp safety
    }

    Vector3 ClampInsideArea(Vector3 pos)
    {
        Vector3 min = areaCenter - areaSize / 2f;
        Vector3 max = areaCenter + areaSize / 2f;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);
        pos.z = Mathf.Clamp(pos.z, min.z, max.z);

        return pos;
    }

    void KeepInsideBounds()
    {
        transform.position = ClampInsideArea(transform.position);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(areaCenter, areaSize);
    }

    void CheckSpawnPosition()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, spawnCheckRadius, restrictedLayer);

        if (hits.Length > 0)
        {
            // Fish spawned inside restricted area
            spawnedInsideRestricted = true;
            restrictedCollider = hits[0];

            if (hits[0].TryGetComponent<PondWater>(out PondWater _pond))
            {
                areaCenter = _pond._parentObject.transform.position + _offset;
                Debug.Log("Inside center " + areaCenter);
                areaSize = new Vector3(areaSize.x, 0.3f, areaSize.z);
            }

            Debug.Log("Spawned INSIDE restricted area");
        }
        else
        {
            // Fish spawned outside
            spawnedInsideRestricted = false;
            areaCenter = _parentObject.transform.position + _offset;
            Debug.Log("Outside center " + areaCenter);
            Debug.Log("Spawned OUTSIDE restricted area");
        }
    }

    bool IsInsideCollider(Collider col, Vector3 worldPos)
    {
        Vector3 localPos = col.transform.InverseTransformPoint(worldPos);

        if (col is BoxCollider box)
        {
            Vector3 half = box.size * 0.5f;

            Vector3 center = box.center;

            Vector3 min = center - half;
            Vector3 max = center + half;

            return (localPos.x > min.x && localPos.x < max.x &&
                    localPos.y > min.y && localPos.y < max.y &&
                    localPos.z > min.z && localPos.z < max.z);
        }

        return false;
    }
}