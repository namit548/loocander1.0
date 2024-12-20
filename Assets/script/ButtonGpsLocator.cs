//this code is userd in getting loaction and doing calculation for user

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;


//public class UserLocation
//{
//    public string userName;
//    public float latitude;
//    public float longitude;
//    public float altitude;

//    public UserLocation(string name, float lat, float lon, float alt)
//    {
//        userName = name;
//        latitude = lat;
//        longitude = lon;
//        altitude = alt;
//    }
//}
//public class ButtonGpsLocator : MonoBehaviour
//{
//    public Text GPSStatus;
//    public Text LatitudeValue;
//    public Text LongitudeValue;
//    public Text AltitudeValue;
//    public Text HorizontalAccuracy;
//    public Text TimeStampValue;
//    public Text NearbyUsersText; // To display nearby users

//    private List<UserLocation> registeredUsers = new List<UserLocation>(); // List of registered users
//    private UserLocation currentUser; // Current user's location

//    void Start()
//    {
//        StartCoroutine(GPSLoc()); // Initialize GPS service at the start
//    }

//    // Get GPS location
//    IEnumerator GPSLoc()
//    {
//        if (!Input.location.isEnabledByUser)
//        {
//            GPSStatus.text = "GPS not enabled";
//            yield break;
//        }

//        Input.location.Start();
//        int maxWait = 20;
//        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
//        {
//            yield return new WaitForSeconds(1);
//            maxWait--;
//        }

//        if (maxWait < 1)
//        {
//            GPSStatus.text = "Time Out";
//            yield break;
//        }

//        if (Input.location.status == LocationServiceStatus.Failed)
//        {
//            GPSStatus.text = "Unable to determine device location";
//            yield break;
//        }
//        else
//        {
//            GPSStatus.text = "GPS Ready";
//        }
//    }

//    // Update current location when button is pressed
//    public void UpdateLocation()
//    {
//        StartCoroutine(UpdateLocationCoroutine());
//    }

//    IEnumerator UpdateLocationCoroutine()
//    {
//        if (Input.location.status != LocationServiceStatus.Running)
//        {
//            GPSStatus.text = "Restarting GPS...";
//            Input.location.Stop();
//            Input.location.Start();
//            int maxWait = 10;
//            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
//            {
//                yield return new WaitForSeconds(1);
//                maxWait--;
//            }

//            if (maxWait < 1)
//            {
//                GPSStatus.text = "Restart failed: Time Out";
//                yield break;
//            }
//        }

//        if (Input.location.status == LocationServiceStatus.Running)
//        {
//            GPSStatus.text = "Updating...";
//            LatitudeValue.text = Input.location.lastData.latitude.ToString();
//            LongitudeValue.text = Input.location.lastData.longitude.ToString();
//            AltitudeValue.text = Input.location.lastData.altitude.ToString();
//            HorizontalAccuracy.text = Input.location.lastData.horizontalAccuracy.ToString();
//            TimeStampValue.text = Input.location.lastData.timestamp.ToString();

//            // Register the current user's location
//            currentUser = new UserLocation("User" + (registeredUsers.Count + 1), Input.location.lastData.latitude, Input.location.lastData.longitude, Input.location.lastData.altitude);
//            registeredUsers.Add(currentUser);

//            GPSStatus.text = "Location Updated";
//        }
//        else
//        {
//            GPSStatus.text = "GPS not running or failed to update";
//        }
//    }

//    // Function to check for nearby users
//    public void FindNearbyUsers()
//    {
//        if (currentUser == null)
//        {
//            NearbyUsersText.text = "No current user set.";
//            return;
//        }

//        NearbyUsersText.text = "Nearby Users: \n";
//        float latitudeThreshold = 0.0009f;
//        float longitudeThreshold = 0.00103f;

//        foreach (UserLocation user in registeredUsers)
//        {
//            if (user != currentUser) // Skip the current user
//            {
//                float latDiff = Mathf.Abs(currentUser.latitude - user.latitude);
//                float lonDiff = Mathf.Abs(currentUser.longitude - user.longitude);

//                if (latDiff <= latitudeThreshold && lonDiff <= longitudeThreshold)
//                {
//                    NearbyUsersText.text += $"User Found: {user.userName}\n";
//                }
//            }
//        }
//    }

//    // Reload the current scene
//    public void ReloadScene()
//    {
//        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
//    }
//}

// Class to represent user location


















//this code works with basic loaction getting

//using System.Collections;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement; // Required for scene management

//public class ButtonGpsLocator : MonoBehaviour
//{
//    public Text GPSStatus;
//    public Text LatitudeValue;
//    public Text LongitudeValue;
//    public Text AltitudeValue;
//    public Text HorizontalAccuracy;
//    public Text TimeStampValue;

