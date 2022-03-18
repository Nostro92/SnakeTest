using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    [SerializeField] private Transform _bodyPrefab;
    [SerializeField] private Transform _kostil;
    [SerializeField] private Text _scoreText;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _exitButton;

    private Vector2 _direction = Vector2.up;
    private float _stepTimer = 0;
    private List<Transform> _bodyList;
    private int _score;

    private void Start()
    {
        Time.timeScale = 0;
        _startButton.SetActive(true);
        _exitButton.SetActive(true);
        
    }
    private void Update()
    {
        ScoreText();
        if (_stepTimer >= 0.3)
        {
            Move();
            _stepTimer = 0;
        }

        _stepTimer += Time.deltaTime;
    }
    private void Move()
    {
        for (int i = _bodyList.Count - 1; i > 0; i--)
            _bodyList[i].position = _bodyList[i - 1].position;
            
        transform.position = new Vector2(transform.position.x + _direction.x, transform.position.y + _direction.y);
    }

   
    public void Up()
    {
        if(_direction != Vector2.down)
          _direction = Vector2.up;
    }

    public void Down()
    {
        if (_direction != Vector2.up)
            _direction = Vector2.down;
    }

    public void Right()
    {
        if(_direction != Vector2.left)
          _direction = Vector2.right;
    }

    public void Left()
    {
        if(_direction != Vector2.right)
           _direction = Vector2.left;
    }

    private void AddBody()
    {
        Transform body = Instantiate(_bodyPrefab);
        body.position = _bodyList[_bodyList.Count - 1].position;
        _bodyList.Add(body);
        _score++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
            AddBody();
        else if (collision.tag == "Death")
            DeathSnake();
    }

    private void DeathSnake()
    {
        for (int i = 2; i < _bodyList.Count; i++)
            Destroy(_bodyList[i].gameObject);

        Time.timeScale = 0;
        _startButton.SetActive(true);
        _exitButton.SetActive(true);
    }

    private void ScoreText()
    {
        _scoreText.text = "SCORE:" + _score.ToString(); 
    }

    public void StartGame()
    {
        transform.position = new Vector2(0, -1);
        _kostil.transform.position = new Vector2(0, -2);

        _bodyList = new List<Transform>();
        _bodyList.Add(transform);
        _bodyList.Add(_kostil.transform);

        _score = 0;

        Time.timeScale = 1;
        _startButton.SetActive(false);
        _exitButton.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
