using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms;
using System;

public class DistanceCalculator : MonoBehaviour
{
    public string UserNameFMatch; // Save input data in string
    public float maxDistanceMeters = 1000f; // Threshold for distance comparison
    public TextMeshProUGUI resultsText; // Text element to display results

    // Reference to FirebaseInitializer
    public FirebaseInitializer firebaseInitializer;

    public void GetDataForMatch()
    {
        // Get the username and profession of the current user
        UserNameFMatch = FirebaseUserManager.userProfile.UserProfessionMessage;
        string userId = FirebaseUserManager.userProfile.UserNameMessage;

        if (string.IsNullOrEmpty(userId) || firebaseInitializer.usersData == null || firebaseInitializer.usersData.Count == 0)// userId is empty(user not logged in).
                                                                                                                              //usersData is null(Firebase data not loaded).
                                                                                                                              //usersData.Count == 0(no users exist in the database).
        {
            Debug.LogError("No data available or user details missing.");
            resultsText.text = "No data found. Please load data first!";
            return; //Stops further execution using return
        }

        // List to store nearby users
        List<string> nearbyUsers = new List<string>();

        // Get the current user's latitude and longitude
      //  dataToSave currentUser = firebaseInitializer.usersData.Find(user => user.username == userId);/////////////

        dataToSave currentUser = null;

        foreach (dataToSave user in firebaseInitializer.usersData)//temparary call data of datatosave user
        {
            if (user.username == userId)
            {
                currentUser = user;
                break; // Exit the loop once the user is found
            }
        }


        if (currentUser == null)
        {
            Debug.LogError("Current user not found in the database.");
            resultsText.text = "Current user not found.";
            return;
        }

        // Iterate through all users
        foreach (var user in firebaseInitializer.usersData)
        {
            if (user.username == userId)
                continue; // Skip current user

            // Calculate the distance
            float distance = CalculateDistance(currentUser.latitude, currentUser.longitude, user.latitude, user.longitude);

            // If within the threshold, add to the list
            if (distance <= maxDistanceMeters)//distance = distance<maxdsitance
            {
                nearbyUsers.Add($"{user.username} ({user.userprofession}) - {distance:F2} meters away");
            }
        }

        // Display the results
        if (nearbyUsers.Count > 0)
        {
            resultsText.text = "Nearby Users:\n" + string.Join("\n", nearbyUsers);
        }
        else
        {
            resultsText.text = "No nearby users found.";
        }
    }

    // Haversine formula to calculate the distance between two coordinates
    private float CalculateDistance(float lat1, float lon1, float lat2, float lon2)
    {
        float R = 6371000; // Radius of Earth in meters
        float dLat = Mathf.Deg2Rad * (lat2 - lat1);
        float dLon = Mathf.Deg2Rad * (lon2 - lon1);
        float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
                  Mathf.Cos(Mathf.Deg2Rad * lat1) * Mathf.Cos(Mathf.Deg2Rad * lat2) *
                  Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        return R * c; // Distance in meters
    }
}
