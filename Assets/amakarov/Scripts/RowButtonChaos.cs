using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RowButtonChaos : MonoBehaviour
{
    private int _chaosNumber;
    private int _rowButtonNumber;
    [SerializeField] private GameObject[] rowButtons = null;

    public void GenerateChaosNumber()
    {
        _chaosNumber = Random.Range(0, 5);
        _rowButtonNumber = GameManager.Instance.rowButtonHand;
        DOTween.Init();
        StartTheChaos();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _chaosNumber = 0;
            _rowButtonNumber = GameManager.Instance.rowButtonHand;
            DOTween.Init();
            StartTheChaos();
        }
    }

    private void StartTheChaos()
    {
        switch (_chaosNumber)
        {
            case 0:
            {
                ChangeScaleAndPosition();
                break;
            }
            case 1:
            {
                ChangeAlphaAndPosition();
                break;
            }
            case 2:
            {
                ShakeRowButton();
                break;
            }
            case 3:
            {
                JumpRowButton();
                break;
            }

            case 4:
            {
                MakeButtonsGreatAgain();
                break;
            }
        }
    }

    private void ChangeScaleAndPosition()
    {
        if (_rowButtonNumber == 0)
            rowButtons[_rowButtonNumber].transform.localScale = 
                new Vector3(-0.3f, 0.3f, 0.3f);
        else rowButtons[_rowButtonNumber].transform.localScale = 
            new Vector3(0.3f, 0.3f, 0.3f);
        var x = Random.Range(-780, 781);
        var y = Random.Range(-370, 371);
        rowButtons[_rowButtonNumber].transform.localPosition = new Vector3(x, y, 0);
        StartCoroutine(WaitAndStopChaos());
    }
    
    private void ChangeAlphaAndPosition()
    {
        rowButtons[_rowButtonNumber].GetComponent<Button>().image.color = 
            new Color(1,1, 1,0.1f);
        var x = Random.Range(-780, 781);
        var y = Random.Range(-370, 371);
        rowButtons[_rowButtonNumber].transform.localPosition = new Vector3(x, y, 0);
        StartCoroutine(WaitAndStopChaos());
    }

    private void ShakeRowButton()
    {
        rowButtons[_rowButtonNumber].transform.DOShakePosition(3, 300,
            100, 30, true);
        StartCoroutine(WaitAndStopChaos());
    }

    private void JumpRowButton()
    {
        rowButtons[_rowButtonNumber].transform.DOLocalJump(new Vector2(100, 200),
            200, 10, 3, true);
        StartCoroutine(WaitAndStopChaos());
    }

    private void MakeButtonsGreatAgain()
    {
        rowButtons[_rowButtonNumber].transform.localPosition = new Vector3(0, 0, 0);
        if (_rowButtonNumber == 0)
            rowButtons[_rowButtonNumber].transform.DOScale(new Vector3(-5f, 5f, 5f), 
                3f);
        else rowButtons[_rowButtonNumber].transform.DOScale(5, 3f);
        StartCoroutine(WaitAndStopChaos());
    }
    
    private IEnumerator WaitAndStopChaos()
    {
        yield return new WaitForSeconds(3f);
        RowButtonsDefault();
    }
    
    private void RowButtonsDefault()
    {
        rowButtons[_rowButtonNumber].GetComponent<Button>().image.color = 
            new Color(1,1, 1,1f);
            if (_rowButtonNumber == 0)
            {
                rowButtons[_rowButtonNumber].transform.localPosition = 
                    new Vector3(-785, -370, 0);
                rowButtons[_rowButtonNumber].transform.localScale = new Vector3(-1, 1, 1);
            }

            else if (_rowButtonNumber == 1)
            {
                rowButtons[_rowButtonNumber].transform.localPosition = 
                    new Vector3(785, -370, 0);
                rowButtons[_rowButtonNumber].transform.localScale = Vector3.one;
            }
    }
}
