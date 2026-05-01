using UnityEngine;

public class PageTurnManager : MonoBehaviour
{
    public Animator bookAnimator;

    public void PlayTurnNext()
    {
        if (bookAnimator != null)
            bookAnimator.SetTrigger("TurnNext");
    }

    public void PlayTurnPrev()
    {
        if (bookAnimator != null)
            bookAnimator.SetTrigger("TurnPrev");
    }
}
