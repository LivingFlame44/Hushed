using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Progress;

public class JigsawPuzzleManager : MonoBehaviour
{
    public List<PuzzlePiece> puzzlePieces = new List<PuzzlePiece>();

    public PuzzlePiece[] correctOrder1, correctOrder2, correctOrder3;

    public PuzzleConnectorCollider[] puzzleConnectors;

    public 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ConnectorChecker() && (CheckOrder1() || CheckOrder2() || CheckOrder3()))
        {
            Debug.Log("Puzzle Finished");
        }
    }

    public bool ConnectorChecker()
    {
        for (int i = 0; i < puzzleConnectors.Length; i++)
        {
            if (puzzleConnectors[i].connected == false)
            {
                return false;
            }
        }

        return true;
    }

    public bool CheckOrder1()
    {
        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            if (puzzlePieces[i] != correctOrder1[i])
            {
                return false;
            }
        }

        return true;
    }

    public bool CheckOrder2()
    {
        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            if (puzzlePieces[i] != correctOrder2[i])
            {
                return false;
            }
        }

        return true;
    }

    public bool CheckOrder3()
    {
        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            if (puzzlePieces[i] != correctOrder3[i])
            {
                return false;
            }
        }

        return true;
    }

    public void UnselectPuzzles()
    {
        foreach (PuzzlePiece p in puzzlePieces)
        {
            p.selected = false;
        }
    }

    public void DecreaseLayer(PuzzlePiece piece)
    {
        //foreach(PuzzlePiece p in puzzlePieces)
        //{
        //    if(p == piece)
        //    {
        //        int i = puzzlePieces.FindIndex(x => x == p);

        //        int newIndex = Mathf.Clamp(i - 1, 0, 4);

        //        puzzlePieces.Remove(piece);
        //        puzzlePieces.Insert(newIndex, piece);

        //        UpdateOrderIndex(); 
        //    }
        //}

        for(int i = 0; i < puzzlePieces.Count; i++)
        {
            if (puzzlePieces[i] == piece)
            {
                int index = puzzlePieces.FindIndex(x => x == puzzlePieces[i]);

                int newIndex = Mathf.Clamp(index - 1, 0, 4);

                puzzlePieces.Remove(piece);
                puzzlePieces.Insert(newIndex, piece);

                UpdateOrderIndex();
                break;
            }
        }
    }

    public void IncreaseLayer(PuzzlePiece piece)
    {
        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            if (puzzlePieces[i] == piece)
            {
                int index = puzzlePieces.FindIndex(x => x == puzzlePieces[i]);

                int newIndex = index + 1;

                // But ensure we don't go out of bounds
                if (newIndex >= puzzlePieces.Count) newIndex = puzzlePieces.Count - 1;

                puzzlePieces.Remove(piece);
                puzzlePieces.Insert(newIndex, piece);

                UpdateOrderIndex();
                break;
            }
        }
    }

    public void UpdateOrderIndex()
    {
        foreach(PuzzlePiece p in puzzlePieces)
        {
            p.spriteRenderer.sortingOrder = puzzlePieces.FindIndex(x => x == p);
        }
    }
}
