using UnityEngine;

public class Box : MonoBehaviour
{
    private PolygonCollider2D[] boxCollisionPolygons;
    private Rigidbody2D boxRb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxRb = GetComponent<Rigidbody2D>();
        boxCollisionPolygons = GetComponentsInChildren<PolygonCollider2D>();
        foreach (PolygonCollider2D boxCollisionPolygon in boxCollisionPolygons)
        {
            foreach (GameObject boxGate in GameObject.FindGameObjectsWithTag("BoxGate"))
            {
                Physics2D.IgnoreCollision(boxCollisionPolygon, boxGate.GetComponent<BoxCollider2D>());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PhysicsOnTop"))
        {
            Rigidbody2D otherRb = other.gameObject.GetComponentInParent<Rigidbody2D>();
            float otherVelocityHorizontal = otherRb.linearVelocity.x;
            boxRb.AddForce(otherVelocityHorizontal * Vector3.right * 26f);
        }
    }
}
