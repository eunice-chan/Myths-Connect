using UnityEngine;
using System.Collections;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    // Latitude
    public float x;
    // Lplaongitude
    public float y;
    IEnumerator Start()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            #if PLATFORM_ANDROID
            Permission.RequestUserPermission(Permission.FineLocation);
            #endif
            yield break;
        }

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
            print("Location services timed out.");
			yield break;
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			print("Unable to determine device location.");
			yield break;
		}
	}

    void FixedUpdate()
    {
        // TODO: Re-enable once testing done
        //x = Input.location.lastData.latitude;
        //y = Input.location.lastData.longitude;
        // Target coordinates
        Vector3 target = new Vector3(x, 0, y);
        // Move player to the target coordinates
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
