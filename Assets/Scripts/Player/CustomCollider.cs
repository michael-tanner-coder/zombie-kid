using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ScriptableObjectArchitecture;

public struct RayRange 
{
    public RayRange(float x1, float y1, float x2, float y2, Vector2 dir) {
        Start = new Vector2(x1, y1);
        End = new Vector2(x2, y2);
        Dir = dir;
    }

    public readonly Vector2 Start, End, Dir;
}

public class CustomCollider : MonoBehaviour
{
    [Header("COLLISION")] 
    [SerializeField] private Bounds _bounds;
    
    [SerializeField] private LayerMask _collisionLayer;
    public LayerMask CollisionLayer => _collisionLayer;

    [SerializeField] private int _detectorCount = 3;
    
    [SerializeField] private float _detectionRayLength = 0.1f;
    
    [SerializeField] [Range(0.1f, 0.3f)] private float _rayBuffer = 0.1f;
    
    [SerializeField, Tooltip("Raising this value increases collision accuracy at the cost of performance.")]
    
    private int _freeColliderIterations = 10;
    public int FreeColliderIterations => _freeColliderIterations;

    private RayRange _raysUp, _raysRight, _raysDown, _raysLeft;

    private bool _colUp, _colRight, _colDown, _colLeft;
    public bool ColUp => _colUp;
    public bool ColRight => _colRight; 
    public bool ColDown => _colDown; 
    public bool ColLeft => _colLeft;

    public RaycastHit2D CurrentHit;

    public void RunCollisionChecks()
    {
        // Generate ray ranges
        CalculateRayRanged();

        // Collision sides
        _colUp = RunDetection(_raysUp);
        _colDown = RunDetection(_raysDown);
        _colLeft = RunDetection(_raysLeft);
        _colRight = RunDetection(_raysRight);

        bool RunDetection(RayRange range) 
        {
            return EvaluateRayPositions(range).Any(point => {
                RaycastHit2D hit = Physics2D.Raycast(point, range.Dir, _detectionRayLength, _collisionLayer);

                if (hit.collider != null)
                {
                    CurrentHit = hit;
                }

                return hit.collider != null;
            });
        }
    }

    public bool GetOverLap(Vector3 position, LayerMask layer)
    {
        return Physics2D.OverlapBox(position, _bounds.size, 0, layer);
    }

    public Bounds GetBounds()
    {
        return _bounds;
    }

    private void CalculateRayRanged() 
    {
        var b = new Bounds(transform.position + _bounds.center, _bounds.size);

        _raysDown = new RayRange(b.min.x + _rayBuffer, b.min.y, b.max.x - _rayBuffer, b.min.y, Vector2.down);
        _raysUp = new RayRange(b.min.x + _rayBuffer, b.max.y, b.max.x - _rayBuffer, b.max.y, Vector2.up);
        _raysLeft = new RayRange(b.min.x, b.min.y + _rayBuffer, b.min.x, b.max.y - _rayBuffer, Vector2.left);
        _raysRight = new RayRange(b.max.x, b.min.y + _rayBuffer, b.max.x, b.max.y - _rayBuffer, Vector2.right);
    }

    private IEnumerable<Vector2> EvaluateRayPositions(RayRange range) 
    {
        for (var i = 0; i < _detectorCount; i++) 
        {
            var t = (float)i / (_detectorCount - 1);
            yield return Vector2.Lerp(range.Start, range.End, t);
        }
    }

    private void OnDrawGizmos() 
    {
        // Bounds
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + _bounds.center, _bounds.size);

        // Rays
        if (!Application.isPlaying) 
        {
            CalculateRayRanged();
            Gizmos.color = Color.blue;
            foreach (var range in new List<RayRange> { _raysUp, _raysRight, _raysDown, _raysLeft }) 
            {
                foreach (var point in EvaluateRayPositions(range)) 
                {
                    Gizmos.DrawRay(point, range.Dir * _detectionRayLength);
                }
            }
        }

        if (!Application.isPlaying) return;
    }

}
