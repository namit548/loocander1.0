using UnityEngine;
using Firebase.Database;
using Firebase;
using System;
using System.Collections;
using System.Collections.Generic; // For List support
using TMPro;

[Serializable]
public class dataToSave
{
    public string username;       // Store the username
    public string userprofession; // User's profession
    public float latitude;        // User's latitude
    public float longitude;       // User's longitude
}


public class FirebaseInitializer : MonoBehaviour
{
    public List<dataToSave> usersData = new List<dataToSave>(); //# COMMENT: Added List to hold all user data
    private DatabaseReference dbRef; // Reference to Firebase database

    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                dbRef = FirebaseDatabase.GetInstance(app, "https://loocander-98b72-default-rtdb.firebaseio.com/").RootReference;
                Debug.Log("Firebase Database initialized!");
            }
            else
            {
                Debug.LogError($"Could not resolve Firebase dependencies: {task.Result}");
            }
        });
    }

    // Function to save data to Firebase
    public void SaveDataFn()
    {
        string userId = FirebaseUserManager.userProfile.UserNameMessage; // Get the username
        dataToSave dts = new dataToSave //# COMMENT: Created a new instance of dataToSave
        {
            userprofession = FirebaseUserManager.userProfile.UserProfessionMessage, // Get the profession
            latitude = ButtonGpsLocator.Latitude, // Get latitude
            longitude = ButtonGpsLocator.Longitude // Get longitude
        };

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(dts.userprofession))
        {
            Debug.LogError("User data is missing! Ensure you have entered the data in input fields.");
            return;
        }

        // Convert to JSON and save in Firebase
        string json = JsonUtility.ToJson(dts);
        dbRef.Child("users").Child(userId).SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Data successfully saved to Firebase!");
            }
            else
            {
                Debug.LogError("Failed to save data to Firebase: " + task.Exception);
            }
        });
    }

    // Function to load ALL users' data into a List
    public void LoadDataFn()
    {
        StartCoroutine(LoadDataEnum());



        // Log all users after data is loaded
        foreach (var user in usersData)
        {
            Debug.Log($"User: Profession = {user.userprofession}, Latitude = {user.latitude}, Longitude = {user.longitude}");
        }




    }

    // Coroutine to load all users' data from Firebase
    IEnumerator LoadDataEnum()
    {
        var serverData = dbRef.Child("users").GetValueAsync();
        yield return new WaitUntil(predicate: () => serverData.IsCompleted);

        if (serverData.Exception != null)
        {
            Debug.LogError("Error loading data: " + serverData.Exception);
            yield break;
        }

        usersData.Clear(); // Clear old data

        DataSnapshot snapshot = serverData.Result;

        if (snapshot.Exists)
        {
            foreach (var userSnapshot in snapshot.Children)
            {
                string userId = userSnapshot.Key; // Get the key (username)
                string jsonData = userSnapshot.GetRawJsonValue();

                if (!string.IsNullOrEmpty(jsonData))
                {
                    dataToSave dts = JsonUtility.FromJson<dataToSave>(jsonData);

                    // Add the username to the data
                    dts.username = userId;

                    // Add the user's data to the list
                    usersData.Add(dts);
                }
            }

            Debug.Log($"Total users loaded: {usersData.Count}");
        }
        else
        {
            Debug.Log("No data exists in the 'users' node.");
        }
    }



}
















































//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using System;
//using Firebase.Database;
//using Unity.VisualScripting;
//using TMPro;
//using UnityEngine.SceneManagement;

//// Define a serializable class to structure the data you want to save to Firebase.
//[Serializable]
//public class dataToSave
//{
//    //LOngitude here   public string userName = FirebaseUserManager.userProfile.UserNameMessage;  // Store the user’s name.
//    public string userprofession = FirebaseUserManager.userProfile.UserProfessionMessage;   // Store the user's current level in the game.
//    //public int totalcoins;   // Store the user's total coins collected.
//   // public int highScore;    // Store the user's highest score.
//   // public TMP_Text txft;
//}

//// Main class responsible for saving and loading data to Firebase.
//public class FirebaseInitializer : MonoBehaviour
//{

//  public dataToSave dts;       // Instance of dataToSave to hold the current user data.
//    public string userId = FirebaseUserManager.userProfile.UserNameMessage;       ///// Unique identifier for each user.
//    /// </summary>
//    DatabaseReference dbRef;     // Reference to the Firebase Database.

//    // Initialize the database reference when the script is first loaded.
//    private void Awake()
//    {
//        dbRef = FirebaseDatabase.DefaultInstance.RootReference;  // Set the root reference to the Firebase database.
//    }

