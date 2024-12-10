//using System.Collections;
//using System.Collections.Generic;
//using System.Runtime.CompilerServices;
//using UnityEngine;
//using UnityEngine.UI;

//public class GpsLocator : MonoBehaviour
//{
//    public Text GPSStatus;
//    public Text LatitudeValue;
//    public Text LongitudeValue;
//    public Text altitudeValue;
//    public Text horizontalAccuracy;
//    public Text TimeStampValue;


//    // Start is called before the first frame update
//    void Start()
//    {
//        StartCoroutine(GPSLoc());

//    }


//    IEnumerator GPSLoc()
//    {
//        if (!Input.location.isEnabledByUser)
//            yield break;
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
//            GPSStatus.text = "Unable to determine device Location";
//            yield break;
//        }
//        else
//        {
//            GPSStatus.text = "Running";
//            InvokeRepeating("UpdateGPSData", 0.5f, 1f);
//        }
//    }
//    private void UpdateGPSData()
//    {
//        if (Input.location.status == LocationServiceStatus.Running)
//        {
//            GPSStatus.text = "Running";
//            LatitudeValue.text = Input.location.lastData.latitude.ToString();
//            LongitudeValue.text = Input.location.lastData.longitude.ToString(); 
//            altitudeValue.text = Input.location.lastData.altitude.ToString();
//            horizontalAccuracy.text = Input.location.lastData.horizontalAccuracy.ToString();
//            TimeStampValue.text = Input.location.lastData.timestamp.ToString();
//        }
//        else
//        {
//            GPSStatus.text = "Stop";
//        }
//    }
//}






using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GpsLocator : MonoBehaviour
{
    public Text GPSStatus;
    public Text LatitudeValue;
    public Text LongitudeValue;
    public Text altitudeValue;
    public Text horizontalAccuracy;
    public Text TimeStampValue;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GPSLoc());

    }


    IEnumerator GPSLoc()
    {
        if (!Input.location.isEnabledByUser)
            yield break;
        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;

        }
        if (maxWait < 1)
        {
            GPSStatus.text = "Time Out";
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            GPSStatus.text = "Unable to determine device Location";
            yield break;
        }
        else
        {
            GPSStatus.text = "Running";
            InvokeRepeating("UpdateGPSData", 0.5f, 1f);
        }
    }
    private void UpdateGPSData()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            GPSStatus.text = "Running";
            LatitudeValue.text = Input.location.lastData.latitude.ToString();
            LongitudeValue.text = Input.location.lastData.longitude.ToString();
            altitudeValue.text = Input.location.lastData.altitude.ToString();
            horizontalAccuracy.text = Input.location.lastData.horizontalAccuracy.ToString();
            TimeStampValue.text = Input.location.lastData.timestamp.ToString();
        }
        else
        {
            GPSStatus.text = "Stop";
        }
    }
}












