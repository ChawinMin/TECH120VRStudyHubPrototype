using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public float skipTime = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
            }
            else
            {
                videoPlayer.Play();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (videoPlayer.isPlaying)
            {
                double newTime = videoPlayer.time + skipTime;
                videoPlayer.time = newTime < videoPlayer.length ? newTime : videoPlayer.length;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (videoPlayer.isPlaying)
            {
                double newTime = videoPlayer.time + skipTime;
                videoPlayer.time = newTime < videoPlayer.length ? newTime : videoPlayer.length;
            }
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            SceneManager.LoadScene("Scenes/Main");
        }
    }
}
