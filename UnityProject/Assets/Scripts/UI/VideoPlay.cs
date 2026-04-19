using UnityEngine;
using UnityEngine.Video;

public class VideoPlay : MonoBehaviour
{
	private VideoPlayer player;

	private void Awake()
	{
		player = GetComponent<VideoPlayer>();
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		string path = Application.streamingAssetsPath + "/video.mp4";

		player.source = VideoSource.Url;
		player.url = path;
		player.isLooping = true;

		player.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
