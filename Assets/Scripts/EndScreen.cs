
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EndScreen : MonoBehaviour
{

    public PostProcessProfile blurProfile;
    public PostProcessProfile normalProfile;
    public PostProcessVolume postProcessVolume;

    // switch between the blurProfile and normalProfile
    public void EnableCameraBlur(bool state)
    {
        if (postProcessVolume != null && blurProfile != null && normalProfile != null)
        {
            postProcessVolume.profile = (state) ? blurProfile : normalProfile;
        }
    }
}
