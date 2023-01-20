using UnityEngine;
using DG.Tweening;
using System.Collections;

public class DoTweenActions : MonoBehaviour
{
    Vector3 initalLocation;
    Vector3 initalSize;
    Vector3 initalRotation;

    public Vector3 targetLocation;
    public Vector3 targetSize;
    public Vector3 targetRotation;
    public float animationDuration;
    public Ease animationEase = Ease.Linear;
    [SerializeField] AnimationType animationType = AnimationType.Move;
    public bool doOnStart;
    public bool oneLoop;
    public bool infiniteLoop;

    enum AnimationType
    {
        Move,
        Rotate,
        Scale,
        MoveAndScale,
        MoveAndRotate,
        ScaleAndRotate
    }

    private void OnEnable()
    {
        initalLocation = transform.localPosition;
        initalSize = transform.localScale;
        initalRotation = transform.localRotation.eulerAngles;
    }

    private void OnDisable()
    {
        transform.localPosition = initalLocation;
        transform.localScale = initalSize;
        transform.localEulerAngles = initalRotation;
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }

    private void Start()
    {
        if (doOnStart)
        {
            if (infiniteLoop)
            {
                StartCoroutine(InfiniteLoop());
            }
            if (oneLoop)
            {
                StartCoroutine(OneLoop());
            }
            if (!infiniteLoop || !oneLoop)
            {
                DoAnimation();
            }
        }
    }

    public IEnumerator InfiniteLoop()
    {
        DoAnimation();
        yield return new WaitForSeconds(animationDuration);
        DoAnimationBackward();
        yield return new WaitForSeconds(animationDuration);
        StartCoroutine(InfiniteLoop());
    }

    public IEnumerator OneLoop()
    {
        DoAnimation();
        yield return new WaitForSeconds(animationDuration);
        DoAnimationBackward();
        yield return new WaitForSeconds(animationDuration);
    }

    public void DoAnimation()
    {
        if (animationType == AnimationType.Move)
        {
            transform.DOLocalMove(targetLocation, animationDuration).SetEase(animationEase);
        }
        else if (animationType == AnimationType.Rotate)
        {
            transform.DOLocalRotate(targetRotation, animationDuration).SetEase(animationEase);
        }
        else if (animationType == AnimationType.Scale)
        {
            transform.DOScale(targetSize, animationDuration).SetEase(animationEase);
        }
        else if (animationType == AnimationType.MoveAndScale)
        {
            DOTween.Sequence().SetAutoKill(false)
                .Append(transform.DOLocalMove(targetLocation, animationDuration).SetEase(animationEase))
                .Join(transform.DOScale(targetSize, animationDuration).SetEase(animationEase));
        }
        else if (animationType == AnimationType.MoveAndRotate)
        {
            DOTween.Sequence().SetAutoKill(false)
                .Append(transform.DOLocalMove(targetLocation, animationDuration).SetEase(animationEase))
                .Join(transform.DOLocalRotate(targetRotation, animationDuration).SetEase(animationEase));
        }
        else if (animationType == AnimationType.ScaleAndRotate)
        {
            DOTween.Sequence().SetAutoKill(false)
                .Append(transform.DOScale(targetSize, animationDuration).SetEase(animationEase))
                .Join(transform.DOLocalRotate(targetRotation, animationDuration).SetEase(animationEase));
        }
    }

    public void DoAnimationBackward()
    {
        if (animationType == AnimationType.Move)
        {
            transform.DOLocalMove(initalLocation, animationDuration).SetEase(animationEase);
        }
        else if (animationType == AnimationType.Rotate)
        {
            transform.DOLocalRotate(initalRotation, animationDuration).SetEase(animationEase);
        }
        else if (animationType == AnimationType.Scale)
        {
            transform.DOScale(initalSize, animationDuration).SetEase(animationEase);
        }
        else if (animationType == AnimationType.MoveAndScale)
        {
            DOTween.Sequence().SetAutoKill(false)
                .Append(transform.DOLocalMove(initalLocation, animationDuration).SetEase(animationEase))
                .Join(transform.DOScale(initalSize, animationDuration).SetEase(animationEase));
        }
        else if (animationType == AnimationType.MoveAndRotate)
        {
            DOTween.Sequence().SetAutoKill(false)
                .Append(transform.DOLocalMove(initalLocation, animationDuration).SetEase(animationEase))
                .Join(transform.DOLocalRotate(initalRotation, animationDuration).SetEase(animationEase));
        }
        else if (animationType == AnimationType.ScaleAndRotate)
        {
            DOTween.Sequence().SetAutoKill(false)
                .Append(transform.DOScale(initalSize, animationDuration).SetEase(animationEase))
                .Join(transform.DOLocalRotate(initalRotation, animationDuration).SetEase(animationEase));
        }
    }
}