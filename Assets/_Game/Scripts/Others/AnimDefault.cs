using DG.Tweening;
using UnityEngine;

public class AnimDefault : MonoBehaviour {

	public static void AnimIn (UnityEngine.UI.Image background, RectTransform popup) {

        background.color = new Vector4(0, 0, 0, 0);
        popup.localPosition = new Vector3(0, -(Screen.height + 300), 0);

        background.DOFade(0.8f, 1);
        popup.DOAnchorPosY(0, 1);
    }

    public static void AnimOut(UnityEngine.UI.Image background, RectTransform popup, GameObject prefab)
    {

        background.DOFade(0, 1);
        popup.DOAnchorPosY(-(Screen.height + 300), 1).OnComplete(() =>
        {
            Destroy(prefab);
        });
    }

    public static void AnimIn(UnityEngine.UI.Image background)
    {
        if (background != null)
        {
            background.color = new Vector4(0, 0, 0, 0);
            background.DOFade(0.8f, 1);
        }
    }

    public static void AnimOut(UnityEngine.UI.Image background)
    {
        background.DOFade(0, 1);
    }
}
