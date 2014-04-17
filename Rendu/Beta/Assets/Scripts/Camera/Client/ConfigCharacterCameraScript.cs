using UnityEngine;
using System.Collections;

public class ConfigCharacterCameraScript : MonoBehaviour {

	/* Ecrire des méthode qui seront appelé par les scripts d'initialisation de la Camera
     * par exemple setter des Référence à des transform ou autre
     * Pour le moment ce script est appelé par le PlayerDataBase par la méthode InitClient
     * Cette méthode est utilisé par les clients
    */
    public void ConfigCameraAndSurvivor(Transform cameraTransform, Transform survivorTransform)
    {
        Camera characterCamera = gameObject.camera;
        
        CameraResetOnCharacterScript cameraResetScript =  cameraTransform.GetComponent<CameraResetOnCharacterScript>();
        cameraResetScript.setSurvivorTranform(survivorTransform);

        InputManagerMoveSurvivorScript inputSurvivantManagerScript = survivorTransform.GetComponent<InputManagerMoveSurvivorScript>();
        inputSurvivantManagerScript.setCameraTransform(characterCamera);

    }
}
