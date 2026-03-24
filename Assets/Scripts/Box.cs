using UnityEngine;

public class Box : MonoBehaviour
{
    private PolygonCollider2D[] boxCollisionPolygons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollisionPolygons = GetComponentsInChildren<PolygonCollider2D>();
        foreach (PolygonCollider2D boxCollisionPolygon in boxCollisionPolygons)
        {
            foreach (GameObject boxGate in GameObject.FindGameObjectsWithTag("BoxGate"))
            {
                Physics2D.IgnoreCollision(boxCollisionPolygon, boxGate.GetComponent<BoxCollider2D>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
