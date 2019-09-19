
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EndScreen : MonoBehaviour
{

    public PostProcessVolume postProcessVolume;

    public PostProcessProfile blurProfile;
    public PostProcessProfile normalProfile;

    // switch between the blurProfile and normalProfile
    public void EnableCameraBlur(bool state)
    {
        if (postProcessVolume != null && blurProfile != null && normalProfile != null)
        {
            postProcessVolume.profile = (state) ? blurProfile : normalProfile;
        }
    }
}
