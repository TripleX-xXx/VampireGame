using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{

    public int distanceToSeePlayer = 5;
    public int distanceToAttack = 1;
    public float cellSize = 1f;

    private Enemy enemy;
    private Vector3 lastPlayerPosition;

    public IA(Enemy enemy)
    {
        this.enemy = enemy;
        lastPlayerPosition = enemy.transform.position;
    }

    // if can see player return his position if not return enemy position
    private Vector3 CanSeePlayer(Vector3 enemyPosition)
    {
        Vector3 target = enemy.transform.position;
        Collider[] seeObjects = Physics.OverlapSphere(enemyPosition, distanceToSeePlayer*cellSize);
        foreach(Collider x in seeObjects)
        {
            if (x.tag == "Player") target = x.transform.position;
        }
        return target;
    }

    // return true if enemy have clear route to player else false (eg. wall between enemy and player)
    private bool CanGoToPlayer(Vector3 enemyPosition, Vector3 playerPosition)
    {
        RaycastHit hit;
        if (Physics.Raycast(enemyPosition, playerPosition, out hit))
        {
            if (hit.collider.tag == "Player") return true;
        }
        return false;
    }

    // returns the page in which enemy should go
    private MG_Sides.Side WhereGo(Vector3 enemyPosition, Vector3 playerPosition)
    {
        MG_Sides.Side where = MG_Sides.Side.none;
        float x = (playerPosition-enemyPosition).x/cellSize;
        float y = (playerPosition - enemyPosition).y/cellSize;

        if (Mathf.Abs(x) <= Mathf.Abs(y) && Mathf.Abs(x)> 1.5)
        {
            if (x > 0)
            {
                if (enemy.GetComponent<Moving>().GetFreeSides()[3]) where = MG_Sides.Side.right;
                else if (y > 0)
                {
                    if (enemy.GetComponent<Moving>().GetFreeSides()[0]) where = MG_Sides.Side.up;
                }
                else
                {
                    if (enemy.GetComponent<Moving>().GetFreeSides()[1]) where = MG_Sides.Side.down;
                }
            }
            else
            {
                if (enemy.GetComponent<Moving>().GetFreeSides()[2]) return MG_Sides.Side.left;
                else if (y > 0)
                {
                    if (enemy.GetComponent<Moving>().GetFreeSides()[0]) where = MG_Sides.Side.up;
                }
                else
                {
                    if (enemy.GetComponent<Moving>().GetFreeSides()[1]) where = MG_Sides.Side.down;
                }
            }
        }
        else if (Mathf.Abs(y) <= Mathf.Abs(x) && Mathf.Abs(y) > 1.5)
        {
            if (y > 0)
            {
                if (enemy.GetComponent<Moving>().GetFreeSides()[0]) where = MG_Sides.Side.up;
                else if (x > 0)
                {
                    if (enemy.GetComponent<Moving>().GetFreeSides()[3]) where = MG_Sides.Side.right;
                }
                else
                {
                    if (enemy.GetComponent<Moving>().GetFreeSides()[2]) return MG_Sides.Side.left;
                }
            }
            else
            {
                if (enemy.GetComponent<Moving>().GetFreeSides()[1]) where = MG_Sides.Side.down;
                else if (x > 0)
                {
                    if (enemy.GetComponent<Moving>().GetFreeSides()[3]) where = MG_Sides.Side.right;
                }
                else
                {
                    if (enemy.GetComponent<Moving>().GetFreeSides()[2]) return MG_Sides.Side.left;
                }
            }
        }

        return where;
    }


    public void WhatToDo()
    {
        Vector3 player = CanSeePlayer(enemy.transform.position);
        Vector3 enemyPos = enemy.transform.position;
        if (player != enemy.transform.position)
        {
            if (CanGoToPlayer(enemyPos, player)) lastPlayerPosition = player;
        }

        Vector3 distance = lastPlayerPosition - enemyPos;
        MG_Sides.Side side = WhereGo(enemyPos, lastPlayerPosition);
        if ((Mathf.Abs(distance.x)<=distanceToAttack || Mathf.Abs(distance.y) <= distanceToAttack) && lastPlayerPosition != enemyPos)
        {
            if(enemyPos.x == lastPlayerPosition.x || enemyPos.y == lastPlayerPosition.y)
            {
                //attack || (rotate && attack)
                RotateToPlayer();
                AttacksList.EnemyAttack1(enemy);
            }
            else
            {
                enemy.GetComponent<Moving>().Move(side);
            }
        }
        else if (side != MG_Sides.Side.none)
        {
            enemy.GetComponent<Moving>().Move(side);
        }

    }

    public void RotateToPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(enemy.transform.position - lastPlayerPosition, enemy.transform.TransformDirection(Vector3.forward));
        enemy.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
    }

}
