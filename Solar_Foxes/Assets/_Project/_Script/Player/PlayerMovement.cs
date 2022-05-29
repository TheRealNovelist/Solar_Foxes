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

    public GameObject consumeButton;
    public GameObject tryAgainWarning;
    public GameObject spinningWheel;
    public AudioSource fx;
    public void StartMoveSequence()
    {
        StartCoroutine(MoveSequence());
    }
    
    private IEnumerator MoveSequence()
    {
        fx.Play();
        Sequence pathSequence = DOTween.Sequence();
        Sequence sequence = DOTween.Sequence();
        consumeButton.SetActive(false);
        manager.SetAllowHolderClick(false);

        float totalTime = 0f;
        for (int i = 0; i < movementNodes.Count; i++)
        {
            pathSequence.Append(transform.DOMove(movementNodes[i], 0.5f));
            totalTime += 0.5f;
        }
        sequence.Prepend(spinningWheel.transform.DORotate(new Vector3(0, 0, -360), totalTime, RotateMode.FastBeyond360));
        sequence.Join(pathSequence);
        
        yield return new DOTweenCYInstruction.WaitForCompletion(sequence);

        spinningWheel.transform.rotation = Quaternion.identity;
        
        manager.ConsumeIngredient();
        manager.SetAllowHolderClick(true);
        ClearLine();
    }

    public void RefreshTotalMovement()
    {
        consumeButton.SetActive(IngredientManager.ChosenIngredients.Length > 0);
        
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
                Restart();
                StartCoroutine(TryAgain());
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

    public void Restart()
    {
        manager.ReleaseAllHolders();
        movementNodes = new List<Vector2>();
        ClearLine();
    }
    
    private void ClearLine()
    {
        foreach (var line in allLines)
        {
            line.positionCount = 0;
        }
    }

    private IEnumerator TryAgain()
    {
        tryAgainWarning.SetActive(true);

        yield return new WaitForSeconds(3f);
        
        tryAgainWarning.SetActive(false);
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
