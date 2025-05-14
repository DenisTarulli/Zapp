using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float interpolationSpeed;
    [SerializeField] private float moveSpeed;
    private float interpolateAmount;
    private int positionIndex;
    private bool isMoving;

    private void Start()
    {
        positionIndex = 1;
    }

    private void Update()
    {
        ForwardMovement();

        if (isMoving) return;

        if (Input.GetKeyDown(KeyCode.A) && positionIndex != 0)
        {
            if (positionIndex == 2)
                StartCoroutine(MoveTo(0f));
            else
                StartCoroutine(MoveTo(-1f));

            positionIndex--;
        }

        if (Input.GetKeyDown(KeyCode.D) && positionIndex != 2)
        {
            if (positionIndex == 0)
                StartCoroutine(MoveTo(0f));
            else
                StartCoroutine(MoveTo(1f));

            positionIndex++;
        }
    }

    private IEnumerator MoveTo(float newXPosition)
    {
        isMoving = true;

        SoundManager.Instance.PlayMove();

        Vector3 currentPosition = transform.position;
        Vector3 newPosition = new(newXPosition, transform.position.y, transform.position.z);

        while (interpolateAmount < 1f)
        {
            interpolateAmount += Time.deltaTime * interpolationSpeed;
            transform.position = Vector3.Lerp(currentPosition, newPosition, interpolateAmount);

            yield return new WaitForEndOfFrame();
        }

        interpolateAmount = 0f;
        isMoving = false;
    }

    private void ForwardMovement()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward);        
    }
}
