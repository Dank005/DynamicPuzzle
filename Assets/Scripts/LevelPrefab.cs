using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Random = UnityEngine.Random;

public class LevelPrefab : MonoBehaviour
{
    public Playfield playfield;
    public VideoPlayer videoPlayer;
    public GameController gameController;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        Fill();
    }


    void Fill()
    {
        videoPlayer = gameObject.GetComponent<VideoPlayer>();

        var rnd = Random.Range(0, gameController.videos.Count);
        videoPlayer.clip = gameController.videos[rnd];
        videoPlayer.targetTexture = gameController.textures[rnd];

        foreach (var tile in playfield.tiles)
        {
            tile.rawImageTile.texture = gameController.textures[rnd];
        }
        
        gameController.videos.RemoveAt(rnd);
    }
}
