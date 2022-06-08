using DG.Tweening;
using System.Linq;
using UnityEngine;

public class Playfield : MonoBehaviour
{
    public Tile[] tiles;
    public static Tile? selectedTile { get; set; }
    public static Audio audioController;

    private void Start()
    {
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<Audio>();
    }

    public static void MoveClickSound()
    {
        audioController.soundSource.clip = audioController.soundMove;
        audioController.soundSource.Play();
    }
    public static void ClickSound()
    {
        audioController.soundSource.clip = audioController.soundClick;
        audioController.soundSource.Play();
    }
    public static void RightPositionSound()
    {
        audioController.soundSource.clip = audioController.soundWin;
        audioController.soundSource.Play();
    }

    public static void SwapTiles(Tile selectedTile, Tile currentTile)
    {
        var tempPosition = new Vector2(selectedTile.transform.position.x, selectedTile.transform.position.y);
        var tempTile = new Tile(selectedTile.X, selectedTile.Y, false, selectedTile.originalX, selectedTile.originalY);

        selectedTile.transform.DOMove(new Vector3(currentTile.transform.position.x, currentTile.transform.position.y), 0.5f);
        selectedTile.X = currentTile.X;
        selectedTile.Y = currentTile.Y;

        currentTile.transform.DOMove(tempPosition, 0.5f);
        currentTile.X = tempTile.X;
        currentTile.Y = tempTile.Y;
    }

    public static void SwapTilesSETUPlevel(Tile selectedTile, Tile currentTile)
    {
        var tempPosition = new Vector2(selectedTile.transform.position.x, selectedTile.transform.position.y);
        var tempTile = new Tile(selectedTile.X, selectedTile.Y, false, selectedTile.originalX, selectedTile.originalY);

        selectedTile.transform.position = currentTile.transform.position;
        selectedTile.X = currentTile.X;
        selectedTile.Y = currentTile.Y;

        currentTile.transform.position = tempPosition;
        currentTile.X = tempTile.X;
        currentTile.Y = tempTile.Y;
    }

    public void Shuffle() 
    {
        var fixedTiles = GameController.levels[GameController.currentLevel].fix;

        while (fixedTiles > 0) //fill fixed tiles
        {
            var randTile = Random.Range(0, tiles.Length);
            if (tiles[randTile].fix) continue;

            tiles[randTile].fix = true;
            tiles[randTile].fixImage.SetActive(true);
            fixedTiles--;
        }

        var notFixedTiles = tiles.Where(x => x.fix != true).ToArray(); //choose notFixed tiles

        var moveCount = GameController.levels[GameController.currentLevel].moveCount;

        for (var i = 0; i < moveCount; i++)
        {
            if (Random.value > 0.5 && notFixedTiles.Length > 2) // swap tiles
            {
                Debug.Log("swap");
                var t1 = notFixedTiles[Random.Range(0, notFixedTiles.Length)];
                var t2 = notFixedTiles[Random.Range(0, notFixedTiles.Length)];
                if (t1 == t2)
                {
                    moveCount++;
                    continue;
                }
                SwapTilesSETUPlevel(t1, t2);
            }
            else // rotate tiles
            {
                var t3 = notFixedTiles[Random.Range(0, notFixedTiles.Length)];
                t3.transform.rotation = new Quaternion(0, 0, 90 * Random.Range(0, 4), 0);
            }
        }

        if (isFinished()) // if field not change
        {
            foreach (var tile in tiles)
                tile.SetUnFix();
            Shuffle();
        }          
    }

    public bool isFinished()
    {        
        return tiles.All(t => t.X == t.originalX && t.Y == t.originalY && t.transform.eulerAngles.z % 360 > -0.5 && t.transform.eulerAngles.z % 360 < 0.5);
    }
}
