using UnityEngine;

public class Some : MonoBehaviour
{
    [SerializeField] private Transform _point;
    
    [SerializeField] private float _radius = 5f;
    
    [SerializeField] private float _radiusDelta = 0.1f;
    [SerializeField] private float _angleDelta = 3.14f;
    
    [SerializeField] private float _debugDrawLineDuration = 1f;

    private void Update()
    {
        var oldPosition = transform.position;
        
        var radiusVector = (Vector2) (oldPosition - _point.position);

        var radius = radiusVector.magnitude;
        var angle = Mathf.Atan2(radiusVector.y, radiusVector.x);

        radius += _radiusDelta * Time.deltaTime;
        angle += _angleDelta * Time.deltaTime;
        
        radius = Mathf.Min(radius, _radius);

        radiusVector = radius * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        
        var newPosition = _point.position + (Vector3) radiusVector;

        Debug.DrawLine(oldPosition, newPosition, Color.red, _debugDrawLineDuration);
        
        transform.position = newPosition;
    }
}
