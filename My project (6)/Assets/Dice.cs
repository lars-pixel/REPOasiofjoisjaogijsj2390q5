using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] public static GameObject player1, player2;
    private Sprite[] diceSides;
    private SpriteRenderer rend;
    private int whosTurn = 1;
    private bool coroutineAllowed = true;

    // Use this for initialization
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
        rend.sprite = diceSides[5];

        player1 = GameObject.Find("Player1movement");
        player2 = GameObject.Find("Player2movement");
        player1.GetComponentInChildren<Animator>().enabled = true;
        player2.GetComponentInChildren<Animator>().enabled = true;
    }

    private void OnMouseDown()
    {
        if (!GameControl.gameOver && coroutineAllowed)
            StartCoroutine("RollTheDice");
    }

    private IEnumerator RollTheDice()
    {
        coroutineAllowed = false;
        int randomDiceSide = 0;
        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }

        GameControl.diceSideThrown = randomDiceSide + 1;
        if (whosTurn == 1)
        {
            GameControl.MovePlayer(1);
            player1.GetComponentInChildren<Animator>().SetBool("playWalkingAnim", true);
        }
        else if (whosTurn == -1)
        {
            GameControl.MovePlayer(2);
            player2.GetComponentInChildren<Animator>().SetBool("playWalkingAnim", true);
        }
        whosTurn *= -1;
        coroutineAllowed = true;
    }
}
