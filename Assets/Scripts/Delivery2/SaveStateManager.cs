using System;
using System.IO;
using UnityEngine;

public class SaveStateManager : MonoBehaviour
{

    // PLAYERPREFS 
    // sirve para guardar preferencias
    // podemos guardar un rango limitado de datos (tipos)
    // en el fondo es un XML con un montón de tuplas llave-valor

    // GUARDADO DE ARCHIVOS 
    // para guardar estructuras más complejas utilizamos el sistema de archivos
    // normalmente el save state tiene alguna espece de encriptación / hashing o algo

    // PARA LA PRUEBA DE GUARDADO DE ARCHIVOS VAMOS A DECLARAR UNA ESTRUCTURA MÁS COMPLEJA

    // serializar - transformar un objeto de representación en memoria a datos
    // que puede ser texto plano o binario

    // deserializar - transformar un objeto representado en datos (texto plano o binario)
    // a una representación en memoria

    [Serializable]
    public class EstadoDelJuego
    {
        public float nivelDeVida;
        public int nivelActual;
        public string nombreDelJugador;
    }

    [SerializeField]
    private EstadoDelJuego _estado;

    // especificar la ruta donde se va a guardar el archivo
    private string _pathEstado;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _pathEstado = Application.persistentDataPath + "/archivito.json";
        print(_pathEstado);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // INTERACCIÓN CON PLAYERPREFS
    public void SavePlayerPrefs(float valor)
    {
        // dos versiones de playerprefs
        // uno en memoria y otro en sistema de archivos
        
        // cambia un dato en memoria
        PlayerPrefs.SetFloat("llaveEjemplo", valor);

        // si queremos guardarlo a sistema de archivos 
        PlayerPrefs.Save();
    }

    public void LoadPlayerPrefs()
    {
        float valor = PlayerPrefs.GetFloat("llaveEjemplo", -100);
        print(valor);
    }

    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteKey("llaveEjemplo");
    }

    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SaveState()
    {
        // 1er paso - serializar a JSON
        string json = JsonUtility.ToJson(_estado, true);

        // guardamos a archivo 
        File.WriteAllText(_pathEstado, json);

        print("ARCHIVO GUARDADO");
    }

    public void LoadState()
    {
        // SI LEEMOS ARCHIVOS SIEMPRE VERIFICAMOS QUE EXISTAN
        // queremos evitar excepciones en runtime

        if(File.Exists(_pathEstado))
        {
            // proceso al reves que el guardado
            // primero leemos el archivo
            string json = File.ReadAllText(_pathEstado);

            // deserializamos a un objeto
            _estado = JsonUtility.FromJson<EstadoDelJuego>(json);

            print("ESTADO ACTUALIZADO");
        } 
        else
        {
            print("ARCHIVO NO EXISTE");
        }
    }
}
