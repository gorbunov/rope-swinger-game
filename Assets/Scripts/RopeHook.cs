using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class RopeHook : MonoBehaviour
{
    public GameObject hingeAnchor;
    public SpriteRenderer hingeAnchorRendererer;

    public Transform crosshair;
    public SpriteRenderer crosshairSprite;

    public DistanceJoint2D ropeJoint;

    public LineRenderer ropeLineRenderer;
    public LayerMask ropeLayerMask;

    private float ropeCastDistance = 20f;
    private List<Vector2> ropePoints = new List<Vector2>();
    private bool isRopeAttached = false;

    private Player player;
    public Rigidbody2D ropeHingeAnchorBody;
    private bool distanceSet;

    // Start is called before the first frame update
    void Start()
    {
        ropeJoint.enabled = false;
        player = GetComponent<Player>();
        ropeHingeAnchorBody = hingeAnchor.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRopeAttached)
        {
            SetCrosshairPosition(GetAimAngle());
        }
        else
        {
            crosshairSprite.enabled = false;
        }

        if (Input.GetMouseButton(0))
        {
            CastRopeHook(AimDirection(GetAimAngle()));
        }
        UpdateRopePoints();
    }

    private float GetAimAngle()
    {
        var worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var facingDirection = worldMousePos - transform.position;
        float aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
        if (aimAngle < 0f)
        {
            aimAngle += 2 * Mathf.PI;
        }

        return aimAngle;
    }

    private Vector2 AimDirection(float aimAngle)
    {
        return Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
    }

    private void SetCrosshairPosition(float aimAngle)
    {
        if (!crosshairSprite.enabled)
        {
            crosshairSprite.enabled = true;
        }

        var position = transform.position;
        float cx = position.x + 1f * Mathf.Cos(aimAngle);
        float cy = position.y + 1f * Mathf.Sin(aimAngle);

        Vector3 crosshairPosition = new Vector3(cx, cy, 0f);
        crosshair.transform.position = crosshairPosition;
        Debug.DrawRay(position, crosshair.localPosition.normalized * 20f, Color.red);
    }

    private void CastRopeHook(Vector2 aimDirection)
    {
        if (isRopeAttached) return;
        ropeLineRenderer.enabled = true;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, aimDirection, ropeCastDistance, ropeLayerMask);
        if (hit.collider != null)
        {
            isRopeAttached = true;
            ropePoints.Add(transform.position);
            if (!ropePoints.Contains(hit.point))
            {
                ropePoints.Add(hit.point);
                ropeJoint.distance = Vector2.Distance(transform.position, hit.point);
                ropeJoint.enabled = true;
                hingeAnchorRendererer.enabled = true;
            }
        }
        else
        {
            ropeLineRenderer.enabled = false;
            isRopeAttached = false;
            ropeJoint.enabled = false;
        }

    }

    private void UpdateRopePoints()
    {
        if (!isRopeAttached) return;
        ropeLineRenderer.positionCount = ropePoints.Count + 1;
        for (int i = ropeLineRenderer.positionCount - 1; i >= 0; i--)
        {
            if (i != ropeLineRenderer.positionCount - 1)
            {
                ropeLineRenderer.SetPosition(i, ropePoints[i]);
            }

            if (i == ropePoints.Count - 1 || ropePoints.Count == 1)
            {
                var ropePosition = ropePoints[ropePoints.Count - 1];
                if (ropePoints.Count == 1)
                {
                    ropeHingeAnchorBody.transform.position = ropePosition;
                    if (!distanceSet)
                    {
                        ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                        distanceSet = true;
                    }
                }
                else
                {
                    ropeHingeAnchorBody.transform.position = ropePosition;
                    if (!distanceSet)
                    {
                        ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                        distanceSet = true;
                    }
                }
            }
            // 5
            else if (i - 1 == ropePoints.IndexOf(ropePoints.Last()))
            {
                var ropePosition = ropePoints.Last();
                ropeHingeAnchorBody.transform.position = ropePosition;
                if (!distanceSet)
                {
                    ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                    distanceSet = true;
                }
            }

            else
            {
                // 6
                ropeLineRenderer.SetPosition(i, transform.position);
            }
        }
    }
}
