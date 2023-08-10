using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Shoot,
    Run,
    Patrol
}

public class StateMachine : MonoBehaviour
{
    [SerializeField]
    private State _aiState = State.Shoot;

    private SpriteRenderer _sprite;

    // Start is called before the first frame update
    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        NextState();
    }

    private void NextState()
    {


        switch (_aiState)
        {
            case State.Shoot:
                StartCoroutine(ShootState());
                break;
            case State.Run:
                StartCoroutine(RunState());
                break;
            case State.Patrol:
                StartCoroutine(PatrolState());
                break;
            default:
                Debug.LogError("WAT R U DOIN HERE?");
                break;

        }
    }

    private IEnumerator ShootState()
    {
        Debug.Log("Start Shoot");

        int colorChangeCount = 0;
        while (_aiState == State.Shoot)
        {
            yield return new WaitForSeconds(0.5f);
            _sprite.color = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f));
            colorChangeCount++;

            if (colorChangeCount >= 5)
            {
                _aiState = State.Patrol;
            }

            yield return null;
        }

        Debug.Log("End Shoot");
        NextState();
    }

    private IEnumerator RunState()
    {
        Debug.Log("Start Run");
        while (_aiState == State.Run)
        {
            float wave = Mathf.Sin(Time.time);

            transform.localScale = new Vector3(1, wave, 1);

            yield return null;
        }

        Debug.Log("End Run");
        NextState();
    }
    private IEnumerator PatrolState()
    {
        Debug.Log("Start Patrol");
        while (_aiState == State.Patrol)
        {
            transform.rotation *= Quaternion.Euler(0f, 0f, 50f*Time.deltaTime);
            yield return null;
        }

        //yield return new WaitForSeconds(2f);

        Debug.Log("End Patrol");
        NextState();
    }


}
