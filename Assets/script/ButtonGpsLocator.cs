using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Required for scene management

public class ButtonGpsLocator : MonoBehaviour
{
    public Text GPSStatus;
    public Text LatitudeValue;
    public Text LongitudeValue;
    public Text AltitudeValue;
    public Text HorizontalAccuracy;
    public Text TimeStampValue;

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
            GPSStatus.text = "Updating...";
            LatitudeValue.text = Input.location.lastData.latitude.ToString();
            LongitudeValue.text = Input.location.lastData.longitude.ToString();
            AltitudeValue.text = Input.location.lastData.altitude.ToString();
            HorizontalAccuracy.text = Input.location.lastData.horizontalAccuracy.ToString();
            TimeStampValue.text = Input.location.lastData.timestamp.ToString();
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
