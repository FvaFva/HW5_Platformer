using UnityEngine;

public class Wollit : MonoBehaviour
{
    [SerializeField] public int CountCoin { get; private set; }

    public void AddCoin()
    {
        CountCoin++;
    }
}
