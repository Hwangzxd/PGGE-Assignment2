using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public List<AudioClip> concreteSfx;
    public List<AudioClip> dirtSfx;
    public List<AudioClip> metalSfx;
    public List<AudioClip> sandSfx;
    public List<AudioClip> woodSfx;

    // Enum to represent different ground materials
    enum groundMaterial
    {
        concrete, dirt, metal, sand, wood, empty
    }

    private AudioSource footstepSource;

    // Initializing the AudioSource component
    void Start()
    {
        footstepSource = GetComponent<AudioSource>();
    }

    // Method to check the ground material beneath the player
    private groundMaterial SurfaceSelect()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, -Vector3.up);
        Material surfaceMaterial;

        // Check for a raycast hit to determine the ground material
        if (Physics.Raycast(ray, out hit, 1.0f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            Renderer surfaceRenderer = hit.collider.gameObject.GetComponentInChildren<Renderer>();
            // Check if a surfaceRenderer is found
            if (surfaceRenderer)
            {
                surfaceMaterial = surfaceRenderer ? surfaceRenderer.sharedMaterial : null;
                // Identify the ground material based on the surface material name
                if (surfaceMaterial.name.Contains("CoarseConcrete_StandardShader"))
                {
                    return groundMaterial.concrete;
                }
                else if (surfaceMaterial.name.Contains("Grass pattern 01"))
                {
                    return groundMaterial.dirt;
                }
                else if (surfaceMaterial.name.Contains("BrushedIron_StandardShader"))
                {
                    return groundMaterial.metal;
                }
                else if (surfaceMaterial.name.Contains("Ground pattern 02"))
                {
                    return groundMaterial.sand;
                }
                else if (surfaceMaterial.name.Contains("WoodenFlooring_StandardShader"))
                {
                    return groundMaterial.wood;
                }
                else
                {
                    return groundMaterial.empty;
                }
            }
        }
        // Return empty if no ground material is detected
        return groundMaterial.empty;
    }

    // Method to play footstep sounds based on the ground material
    public void PlayFootstep()
    {
        AudioClip clip = null;

        // Get the ground material beneath the player
        groundMaterial surface = SurfaceSelect();

        // Select a random footstep sound clip based on the ground material
        switch (surface)
        {
            case groundMaterial.concrete:
                clip = concreteSfx[Random.Range(0, concreteSfx.Count)];
                break;
            case groundMaterial.dirt:
                clip = dirtSfx[Random.Range(0, dirtSfx.Count)];
                break;
            case groundMaterial.metal:
                clip = metalSfx[Random.Range(0, metalSfx.Count)];
                break;
            case groundMaterial.sand:
                clip = sandSfx[Random.Range(0, sandSfx.Count)];
                break;
            case groundMaterial.wood:
                clip = woodSfx[Random.Range(0, woodSfx.Count)];
                break;
            default:
                break;
        }

        // Play the selected footstep sound if the ground material is not empty
        if (surface != groundMaterial.empty)
        {
            footstepSource.clip = clip;
            // Play the clip at a random volume
            footstepSource.volume = Random.Range(0.1f, 0.3f);
            // Play the clip at a random pitch
            footstepSource.pitch = Random.Range(0.8f, 1.2f);
            footstepSource.Play();
        }

    }

}
