using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsHealthVisual : MonoBehaviour
{
    [SerializeField] private Sprite heart0Sprite;
    [SerializeField] private Sprite heart1Sprite;
    [SerializeField] private Sprite heart2Sprite;
    [SerializeField] private Sprite heart3Sprite;
    [SerializeField] private Sprite heart4Sprite;

    private List<HeartImage> heartImageList;
    private HeartsHealthSystem heartsHealthSystem;

    private void Awake()
    {
        heartImageList = new List<HeartImage>();
    }

    private void Start()
    {
        HeartsHealthSystem heartsHealthSystem = new HeartsHealthSystem(5);
        SetHeartsHealthSystem(heartsHealthSystem);

        //heartsHealthSystem.Damage(0);
        //heartsHealthSystem.Heal(2);
    }

    public void SetHeartsHealthSystem(HeartsHealthSystem heartsHealthSystem)
    {
        this.heartsHealthSystem = heartsHealthSystem;

        List<HeartsHealthSystem.Heart> heartList = heartsHealthSystem.GetHeartList();
        int row = 0;
        int col = 0;
        int colMax = 5;
        float rowColSize = 40f;

        
        for(int i=0; i < heartList.Count; i++)
        {
            HeartsHealthSystem.Heart heart = heartList[i];
            Vector2 heartAnchoredPosition = new Vector2(col * rowColSize, -row * rowColSize);
            CreateHeartImage(heartAnchoredPosition).SetHeartFragments(heart.GetFragmentAmount());

            col++;
            if(col >= colMax)
            {
                row++;
                col = 0;
            }
        }

        heartsHealthSystem.OnDamaged += HeartsHealthSystem_OnDamaged;
        heartsHealthSystem.OnHealed += HeartsHealthSystem_OnHealed;
        heartsHealthSystem.OnDead += HeartsHealthSystem_OnDead;
    }

    private void HeartsHealthSystem_OnDead(object sender, System.EventArgs e)
    {
        Debug.Log("DEAD");
    }

    private void HeartsHealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        //Hearts health system was healed
        RefreshAllHearts();
    }

    private void HeartsHealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        //Hearts health system was damaged
        RefreshAllHearts();
    }

    private void RefreshAllHearts()
    {
        List<HeartsHealthSystem.Heart> heartList = heartsHealthSystem.GetHeartList();
        for (int i = 0; i < heartImageList.Count; i++)
        {
            HeartImage heartImage = heartImageList[i];
            HeartsHealthSystem.Heart heart = heartList[i];
            heartImage.SetHeartFragments(heart.GetFragmentAmount());
        }
    }

    private void HealingAnimatedPeriodic()
    {

    }

    private HeartImage CreateHeartImage(Vector2 anchoredPosition)
    {
        //Create Game Object
        GameObject heartGameObject = new GameObject("Heart", typeof(Image));

        //Set as child of this transform
        heartGameObject.transform.SetParent(transform);
        heartGameObject.transform.localPosition = Vector3.zero;

        //Locate and Size heart
        heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;

        //Set heart sprite
        Image heartImageUI = heartGameObject.GetComponent<Image>();
        heartImageUI.sprite = heart0Sprite;
        heartImageUI.SetNativeSize();

        HeartImage heartImage = new HeartImage(this,heartImageUI);
        heartImageList.Add(heartImage);

        return heartImage;
    }

    //Represents a single heart
    public class HeartImage
    {
        private Image heartImage;
        private HeartsHealthVisual heartsHealthVisual;

        public HeartImage(HeartsHealthVisual heartsHealthVisual, Image heartImage)
        {
            this.heartsHealthVisual = heartsHealthVisual;
            this.heartImage = heartImage;
        }

        public void SetHeartFragments(int fragments)
        {
            switch (fragments)
            {
                case 0: heartImage.sprite = heartsHealthVisual.heart0Sprite; break;
                case 1: heartImage.sprite = heartsHealthVisual.heart1Sprite; break;
                case 2: heartImage.sprite = heartsHealthVisual.heart2Sprite; break;
                case 3: heartImage.sprite = heartsHealthVisual.heart3Sprite; break;
                case 4: heartImage.sprite = heartsHealthVisual.heart4Sprite; break;
            }
        }
    }
}
