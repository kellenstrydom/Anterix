using System.Collections;
using UnityEngine;

public class Collection : MonoBehaviour
{
    public float collectAnimationTime;
    PotionManager _potion;
    public IngredientInfo ingredientInfo;
    
    public AnimationCurve moveCurve;
    public AnimationCurve shrinkCurve;
    public void Collect()
    {
        // find potion
        _potion = GameObject.Find("Potion Manager").GetComponent<PotionManager>();
        
        
        // animation
        StartCoroutine(CollectionAnimation(_potion.potion.position));
        // die
        
    }

    IEnumerator CollectionAnimation(Vector2 target)
    {
        Vector2 startPosition = transform.position;
        Vector3 startScale = transform.localScale;
        float time = 0f;

        while (time < collectAnimationTime)
        {
            float t = time / collectAnimationTime;         
            float curveT = moveCurve.Evaluate(t); 
            transform.position = Vector2.Lerp(startPosition, target, curveT);
            float shrinkT = shrinkCurve.Evaluate(t);
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, shrinkT);

            time += Time.deltaTime;
            yield return null;
        }

        AddToPotion();
        Destroy(gameObject);
    }

    void AddToPotion()
    {
        _potion.AddIngredients(ingredientInfo.ingredientType);
    }
    
}
