using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow_Visuals : MonoBehaviour
{
    [SerializeField] private LineRenderer attackVisual;
    [SerializeField] private float attackVisualDuration = .1f;

    [Header("Glowing visuals")]
    [SerializeField] private MeshRenderer meshRenderer;
    [Space]
    [SerializeField] private float maxIntensity = 150;
    [Space]
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;

    [Header("Rotor visuals")]
    [SerializeField] private Transform rotor;
    [SerializeField] private Transform rotorUnloaded;
    [SerializeField] private Transform rotorLoaded;

    [Header("Front glow string")]
    [SerializeField] private LineRenderer frontLeftString;
    [SerializeField] private LineRenderer frontRightString;
    [Space]
    [SerializeField] private Transform frontStartPointLeft;
    [SerializeField] private Transform frontStartPointRight;
    [SerializeField] private Transform frontEndPointLeft;
    [SerializeField] private Transform frontEndPointRight;

    [Header("Back glow string")]
    [SerializeField] private LineRenderer backLeftString;
    [SerializeField] private LineRenderer backRightString;
    [Space]
    [SerializeField] private Transform backStartPointLeft;
    [SerializeField] private Transform backStartPointRight;
    [SerializeField] private Transform backEndPointLeft;
    [SerializeField] private Transform backEndPointRight;

    [SerializeField] private LineRenderer[] lineRendererArray;

    private Material material;
    private float currentIntensity;
    private Enemy myEnemy;

    private void Awake()
    {
        material = new Material(meshRenderer.material);
        meshRenderer.material = material;
        UpdateMaterialsOnLineRenderers();

        StartCoroutine(ChangeEmision(1));
    }    

    private void Update()
    {
        UpdateEmissionColor();
        UpdateStrings();
        UpdateAttackVisualsIfNeeded();
    }

    private void UpdateAttackVisualsIfNeeded()
    {
        if (attackVisual.enabled && myEnemy != null)
        {
            attackVisual.SetPosition(1, myEnemy.CenterPoint());
        }
    }

    private void UpdateMaterialsOnLineRenderers()
    {
        foreach (var line in lineRendererArray)
        {
            line.material = material;
        }
    }

    private void UpdateStrings()
    {
        UpdateStringVisual(frontLeftString, frontStartPointLeft, frontEndPointLeft);
        UpdateStringVisual(frontRightString, frontStartPointRight, frontEndPointRight);
        UpdateStringVisual(backRightString, backStartPointRight, backEndPointRight);
        UpdateStringVisual(backLeftString, backStartPointLeft, backEndPointLeft);
    }

    private void UpdateEmissionColor()
    {
        Color emissionColor = Color.Lerp(startColor, endColor, currentIntensity / maxIntensity);
        emissionColor *= Mathf.LinearToGammaSpace(currentIntensity);

        material.SetColor("_EmissionColor", emissionColor);
    }

    public void PlayReloadVFX(float duration)
    {
        float newDuration = duration / 2;

        StartCoroutine(ChangeEmision(newDuration));
        StartCoroutine(UpdateRotorPosition(newDuration));
    }

    public void PlayAttackVFX(Vector3 startPoint, Vector3 endPoint, Enemy newEnemy)
    {
        StartCoroutine(VFXCoroutine(startPoint, endPoint, newEnemy));
    }

    private IEnumerator VFXCoroutine(Vector3 startPoint, Vector3 endPoint, Enemy newEnemy)
    {
        myEnemy = newEnemy;

        attackVisual.enabled = true;

        attackVisual.SetPosition(0, startPoint);
        attackVisual.SetPosition(1, endPoint);

        yield return new WaitForSeconds(attackVisualDuration);

        attackVisual.enabled = false;
    }

    private IEnumerator ChangeEmision(float duration)
    {
        float startTime = Time.time;
        float startIntensity = 0;

        while(Time.time - startTime < duration)
        {
            float tvalue = (Time.time - startTime) / duration;
            currentIntensity = Mathf.Lerp(startIntensity, maxIntensity, tvalue);
            yield return null;
        }

        currentIntensity = maxIntensity;
    }

    private IEnumerator UpdateRotorPosition(float duration)
    {
        float startTime = Time.time;

        while(Time.time - startTime < duration)
        {
            float tValue = (Time.time - startTime) / duration;
            rotor.position = Vector3.Lerp(rotorUnloaded.position, rotorLoaded.position, tValue);
            yield return null;
        }
        rotor.position = rotorLoaded.position;
    }

    private void UpdateStringVisual(LineRenderer lineRenderer, Transform startPoint, Transform endPoint)
    {
        lineRenderer.SetPosition(0, startPoint.position);
        lineRenderer.SetPosition(1, endPoint.position);
    }
}
