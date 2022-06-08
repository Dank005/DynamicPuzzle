using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public RawImage rawImageTile;
    public int X;
    public int Y;

    public bool fix;
    public GameObject fixImage;

    public int originalX;
    public int originalY;

    public Playfield playfield;


    public Tile(int X, int Y, bool fix, int originalX, int originalY)
    {
        this.X = X;
        this.Y = Y;

        this.fix = fix;
        if (fix)
            fixImage.SetActive(true);

        this.originalX = originalX;
        this.originalY = originalY;
    }

    public void SetFix()
    {
        fix = true;
        fixImage.SetActive(true);
    }

    public void SetUnFix()
    {
        fix = false;
        fixImage.SetActive(false);
    }

    public void TileClick()
    {
        var currentTile = Playfield.selectedTile;

        Playfield.ClickSound(); // проигрывание звука нажатия
        if (currentTile != null) // проверка наличия активного пазла
        {
            
            if (currentTile == this && !currentTile.fix)
            {             
                if (DOTween.IsTweening(transform)) return;
                transform.DORotate(transform.eulerAngles + new Vector3(0, 0, 90), 0.3f);
                RestartUnselectTimeout(); // сбросить активность пазла
                InRightPlaceAfterRotate();
            }
            else if (!currentTile.fix && !this.fix) // при втором нажатии на другой пазл при условии, что оба не фиксированы - перестанока
            {
                if (DOTween.IsTweening(transform)) return;
                Playfield.SwapTiles(currentTile, this);
                Playfield.MoveClickSound(); // проигрывание звука перестановки

                InRightPlaceAfterReplace(currentTile, this);

                Playfield.selectedTile = null; 
            }
            if (currentTile.fix) return;
            StartCoroutine(WaitRotation());
        }
        else // присвоение активного пазла при первом нажатии
        {
            if (this.fix) return;
            Playfield.selectedTile = this;
            RestartUnselectTimeout();
        }
    }

    void InRightPlaceAfterRotate()
    {
        var nextRotate = transform.eulerAngles.z + 90; // из за анимации
        if (X == originalX && Y == originalY && nextRotate % 360 == 0)
        {
            Playfield.RightPositionSound();
            fix = true;
        }
    }

    void InRightPlaceAfterReplace(Tile currentTile, Tile thisTile)
    {
        if (currentTile.X == currentTile.originalX && currentTile.Y == currentTile.originalY && currentTile.transform.eulerAngles.z % 360 == 0)
        {
            Playfield.RightPositionSound();
            currentTile.fix = true;
        }
        if (thisTile.X == thisTile.originalX && thisTile.Y == thisTile.originalY && thisTile.transform.eulerAngles.z % 360 == 0)
        {
            Playfield.RightPositionSound();
            thisTile.fix = true;
        }
    }

    private void FixedUpdate() // не оптимально
    {
        if (!DOTween.IsTweening(transform))
        {
            if (playfield.isFinished())
            {
                Playfield.selectedTile = null;
                GameController.gameIsFinished = true; // изменение состояния игры
            }
        }
    }

    void RestartUnselectTimeout()
    {
        StopAllCoroutines();
        StartCoroutine(SelectedTimeout());
    }


    IEnumerator SelectedTimeout()
    {
        rawImageTile.color = Color.gray;

        yield return new WaitForSeconds(1.5f);

        rawImageTile.color = Color.white;
        Playfield.selectedTile = null;
    }

    IEnumerator WaitRotation()
    {
        yield return new WaitForSeconds(1);
        if (playfield.isFinished() && GameController.gameIsFinished == false) // проверка на правильность сборки
        {
            Playfield.selectedTile = null;
            GameController.gameIsFinished = true; // изменение состояния игры
        }
    }
}
