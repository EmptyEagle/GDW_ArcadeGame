using UnityEngine;
using System.Collections;

public class PistonPush : MonoBehaviour
{
    public float appliedForce;
    private Piston attachedPiston;
    // Makes collisions give consistent force even when objects have more than one collider
    private bool pushedObject;
    private PlayerController impactedPlayer;

    void Start()
    {
        attachedPiston = GetComponentInParent<Piston>();
        pushedObject = false;
    }

    void OnEnable()
    {
        pushedObject = false;
        StopCoroutine(DisableTimer());
        StartCoroutine(DisableTimer());
    }

    IEnumerator DisableTimer()
    {
        yield return new WaitForSeconds(0.25f);
        gameObject.SetActive(false);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ButtonWeightCollision") && !pushedObject)
        {
            if (other.gameObject.GetComponentInParent<PlayerController>() != null)
            {
                impactedPlayer = other.gameObject.GetComponentInParent<PlayerController>();
                impactedPlayer.beingPushed = true;
            }
            Debug.Log("Pushing object");
            pushedObject = true;
            Rigidbody2D pushableRb = other.gameObject.GetComponentInParent<Rigidbody2D>();
            switch (attachedPiston.pistonFacing)
            {
                case Piston.PistonFacing.Up:
                    pushableRb.AddForce(Vector3.up * appliedForce, ForceMode2D.Impulse);
                    break;
                case Piston.PistonFacing.Down:
                    pushableRb.AddForce(Vector3.down * appliedForce, ForceMode2D.Impulse);
                    break;
                case Piston.PistonFacing.Left:
                    pushableRb.AddForce(Vector3.left * appliedForce, ForceMode2D.Impulse);
                    break;
                case Piston.PistonFacing.Right:
                    pushableRb.AddForce(Vector3.right * appliedForce, ForceMode2D.Impulse);
                    break;
            }
        }
    }
}
