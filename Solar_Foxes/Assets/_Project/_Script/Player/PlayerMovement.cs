using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public IngredientManager manager;
    
    public List<LineRenderer> allLines;

    private static List<Movement> allMovements;
    private static List<Vector2> movementNodes;

    public Collider2D frames;
    
    // Start is called before the first frame update
    private void Start()
    {
        RefreshTotalMovement();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(MoveSequence());
        }
    }

    public IEnumerator MoveSequence()
    {
        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < movementNodes.Count; i++)
        {
            sequence.Append(transform.DOMove(movementNodes[i], 0.5f));
        }
        
        yield return new DOTweenCYInstruction.WaitForCompletion(sequence);
        
        manager.ConsumeIngredient();
        ClearLine();
    }

    public void RefreshTotalMovement()
    {
        List<Movement> movements = new List<Movement>();

        for (int i = 0; i < IngredientManager.ChosenIngredients.Length; i++)
        {
            if (IngredientManager.ChosenIngredients[i] == null)
            {
                continue;
            }
            
            foreach (Movement move in IngredientManager.ChosenIngredients[i].movementQueue)
            {
                movements.Add(move);
            }
        }

        allMovements = movements;

        ConvertMovementsToNode();
    }

    void ConvertMovementsToNode()
    {
        List<Vector2> newMovementNodes = new List<Vector2>();
        newMovementNodes.Insert(0, transform.position);

        for (int i = 0; i < allMovements.Count; i++)
        {
            Movement move = allMovements[i];
            Vector2 moveDirection = Vector2.zero;
            switch (move.direction)
            {
                case Direction.North:
                    moveDirection = new Vector2(0, move.moves);
                    break;
                case Direction.South:
                    moveDirection = new Vector2(0, -move.moves);
                    break;
                case Direction.West:
                    moveDirection = new Vector2(-move.moves, 0);
                    break;
                case Direction.East:
                    moveDirection = new Vector2(move.moves, 0);
                    break;
            }

            Vector2 nodeToAdd = newMovementNodes[i] + moveDirection;
            if (!IsNodeValid(nodeToAdd))
            {
                movementNodes = new List<Vector2>();
                manager.ReleaseAllHolders();
                ClearLine();
                return;
            }
            newMovementNodes.Add(nodeToAdd);
        }
        movementNodes = newMovementNodes;

        DrawLine();
    }

    private bool IsNodeValid(Vector2 node)
    {
        return frames.bounds.Contains(node);
    }

    private void ClearLine()
    {
        foreach (var line in allLines)
        {
            line.positionCount = 0;
        }
    }
    
    private void DrawLine()
    {
        int startingNodeIndex = 0;
        for (int currentLine = 0; currentLine < allLines.Count; currentLine++)
        {
            if (IngredientManager.ChosenIngredients[currentLine] == null)
            {
                allLines[currentLine].positionCount = 0;
                continue;
            }

            int count = IngredientManager.ChosenIngredients[currentLine].movementQueue.Count;
            int endingNodeIndex = startingNodeIndex + count;
            List<Vector2> currentNodes = movementNodes.GetRange(startingNodeIndex, count + 1);
            allLines[currentLine].positionCount = currentNodes.Count;
            for (int i = 0; i < currentNodes.Count; i++)
            {
                allLines[currentLine].SetPosition(i, currentNodes[i]);
            }
            startingNodeIndex = endingNodeIndex;
        }
        
    }
}
