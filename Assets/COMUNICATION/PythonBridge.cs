using System.Diagnostics;
using UnityEngine;

public class Ejemplo : MonoBehaviour
{
    [SerializeField]
    public string destino;

    [SerializeField]
    public DataReader reader;

    void Awake()
    {

        destino = PlayerPrefs.GetString("City");

        if (destino != null && destino != "" && destino != "Valencia, Spain")
        {
            // Obtener la ruta completa al script Python en la carpeta Assets
            string rutaScript = Application.dataPath + "/Logics/Datasender.py " + destino.Replace(" ","");

            // Reemplazar las barras invertidas con barras diagonales para compatibilidad
            rutaScript = rutaScript.Replace("\\", "/");
            reader.isVlc = false;
            EjecutarScriptPythonInTerminal(rutaScript);
        }
        else
        {
            if (destino == "Valencia, Spain") reader.isVlc = true;
            else reader.isVlc = false;
        }
    }

    void EjecutarScriptPythonInTerminal(string rutaCompletaAlScript)
    {
        string comandoCmd = $"cmd /k python {rutaCompletaAlScript})";

        UnityEngine.Debug.Log("Comando cmd: " + comandoCmd);

        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "cmd.exe";
        startInfo.Arguments = $"/k {comandoCmd}";  // /k mantiene la ventana de cmd abierta después de la ejecución

        using (Process process = new Process())
        {
            process.StartInfo = startInfo;

            try
            {
                process.Start();
                process.WaitForExit();  // Esperar a que el proceso de cmd termine antes de continuar

                UnityEngine.Debug.Log("Proceso de cmd terminado");
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogError("Error al ejecutar cmd: " + ex.Message);
            }
        }
    }
}
