using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;

    public Transform _segmentPrefab;
    private List<Transform> _segments = new List<Transform>();

    public int initialSize = 4;
    private void Start()
    {
        ResetState();
    }

    //Movement
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            _direction = Vector2.up;
        else if(Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }
    }
    private void FixedUpdate()
    {

        for (int i = _segments.Count- 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
            );
    }

    //eat and grow
    private void Grow()
    {
        ScoreScript.scoreValue += 1;
        Transform segment = Instantiate(this._segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);    
    }
    //returning to the start
    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);

        for(int i = 1; i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(this._segmentPrefab));
        }
        this.transform.position = Vector3.zero;
    }
    //if y eat you will grow then if you hitting the wall go to reset state method.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Food")
        {
            Grow();
        }
        else if (other.tag == "Obstacle")
        {
            ResetState();
            ScoreScript.scoreValue = 0;
        }
    }


}
