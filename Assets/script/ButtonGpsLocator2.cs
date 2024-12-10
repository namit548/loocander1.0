using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Required for scene management

public class ButtonGpsLocator2 : MonoBehaviour
{
    public Text GPSStatus2;
    public Text LatitudeValue2;
    public Text LongitudeValue2;
    public Text AltitudeValue2;
    public Text HorizontalAccuracy2;
    public Text TimeStampValue2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GPSLoc()); // Initialize GPS service at the start
    }

    IEnumerator GPSLoc()
    {
        if (!Input.location.isEnabledByUser)
        {
            GPSStatus2.text = "GPS not enabled"; // Inform user GPS is off
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
            GPSStatus2.text = "Time Out"; // Inform user of timeout
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            GPSStatus2.text = "Unable to determine device location"; // Inform user of failure
            yield break;
        }
        else
        {
            GPSStatus2.text = "GPS Ready"; // GPS is ready for updates
        }
    }

    public void UpdateLocation2()
    {
        StartCoroutine(UpdateLocationCoroutine());
    }

    IEnumerator UpdateLocationCoroutine()
    {
        // Restart GPS if it is not running
        if (Input.location.status != LocationServiceStatus.Running)
        {
            GPSStatus2.text = "Restarting GPS...";
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
                GPSStatus2.text = "Restart failed: Time Out";
                yield break;
            }
        }

        // Check if GPS is now running
        if (Input.location.status == LocationServiceStatus.Running)
        {
            GPSStatus2.text = "Updating...";
            LatitudeValue2.text = Input.location.lastData.latitude.ToString();
            LongitudeValue2.text = Input.location.lastData.longitude.ToString();
            AltitudeValue2.text = Input.location.lastData.altitude.ToString();
            HorizontalAccuracy2.text = Input.location.lastData.horizontalAccuracy.ToString();
            TimeStampValue2.text = Input.location.lastData.timestamp.ToString();
            GPSStatus2.text = "Location Updated";
        }
        else
        {
            GPSStatus2.text = "GPS not running or failed to update";
        }
    }

    // Reload the current scene
    public void ReloadScene2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}
