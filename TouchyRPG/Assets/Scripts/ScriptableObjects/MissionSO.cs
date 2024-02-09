using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "MissionSO", menuName = "ScriptableObjects/Missions/Mission")]
public class MissionSO : ScriptableObject
{
    [Serializable]
    public class State
    {
        public string scene_dest;
        public int state;
    }

    [Serializable]
    public class DestinationPoints
    {
        public Vector3 destination;
        public int state;
    }

    [Serializable]
    public class Information
    {
        public string destination;
        public int state;
    }

    public string title;
    public List<DestinationPoints> missionDestinationPoints = new List<DestinationPoints>();
    public List<Information> information = new List<Information>();
    public int currentState = 0;
    public int goldReward;
    public GameObject unitPrefab;
    public Dictionary<int, Vector3> destinationPoints = new Dictionary<int, Vector3>();
    public List<State> listState = new List<State>();
    



    public void Start()
    {
        // Agregar objetos a un diccionario usando el índice como clave
        /**
        for (int i = 0; i < missionDestinationPoints.Count; i++)
        {   
            Vector3 estado = missionDestinationPoints[i].state;
            Debug.Log(estado);
            destinationPoints.Add(estado, missionDestinationPoints[i].destination);
        }
        */

        currentState = listState[0].state;
        Debug.Log("El estado actual del estado es:" + currentState);
        instantiateMissionIcon();


        unEnable();

        

    }

    public void unEnable()
    {
        // Buscar el objeto Sensei en la escena por nombre
        GameObject sensei = GameObject.Find("Sensei");

        if (sensei != null)
        {
            // Obtener el componente MissionIcon y desactivarlo
            GameObject missionIcon = sensei.transform.GetChild(3).gameObject;
            Debug.Log("El objeto obtenido es" + missionIcon.name);

            if (missionIcon != null)
            {
                missionIcon.SetActive(false); // O usa missionIconComponente.gameObject.SetActive(false);
                Debug.Log("Componente MissionIcon desactivado en el objeto Sensei.");
            }
            else
            {
                Debug.LogError("No se encontró el componente MissionIcon en el objeto Sensei.");
            }
        }
        else
        {
            Debug.LogError("No se encontró el objeto Sensei en la escena.");
        }
    }


    public void reload()
    {
        if(currentState == 1)
        {
            instantiateMissionIcon();
        }


        string nombreEscenaActual = SceneManager.GetActiveScene().name;
        Debug.Log("La escena actual es " + nombreEscenaActual);

        foreach (var scene in listState)
        {
            if(scene.state == currentState)
            {
                if(scene.scene_dest == nombreEscenaActual)
                {
                    currentState++;
                }
            }
        }




    }


    public void instantiateMissionIcon()
    {
        // Encuentra el objeto "MissionIconBox" en la escena
        GameObject missionIconBox = GameObject.Find("MissionIconBoxes");
        GameObject icon;

        Vector3 dest = missionDestinationPoints[currentState-1].destination;


        // Buscar el valor asociado a la clave "Dos"
        if (dest != null)
        {
            Debug.Log("El valor asociado a la clave 'Dos' es: " + dest + "cont estado:" + currentState);


            if (missionIconBox != null)
            {
                // Instancia el objeto y configúralo como hijo de "MissionIconBox"
                icon = Instantiate(unitPrefab, dest, Quaternion.identity, missionIconBox.transform);
                icon.name = "MissionDestination";
            }
            else
            {
                Debug.LogError("No se encontró el objeto MissionIconBox en la escena.");
            }
        }
        else
        {
            Console.WriteLine("El estado actual no se encuentra en el diccionario.");
        }

        
    }
    
}
