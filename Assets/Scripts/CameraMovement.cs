using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject playerOne;
    public GameObject playerTwo;
    private float maxVerticalOffset = 3f;
    private float minVerticalOffset = 2f;
    private float verticalOffset;
    private float maxVerticalDistance = 20f;
    private float minVerticalDistance = 0f;

    // Update is called once per frame
    void Update()
    {
        verticalOffset = NormalizeVerticalBetweenPlayerDistance(DistanceVertical());
        //Debug.Log(verticalOffset);
        transform.position = new Vector3(0, AverageVertical() + verticalOffset, -10);
    }

    private float AverageVertical()
    {
        float heightSum = playerOne.transform.position.y + playerTwo.transform.position.y;
        float averageVertical = heightSum / 2;
        return averageVertical;
    }

    private float DistanceVertical()
    {
        float heightDifference = Mathf.Abs(playerOne.transform.position.y - playerTwo.transform.position.y);
        return heightDifference;
    }

    private float NormalizeVerticalBetweenPlayerDistance(float heightDifference)
    {
        float normalizedPosition = (maxVerticalOffset - minVerticalOffset) * ((heightDifference - minVerticalDistance) / (maxVerticalDistance - minVerticalDistance)) + minVerticalOffset;
        return normalizedPosition;
    }
}
