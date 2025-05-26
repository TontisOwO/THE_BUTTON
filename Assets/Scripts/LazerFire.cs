using UnityEngine;

[RequireComponent (typeof(LineRenderer))]
public class LazerFire : MonoBehaviour
{
    [SerializeField] int reflections;
    [SerializeField] float maxRayLength;
    [SerializeField] LineRenderer myLineRenderer;
    [SerializeField] Ray ray;
    [SerializeField] RaycastHit hit;
    [SerializeField] Vector3 direction;
    [SerializeField] Transform targetHitTransform;
    [SerializeField] Transform[] newMirrors;
    [SerializeField] Transform[] oldMirrors;
    [SerializeField] LayerMask ignoreLayer;
    
    void Awake()
    {
        myLineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        ray = new Ray(transform.position, transform.up);
        
        myLineRenderer.positionCount = 1;
        myLineRenderer.SetPosition (0,transform.position);
        float remainingRayLength = maxRayLength;

        for (int i = 0; i < reflections; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, remainingRayLength, ignoreLayer);
            if (hit)
            {
                myLineRenderer.positionCount++;
                myLineRenderer.SetPosition (myLineRenderer.positionCount - 1, hit.point);
                remainingRayLength -= Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                if (hit.collider.CompareTag("Mirror") == false)
                {
                    break;
                }
                hit.transform.gameObject.layer = 10;
                newMirrors = new Transform[oldMirrors.Length + 1];
                for (int f = 0; f < newMirrors.Length - 1; f++)
                {
                    newMirrors[f] = oldMirrors[f];
                    newMirrors[f].gameObject.layer = 0;
                    newMirrors[f].tag = "Untagged";
                }
                newMirrors[newMirrors.Length - 1] = hit.transform;
                oldMirrors = newMirrors;
            }
            else
            {
                myLineRenderer.positionCount++;
                myLineRenderer.SetPosition(myLineRenderer.positionCount - 1, ray.origin + ray.direction * remainingRayLength);
            }
        }
        for (int i = 0; i < newMirrors.Length; i++)
        {
            newMirrors[i].gameObject.layer = 0;
            newMirrors[i].tag = "Mirror";
        }
        newMirrors = new Transform[0];
        oldMirrors = new Transform[0];
    }
}
