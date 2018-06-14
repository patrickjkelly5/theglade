using UnityEngine;
using VRTK;

public class OculusHapticsController : MonoBehaviour
{
	[SerializeField]
	OVRInput.Controller controllerMask;
    public GameObject steamController;

	private OVRHapticsClip clipLight;
	private OVRHapticsClip clipMedium;
	private OVRHapticsClip clipHard;

    public PlayerHealth player;
    bool hapticsInitialized;

    void Update()
    {

        // CHECK SDK TYPE
        if (player.SDKType == "oculus")
        {
            transform.localPosition = OVRInput.GetLocalControllerPosition(controllerMask);
            transform.localRotation = OVRInput.GetLocalControllerRotation(controllerMask);
        }
        else
        {
            if (VRTK_DeviceFinder.GetActualController(steamController))
            {
                transform.position = VRTK_DeviceFinder.GetActualController(steamController).transform.position;
                transform.rotation = VRTK_DeviceFinder.GetActualController(steamController).transform.rotation;
                transform.Rotate(60, 0, 0);
            }
        }

    }

    // This function is called from RightControllerGrabGun
   public void InitializeOVRHaptics()
	{
		int cnt = 10;
		clipLight = new OVRHapticsClip(cnt);
		clipMedium = new OVRHapticsClip(cnt);
		clipHard = new OVRHapticsClip(cnt);
		for (int i = 0; i < cnt; i++)
		{
			clipLight.Samples[i] = i % 2 == 0 ? (byte)0 : (byte)40;
			clipMedium.Samples[i] = i % 2 == 0 ? (byte)0 : (byte)150;
			clipHard.Samples[i] = i % 2 == 0 ? (byte)0 : (byte)255;
		}

		clipLight = new OVRHapticsClip(clipLight.Samples, clipLight.Samples.Length);
		clipMedium = new OVRHapticsClip(clipMedium.Samples, clipMedium.Samples.Length);
		clipHard = new OVRHapticsClip(clipHard.Samples, clipHard.Samples.Length);
	}

	public void Vibrate(VibrationForce vibrationForce)
	{
		var channel = OVRHaptics.RightChannel;
		if (controllerMask == OVRInput.Controller.LTouch)
			channel = OVRHaptics.LeftChannel;

		switch (vibrationForce)
		{
			case VibrationForce.Light:
				channel.Preempt(clipLight);
				break;
			case VibrationForce.Medium:
				channel.Preempt(clipMedium);
				break;
			case VibrationForce.Hard:
				channel.Preempt(clipHard);
				break;
		}
	}
}