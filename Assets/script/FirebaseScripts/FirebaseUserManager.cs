using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FirebaseUserManager : MonoBehaviour
{
    public TMP_InputField userName; // Input field for user name
    public TMP_InputField profession; // Input field for profession
    public Button saveUserNameButton; // Button to save the username
    public Button saveProfessionButton; // Button to save the profession

    // Class to represent a user profile
    public class UserProfile
    {
        public string UserNameMessage; // Variable to store the user name
        public string UserProfessionMessage; // Variable to store the profession
    }

    public static UserProfile userProfile = new UserProfile(); // Static instance for global access

    void Start()
    {
        // Add listeners to the buttons' onClick events
        saveUserNameButton.onClick.AddListener(SaveUserName);
        saveProfessionButton.onClick.AddListener(SaveProfession);
    }

    // Function to save the username
    void SaveUserName()
    {
        userProfile.UserNameMessage = userName.text; // Save the username input
        Debug.Log("Username saved: " + userProfile.UserNameMessage); // Log the saved username
    }

    // Function to save the profession
    void SaveProfession()
    {
        userProfile.UserProfessionMessage = profession.text; // Save the profession input
        Debug.Log("Profession saved: " + userProfile.UserProfessionMessage); // Log the saved profession
    }
}
