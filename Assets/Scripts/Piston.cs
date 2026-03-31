using System.Collections;
using UnityEngine;

public class Piston : MonoBehaviour
{
    private float startPosY;
    public float targetHeightY;
    private float currentVerticalPos;
    private float speedExtending = 30f;
    private float speedRetracting = 5f;
    public GameObject pistonCylinder;
    private Rigidbody2D pistonCylinderRb;
    public GameObject pistonPushObject;

    public enum PistonFacing
    {
        Up,
        Down,
        Left,
        Right
    }

    public PistonFacing pistonFacing;
    
    // Typically all required buttons need to be pressed for the elevator to activate
    public GameObject[] requiredButtons;
    // Alternatively, all override button(s) combined force the elevator to activate
    public GameObject[] overrideButtons;
    // Only one of the required buttons is required to be pressed for the elevator to activate if this is true, otherwise all required buttons need to be pressed
    public bool onlyOneRequired;
    private bool actuatedState;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosY = pistonCylinder.transform.localPosition.y;
        pistonCylinderRb = pistonCylinder.GetComponent<Rigidbody2D>();
        pistonPushObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonInteractableObjects.ButtonCheck(requiredButtons, overrideButtons, onlyOneRequired))
        {
            InitiateActuatePiston();
        }
        else
        {
            InitiateDeactuatePiston();
        }
    }
    
    private void InitiateActuatePiston()
    {
        if (!actuatedState && startPosY + targetHeightY > currentVerticalPos)
        {
            actuatedState = true;
            StopAllCoroutines();
            StartCoroutine(ActuatePiston());
        }
    }

    private void InitiateDeactuatePiston()
    {
        if (startPosY < currentVerticalPos)
        {
            actuatedState = false;
            StopAllCoroutines();
            StartCoroutine(DeactuatePiston());
        }
    }

    IEnumerator ActuatePiston()
    {
        pistonPushObject.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        while (startPosY + targetHeightY > currentVerticalPos)
        {
            pistonCylinder.transform.Translate(Vector3.up * speedExtending * Time.deltaTime);
            currentVerticalPos = pistonCylinder.transform.localPosition.y;
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator DeactuatePiston()
    {
        while (startPosY < currentVerticalPos)
        {
            pistonCylinder.transform.Translate(Vector3.down * speedRetracting * Time.deltaTime);
            currentVerticalPos = pistonCylinder.transform.localPosition.y;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
