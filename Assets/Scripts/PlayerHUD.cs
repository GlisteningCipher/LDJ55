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
    private Sequence neutralAnimation;

    public enum Reaction
    {
       Neutral, Blink, Sad, Happy
    }

    private void Start()
    {
        Neutral();
    }

    [ContextMenu("React Neutral")]
    public void Neutral()
    {
        magicianImage.sprite = reactions[(int)Reaction.Neutral];
        neutralAnimation = DOTween.Sequence()
            .AppendInterval(5f)
            .AppendCallback(Blink)
            .SetLoops(-1, LoopType.Restart);
    }
    [ContextMenu("React Blink")]
    public void Blink() => ReactForSeconds(Reaction.Blink, 0.1f);

    [ContextMenu("React Sad")]
    public void Sad() => ReactForSeconds(Reaction.Sad, 1.5f);

    [ContextMenu("React Happy")]
    public void Happy() => ReactForSeconds(Reaction.Happy, 1.5f);

    public void ReactForSeconds(Reaction reaction, float seconds)
    {
        if (neutralAnimation.IsActive()) neutralAnimation.Kill();
        magicianImage.sprite = reactions[(int)reaction];
        StartCoroutine(ResetReaction(seconds));
    }

    private IEnumerator ResetReaction(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Neutral();
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
