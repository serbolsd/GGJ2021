using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to detect if a character is on the ground
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class GroundDetection : MonoBehaviour
{
  /// <summary>
  /// Require que tu pongas el box collider.
  /// </summary>
  public BoxCollider2D dectector;

  public int colliderID;

  private bool isOnGround_ { get; set; }
  public bool isOnGround => isOnGround_;

  private void Start()
  {
    Debug.Assert(dectector != null);
    dectector.isTrigger = true;
    colliderID = Time.frameCount;
    dectector.name = "id " + colliderID.ToString() + " Box detector";
    isOnGround_ = false;
  }

  public bool init(Vector2 boxPos, Vector2 boxSize)
  {
    if (float.Epsilon > boxSize.x || float.Epsilon > boxSize.y)
    {
      return false;
    }

    dectector.transform.position = boxPos;
    Vector2 newBoxOffset = new Vector2(0, -2);
    dectector.offset = newBoxOffset; 
    Vector2 newBoxSize = new Vector2(boxSize.x, boxSize.y);
    dectector.size = newBoxSize; 
    return true;
  }


  private void OnTriggerEnter2D(Collider2D collision)
  {
    isOnGround_ = true;
  }

  private void OnTriggerStay2D(Collider2D collision)
  {
    isOnGround_ = true;
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    isOnGround_ = false;
  }

}
