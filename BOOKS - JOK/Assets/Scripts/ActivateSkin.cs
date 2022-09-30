using UnityEngine;

public class ActivateSkin : MonoBehaviour
{
    [SerializeField]
    ChangeSkin changeSkin;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(this.name == "MonkeyActivate")
        {
            Destroy(this.gameObject);
            Debug.Log("Monkey Activating");
            changeSkin.MonkeySkin();
        }
        if(this.name == "CheetahActivate")
        {
            Destroy(this.gameObject);
            Debug.Log("Cheetah Activating");
            changeSkin.CheetahSkin();
            
        }
    }
}
