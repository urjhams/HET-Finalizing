﻿using UnityEngine;

public class ColliderHandleHard : MonoBehaviour
{
    public Global.GameObjectPattern selectedPattern;
    private void registerSelectedObject() {
        EyeOnlyHardRunner runnerInstance = GameObject
            .Find("GameRunner")
            .GetComponent<EyeOnlyHardRunner>;
        if (!runnerInstance.trialDone && Global.currentState != TrialState.Head)
        {
            runnerInstance.selectedPatternSet = selectedPattern;
        }
    }

    private void deRegisterSelectedObject() {
        EyeOnlyHardRunner runnerInstance = GameObject
            .Find("GameRunner")
            .GetComponent<EyeOnlyHardRunner>;
        if (!runnerInstance.trialDone && Global.currentState != TrialState.Head)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite 
                =  runnerInstance.white;
        }
        if (Global.currentState == TrialState.HeadEye) 
        { 
            GameObject
                .Find("headCursor")
                .GetComponent<HeadHandler>()
                .isObserving = false;
            GameObject
                .Find("headCursor")
                .GetComponent<HeadHandler>()
                .stateSequence
                .Clear();
        }       
        EyeOnlyHardRunner.selectedPatternSet = null;
    }

    private void registerHeadSelectedObject() {
        // with condition 3, the collider of Head system is not required 
        // but still need a Head tracker object
        switch (Global.currentState)
        {
            case TrialState.Eye:
                return;
            case TrialState.Head:
                break;
            case TrialState.HeadEye:
                return;
            case TrialState.Order:
                break;
        }
        EyeOnlyHardRunner runnerInstance = GameObject
            .Find("GameRunner")
            .GetComponent<EyeOnlyHardRunner>;
        if (runnerInstance.selectedPatternSet == selectedPattern) {
            runnerInstance.headSelectedPatternSet = selectedPattern;
        }
        
    }

    private void deRegisterHeadSelectedObject() {
        // with condition 3, the collider of Head system is not required 
        // but still need a Head tracker object
        switch (Global.currentState)
        {
            case TrialState.Eye:
                return;
            case TrialState.Head:
                break;
            case TrialState.HeadEye:
                return;
            case TrialState.Order:
                break;
        }
        EyeOnlyHardRunner runnerInstance = GameObject
            .Find("GameRunner")
            .GetComponent<EyeOnlyHardRunner>;
        if (runnerInstance.selectedPatternSet == selectedPattern) {
            if (Global.currentState == TrialState.Head && !runnerInstance.trialDone)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite 
                    = runnerInstance.white;
            }
            runnerInstance.headSelectedPatternSet = null;
        }
        
    }

    /// <summary>
    /// Sent each frame where another object is within a trigger collider
    /// attached to this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("eyeCursor")) 
        {
            registerSelectedObject();
        }
        if (other.gameObject.name.Equals("headCursor")) 
        {
            registerHeadSelectedObject();
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("eyeCursor")) 
        {
            deRegisterSelectedObject();
        }
        if (other.gameObject.name.Equals("headCursor")) 
        {
            deRegisterHeadSelectedObject();
        }
    }
}
