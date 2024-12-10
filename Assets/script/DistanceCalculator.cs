using UnityEngine;

public class DistanceCalculator : MonoBehaviour
{
    private Vector3 user1Location; // Stores User 1's latitude, longitude, and altitude
    private Vector3 user2Location; // Stores User 2's latitude, longitude, and altitude

    // Button 1: Capture User 1 Location
    public void GetUser1Location()
    {
        user1Location = GetLocation();
        Debug.Log("User 1 Location: " + user1Location);
    }

    // Button 2: Capture User 2 Location
    public void GetUser2Location()
    {
        user2Location = GetLocation();
        Debug.Log("User 2 Location: " + user2Location);

        // Compare locations once both are available
        CheckProximity();
    }

    // Function to simulate getting the current location (replace with actual GPS logic)
    private Vector3 GetLocation()
    {
        return new Vector3(
            Input.location.lastData.latitude,
            Input.location.lastData.longitude,
            Input.location.lastData.altitude
        );
    }

    // Function to check if users are within 100 meters of each other
    private void CheckProximity()
    {
        // Calculate differences
        float latitudeDiff = Mathf.Abs(user1Location.x - user2Location.x);
        float longitudeDiff = Mathf.Abs(user1Location.y - user2Location.y);

        // Threshold values for 100 meters
        float latitudeThreshold = 0.0009f;
        float longitudeThreshold = 0.00103f;

        // Check proximity
        if (latitudeDiff <= latitudeThreshold && longitudeDiff <= longitudeThreshold)
        {
            Debug.Log("User Found: Both users are within 100 meters.");
        }
        else
        {
            Debug.Log("User Not Found: Users are more than 100 meters apart.");
        }
    }
}