//    // Start is called before the first frame update
//    void Start()
//    {
//        StartCoroutine(GPSLoc()); // Initialize GPS service at the start
//    }

//    IEnumerator GPSLoc()
//    {
//        if (!Input.location.isEnabledByUser)
//        {
//            GPSStatus.text = "GPS not enabled"; // Inform user GPS is off
//            yield break;
//        }

//        Input.location.Start();
//        int maxWait = 20;
//        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
//        {
//            yield return new WaitForSeconds(1);
//            maxWait--;
//        }

//        if (maxWait < 1)
//        {
//            GPSStatus.text = "Time Out"; // Inform user of timeout
//            yield break;
//        }

//        if (Input.location.status == LocationServiceStatus.Failed)
//        {
//            GPSStatus.text = "Unable to determine device location"; // Inform user of failure
//            yield break;
//        }
//        else
//        {
//            GPSStatus.text = "GPS Ready"; // GPS is ready for updates
//        }
//    }

//    public void UpdateLocation()
//    {
//        StartCoroutine(UpdateLocationCoroutine());
//    }

//    IEnumerator UpdateLocationCoroutine()
//    {
//        // Restart GPS if it is not running
//        if (Input.location.status != LocationServiceStatus.Running)
//        {
//            GPSStatus.text = "Restarting GPS...";
//            Input.location.Stop(); // Stop the service to refresh it
//            Input.location.Start(); // Restart the service
//            int maxWait = 10;
//            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
//            {
//                yield return new WaitForSeconds(1);
//                maxWait--;
//            }

//            if (maxWait < 1)
//            {
//                GPSStatus.text = "Restart failed: Time Out";
//                yield break;
//            }
//        }

//        // Check if GPS is now running
//        if (Input.location.status == LocationServiceStatus.Running)
//        {
//            GPSStatus.text = "Updating...";
//            LatitudeValue.text = Input.location.lastData.latitude.ToString();
//            LongitudeValue.text = Input.location.lastData.longitude.ToString();
//            AltitudeValue.text = Input.location.lastData.altitude.ToString();
//            HorizontalAccuracy.text = Input.location.lastData.horizontalAccuracy.ToString();
//            TimeStampValue.text = Input.location.lastData.timestamp.ToString();
//            GPSStatus.text = "Location Updated";
//        }
//        else
//        {
//            GPSStatus.text = "GPS not running or failed to update";
//        }
//    }

//    // Reload the current scene
//    public void ReloadScene()
//    {
//        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
//    }
//}








using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering; // Required for scene management

public class ButtonGpsLocator : MonoBehaviour
{


    public Text GPSStatus;
    public Text LatitudeValue;
    public Text LongitudeValue;
   // public Text AltitudeValue;  
   // public Text HorizontalAccuracy;
   // public Text TimeStampValue;

    public static float Latitude;//changes for Firebase
    public static float Longitude;//changes for Firebase

   
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GPSLoc()); // Initialize GPS service at the start
    }

    IEnumerator GPSLoc()
    {
        if (!Input.location.isEnabledByUser)
        {
            GPSStatus.text = "GPS not enabled"; // Inform user GPS is off
            yield break;
        }

        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            GPSStatus.text = "Time Out"; // Inform user of timeout
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            GPSStatus.text = "Unable to determine device location"; // Inform user of failure
            yield break;
        }
        else
        {
            GPSStatus.text = "GPS Ready"; // GPS is ready for updates
        }
    }

    public void UpdateLocation()
    {
        StartCoroutine(UpdateLocationCoroutine());
    }

    IEnumerator UpdateLocationCoroutine()
    {
        // Restart GPS if it is not running
        if (Input.location.status != LocationServiceStatus.Running)
        {
            GPSStatus.text = "Restarting GPS...";
            Input.location.Stop(); // Stop the service to refresh it
            Input.location.Start(); // Restart the service
            int maxWait = 10;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            if (maxWait < 1)
            {
                GPSStatus.text = "Restart failed: Time Out";
                yield break;
            }
        }

        // Check if GPS is now running
        if (Input.location.status == LocationServiceStatus.Running)
        {
            // Store values in the float variables
            Latitude = Input.location.lastData.latitude;//firebase
            Longitude = Input.location.lastData.longitude;//firebase

            // Update the UI text

            GPSStatus.text = "Updating...";
            LatitudeValue.text = Input.location.lastData.latitude.ToString();
            LongitudeValue.text = Input.location.lastData.longitude.ToString();
            //AltitudeValue.text = Input.location.lastData.altitude.ToString();
            //HorizontalAccuracy.text = Input.location.lastData.horizontalAccuracy.ToString();
            //TimeStampValue.text = Input.location.lastData.timestamp.ToString();
            GPSStatus.text = "Location Updated";
        }
        else
        {
            GPSStatus.text = "GPS not running or failed to update";
        }
    }

    // Reload the current scene
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}





