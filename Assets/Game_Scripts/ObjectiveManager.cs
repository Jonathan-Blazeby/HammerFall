using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private List<ObjectiveHealth> allObjectiveTransforms;

    private ObjectiveHealth currentObjectiveTransform;
    #endregion

    #region Public Fields
    public static ObjectiveManager Instance;
    #endregion

    #region Monobehavior Callbacks
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region Private Methods
    private void LastObjectiveDestroyed()
    {
        GameManager.Instance.PlayerLoss();
    }
    #endregion

    #region Public Methods
    public void SetupGameMode()
    {
        currentObjectiveTransform = allObjectiveTransforms[0];
    }
    public Transform GetCurrentObjectiveTransform()
    {
        return currentObjectiveTransform.transform;
    }

    public void ObjectiveDestroyed(ObjectiveHealth deadObjective)
    {
        int nextObjIndex = allObjectiveTransforms.IndexOf(deadObjective) + 1;
        if(nextObjIndex == allObjectiveTransforms.Count)
        {
            LastObjectiveDestroyed(); 
        }
        else
        {
            currentObjectiveTransform = allObjectiveTransforms[nextObjIndex];
        }
    }
    #endregion
}
