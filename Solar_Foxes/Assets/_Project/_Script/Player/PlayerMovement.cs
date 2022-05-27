using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public LineRenderer line;
    public Vector2 originPoint;
    
    public List<Ingredient> chosenIngredients;
    public List<Movement> allMovements;
    public List<Vector2> movementNodes;
    
    // Start is called before the first frame update
    private void Start()
    {
        movementNodes.Insert(0, originPoint);
        RefreshTotalMovement();
    }
    
    // Update is called once per frame
    void Update()
    {
        DrawLine();
        ConvertMovementsToNode();
    }
    
    void RefreshTotalMovement()
    {
        List<Movement> movements = new List<Movement>();
        
        for (int i = 0; i < chosenIngredients.Count; i++)
        {
            foreach (Movement move in chosenIngredients[i].movementQueue)
            {
                movements.Add(move);
            }
        }

        allMovements = movements;
    }

    void ConvertMovementsToNode()
    {
        List<Vector2> newMovementNodes = new List<Vector2>();
        newMovementNodes.Insert(0, Vector2.zero);

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

            newMovementNodes.Add(newMovementNodes[i] + moveDirection);
        }

        movementNodes = newMovementNodes;
    }

    private void DrawLine()
    {
        line.positionCount = movementNodes.Count;
        
        for (int i = 0; i < movementNodes.Count; i++)
        {
            line.SetPosition(i, movementNodes[i]);
        }
    }
    
    
    // private void OnDrawGizmos()
    // {
    //     for (int i = 0; i < movementNodes.Count; i++)
    //     {
    //         if (i + 1 < movementNodes.Count)
    //         {
    //             Debug.DrawLine(movementNodes[i], movementNodes[i + 1], Color.white);
    //         }
    //     }
    // }
}
