using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHUD : MonoBehaviour
{
    [Header("Magician")]
    [SerializeField] Image magicianImage;
    [SerializeField] Sprite[] reactions;
    [Header("Recipe")]
    [SerializeField] Image[] recipeImages;
    [SerializeField] float flashPeriod = 1f;
    [SerializeField] float opacityMin = 0.5f;
    [SerializeField] float opacityMax = 1f;

    private Tween flashTween;

    public void SetReaction(int reactionIndex)
    {
        magicianImage.sprite = reactions[reactionIndex];
        StartCoroutine(ResetReaction());
    }

    private IEnumerator ResetReaction()
    {
        yield return new WaitForSeconds(1f);
        magicianImage.sprite = reactions[0];
    }

    public void SetSpellRecipe(Sprite[] sprites)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            recipeImages[i].sprite = sprites[i];
        }
    }

    public void SetFlash(int currentWave)
    {
        if (flashTween.IsActive()) flashTween.Kill();
        for(int i = 0; i < recipeImages.Length; ++i)
        {
            if (i < currentWave) recipeImages[i].DOFade(opacityMax, 0f); //full opacity
            if (i == currentWave) flashTween = FlashItem(recipeImages[i]);
            if (i > currentWave) recipeImages[i].DOFade(opacityMin, 0f); //partial opacity
        }
    }

    private Tween FlashItem(Image itemImage)
    {
        return itemImage.DOFade(1f, flashPeriod * 0.5f)
            .From(0.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear)
            .OnKill(()=>itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b , 1f));
    }
}
