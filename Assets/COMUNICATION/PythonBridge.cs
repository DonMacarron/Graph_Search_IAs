using System;
using System.Diagnostics;
using UnityEngine;

public class PythonBridge : MonoBehaviour
{
    [SerializeField]
    public string destino;

    [SerializeField]
    public DataReader reader;

    void Awake()
    {
        destino = PlayerPrefs.GetString("City");
        UnityEngine.Debug.Log("Destino:  "+destino);
        if (destino != null && destino != "" && destino != "Valencia, Spain")
        {
            string rutaScriptPython = "Assets\\Logics\\Datasender.py ";
            string argsPython = destino.Replace(" ", "");
            UnityEngine.Debug.Log("Destino:  " + destino);

            reader.isVlc = false;
            executePythonCityIni(rutaScriptPython, argsPython);
        }
        else
        {
            if (destino == "Valencia, Spain") reader.isVlc = true;
            else reader.isVlc = false;
        }
    }

    void executePythonCityIni(string rutaScriptPython, string argumentos)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "python";  
        startInfo.Arguments = $"{rutaScriptPython} {argumentos}";
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardError = true;
        startInfo.UseShellExecute = false;
        //startInfo.CreateNoWindow = true;

        using (Process process = new Process())
        {
            process.StartInfo = startInfo;

            try
            {
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                UnityEngine.Debug.Log("Output: " + output);
                UnityEngine.Debug.Log("Error: " + error);

                process.WaitForExit();

                UnityEngine.Debug.Log("Proceso de Python terminado");
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.Log("Error al ejecutar Python: " + ex.Message);
            }
        }
    }
}
