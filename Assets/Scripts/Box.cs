using UnityEngine;

public class Box : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("Button"))
        //{
        //    if (other.gameObject.GetComponent<Button>() != null)
        //    {
        //        Button buttonScript = other.gameObject.GetComponent<Button>();
        //        buttonScript.SetButtonState(true);
        //    }
        //}
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("Button"))
        //{
        //    if (other.gameObject.GetComponent<Button>() != null)
        //    {
        //        Button buttonScript = other.gameObject.GetComponent<Button>();
        //        buttonScript.SetButtonState(false);
        //    }
        //}
    }
}
