using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] bool onGround = default;
    [SerializeField] bool onWall = default;
    [SerializeField] bool onRightWall = default;
    [SerializeField] bool onLeftWall = default;

    public bool OnGround => onGround;
    public bool OnWall => onWall;
    public bool OnRightWall => onRightWall;
    public bool OnLeftWall => onLeftWall;
    
    [Space]
    
    [Header("Layers")]
    [SerializeField] LayerMask groundLayer = default;
    
    [Space]
    
    [Header("Collision")]
    [SerializeField] float collisionRadius = 0.25f;
    [SerializeField] Vector2 bottomOffset = default, rightOffset = default, leftOffset = default;

    void Update()
    {  
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        onWall = onRightWall || onLeftWall;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position  + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }
}
