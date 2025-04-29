using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform pointC;
    [SerializeField] private float speedModifier;
    private float interpolateAmount;
    private int positionIndex;
    private bool isMoving;

    private void Start()
    {
        positionIndex = 1;
    }

    private void Update()
    {
        if (isMoving) return;

        if (Input.GetKeyDown(KeyCode.A) && positionIndex != 0)
        {
            if (positionIndex == 2)
                StartCoroutine(Movement(pointC, pointB));
            else
                StartCoroutine(Movement(pointB, pointA));

            positionIndex--;
        }

        if (Input.GetKeyDown(KeyCode.D) && positionIndex != 2)
        {
            if (positionIndex == 0)
                StartCoroutine(Movement(pointA, pointB));
            else
                StartCoroutine(Movement(pointB, pointC));

            positionIndex++;
        }
    }

    private IEnumerator Movement(Transform startingPoint, Transform finalPoint)
    {
        isMoving = true;

        while (interpolateAmount < 1f)
        {
            interpolateAmount += Time.deltaTime * speedModifier;
            transform.position = Vector3.Lerp(startingPoint.position, finalPoint.position, interpolateAmount);

            yield return new WaitForEndOfFrame();
        }

        interpolateAmount = 0f;
        isMoving = false;
    }
}