//    // Function to save user data to Firebase.
//    public void SaveDataFn()
//    {
//        string json = JsonUtility.ToJson(dts);  // Convert the dataToSave object to a JSON string.
//        dbRef.Child("users").Child(userId).SetRawJsonValueAsync(json);  // Save the JSON data under a unique user ID in the "users" child node.
//    }

//    // Function to load user data from Firebase.
//    public void LoadDataFn()
//    {
//        StartCoroutine(LoadDataEnum());  // Start the coroutine to load data asynchronously.
//    }

//    // Coroutine to retrieve data from Firebase asynchronously.
//    IEnumerator LoadDataEnum()
//    {
//        // Request the data for the specific user ID from Firebase.
//        var serverData = dbRef.Child("users").Child(userId).GetValueAsync();


//        // Wait until the data request is complete.
//        yield return new WaitUntil(predicate: () => serverData.IsCompleted);
//        print("process is complete");

//        // Get the result of the data request.
//        DataSnapshot snapshot = serverData.Result;

//        // Retrieve the JSON data as a string from the snapshot.
//        string jsonData = snapshot.GetRawJsonValue();

//        // Check if any data was found.
//        if (jsonData != null)
//        {
//            print("server data found");

//            // Convert the JSON string back to a dataToSave object and update the local data.
//            dts = JsonUtility.FromJson<dataToSave>(jsonData);
//        }
//        else
//        {
//            print("no data found");
//        }
//       // dts.txft.text = dts.userName;
//    }
















//    //////public Text GPSStatus2;
//    //////public Text LatitudeValue2;
//    //////public Text LongitudeValue2;
//    //////public Text AltitudeValue2;
//    //////public Text HorizontalAccuracy2;
//    //////public Text TimeStampValue2;

//    //////// Start is called before the first frame update
//    //////void Start()
//    //////{
//    //////    StartCoroutine(GPSLoc()); // Initialize GPS service at the start
//    //////}

//    //////IEnumerator GPSLoc()
//    //////{
//    //////    if (!Input.location.isEnabledByUser)
//    //////    {
//    //////        GPSStatus2.text = "GPS not enabled"; // Inform user GPS is off
//    //////        yield break;
//    //////    }

//    //////    Input.location.Start();
//    //////    int maxWait = 20;
//    //////    while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
//    //////    {
//    //////        yield return new WaitForSeconds(1);
//    //////        maxWait--;
//    //////    }

//    //////    if (maxWait < 1)
//    //////    {
//    //////        GPSStatus2.text = "Time Out"; // Inform user of timeout
//    //////        yield break;
//    //////    }

//    //////    if (Input.location.status == LocationServiceStatus.Failed)
//    //////    {
//    //////        GPSStatus2.text = "Unable to determine device location"; // Inform user of failure
//    //////        yield break;
//    //////    }
//    //////    else
//    //////    {
//    //////        GPSStatus2.text = "GPS Ready"; // GPS is ready for updates
//    //////    }
//    //////}

//    //////public void UpdateLocation2()
//    //////{
//    //////    StartCoroutine(UpdateLocationCoroutine());
//    //////}

//    //////IEnumerator UpdateLocationCoroutine()
//    //////{
//    //////    // Restart GPS if it is not running
//    //////    if (Input.location.status != LocationServiceStatus.Running)
//    //////    {
//    //////        GPSStatus2.text = "Restarting GPS...";
//    //////        Input.location.Stop(); // Stop the service to refresh it
//    //////        Input.location.Start(); // Restart the service
//    //////        int maxWait = 10;
//    //////        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
//    //////        {
//    //////            yield return new WaitForSeconds(1);
//    //////            maxWait--;
//    //////        }

//    //////        if (maxWait < 1)
//    //////        {
//    //////            GPSStatus2.text = "Restart failed: Time Out";
//    //////            yield break;
//    //////        }
//    //////    }

//    //////    // Check if GPS is now running
//    //////    if (Input.location.status == LocationServiceStatus.Running)
//    //////    {
//    //////        GPSStatus2.text = "Updating...";
//    //////        LatitudeValue2.text = Input.location.lastData.latitude.ToString();
//    //////        LongitudeValue2.text = Input.location.lastData.longitude.ToString();
//    //////        AltitudeValue2.text = Input.location.lastData.altitude.ToString();
//    //////        HorizontalAccuracy2.text = Input.location.lastData.horizontalAccuracy.ToString();
//    //////        TimeStampValue2.text = Input.location.lastData.timestamp.ToString();
//    //////        GPSStatus2.text = "Location Updated";
//    //////    }
//    //////    else
//    //////    {
//    //////        GPSStatus2.text = "GPS not running or failed to update";
//    //////    }
//    //////}

//    //////// Reload the current scene
//    //////public void ReloadScene2()
//    //////{
//    //////    SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
//    //////}







//}

