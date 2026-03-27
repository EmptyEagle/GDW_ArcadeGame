using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour
{
    private float startPosY;
    private float gateHeight = 3;
    private float currentVerticalPos;
    private float gateSpeedOpen = 0.05f;
    private float gateSpeedClose = 0.2f;
    
    // Typically all required buttons need to be pressed for the gate to open
    public GameObject[] requiredButtons;
    // Alternatively, all override button(s) combined force the gate to be open
    public GameObject[] overrideButtons;
    // Only one of the required buttons is required to be pressed to open the gate if this is true, otherwise all required buttons need to be pressed
    public bool onlyOneRequired;
    private bool noButtonsAttached;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosY = transform.localPosition.y;
        if (requiredButtons.Length < 1 && overrideButtons.Length < 1)
        {
            noButtonsAttached = true;
        }
        else
        {
            noButtonsAttached = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonInteractableObjects.ButtonCheck(requiredButtons, overrideButtons, onlyOneRequired) && !noButtonsAttached)
        {
            InitiateOpenGate();
        }
        else
        {
            InitiateCloseGate();
        }
    }
    
    private void InitiateOpenGate()
    {
        if (startPosY + gateHeight > currentVerticalPos)
        {
            StopAllCoroutines();
            StartCoroutine(OpenGate());
        }
    }
    
    private void InitiateCloseGate()
    {
        if (startPosY < currentVerticalPos)
        {
            StopAllCoroutines();
            StartCoroutine(CloseGate());
        }
    }

    IEnumerator OpenGate()
    {
        while (startPosY + gateHeight > currentVerticalPos)
        {
            transform.Translate(Vector3.up * gateSpeedOpen);
            currentVerticalPos = transform.localPosition.y;
            yield return new WaitForSeconds(0.02f);
        }
    }
    
    IEnumerator CloseGate()
    {
        while (startPosY < currentVerticalPos)
        {
            transform.Translate(Vector3.down * gateSpeedClose);
            currentVerticalPos = transform.localPosition.y;
            yield return new WaitForSeconds(0.005f);
        }
    }
}
