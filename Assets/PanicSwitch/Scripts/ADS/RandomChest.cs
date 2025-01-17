using System.Collections;
using UnityEngine;
using TMPro;

public class RandomChest : MonoBehaviour
{
    [SerializeField] private GameObject chestPrefab;
    [SerializeField] private Transform[] chestPoints;
    [SerializeField] ParticleSystem particleGold;
    [SerializeField] private TextMeshProUGUI _displayMoneyText;
    [SerializeField] private Animator ChestWindowUI;
   // private VideoReward _maxReward;
    private float _timer = 10;

    void Start()
    {
       // _maxReward = FindObjectOfType<VideoReward>();
        ChestPickUp _chestPickUp = chestPrefab.GetComponentInChildren<ChestPickUp>();
        _chestPickUp.PickUpCHestParticle = particleGold;
        _chestPickUp._randomChest = this;
    }

    public void StartChestTimer()
    {
        StartCoroutine(InstantChest(_timer));
    }

    IEnumerator InstantChest(float _timerChest)
    {
        yield return new WaitForSeconds(_timerChest);
      //  UpdateText();
        Instantiate(chestPrefab, chestPoints[Random.Range(0, chestPoints.Length)].position, Quaternion.Euler(new Vector3(0, -180, 0)));
        _timer = Random.Range(50, 120);
        StartCoroutine(InstantChest(_timer));
    }

    private void UpdateText()
    {
      //  _displayMoneyText.text = "<color=#ffdc30>+" +_maxReward.RandomChestOfGold.ToString()+ " gold</color>";
      
    }

    public void OpenChestWindow()
    {
        UpdateText();
//        ChestWindowUI.SetTrigger("show");       
    }

    public void Close()
    {
        ChestWindowUI.SetTrigger("hide");
    }

    public void AdsGetGold()
    {
     //   _maxReward.AdsGetReward(32);
        Close();
    }
}
