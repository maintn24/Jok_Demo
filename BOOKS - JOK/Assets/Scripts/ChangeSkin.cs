using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    public AnimatorOverrideController PeteAnim;
    public AnimatorOverrideController MonkeyAnim;
    public AnimatorOverrideController CheetahAnim;

    public string whatskin = "Pete";

    public void PeteSkin()
    {
        GetComponent<Animator>().runtimeAnimatorController = PeteAnim as RuntimeAnimatorController;
        whatskin = "Pete";
    }

    public void MonkeySkin()
    {
        GetComponent<Animator>().runtimeAnimatorController = MonkeyAnim as RuntimeAnimatorController;
        whatskin = "Monkey";
    }

    public void CheetahSkin()
    {
        GetComponent<Animator>().runtimeAnimatorController = CheetahAnim as RuntimeAnimatorController;
        whatskin = "Cheetah";
    }
}
