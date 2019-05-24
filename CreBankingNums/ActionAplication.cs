using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreBankingNums
{
    public class ActionAplication
    {

        public string info()
        {
            return "{version: 1.4.0, type: 'Windows'}";
        }

        public void shutdownapp()
        {
            Application.Exit();
        }

        public bool ExistFile(string path)
        {
            return (System.IO.File.Exists(path));
        }

        public void restartapp()
        {
            Application.Restart();
        }
        //Si existe acceso al internet
        public bool AccessNet()
        {
            try
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.google.com");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void ExecuteCommand(string _Command)
        {
            //Indicamos que deseamos inicializar 
            //el proceso cmd.exe junto a un comando de arranque. 
            ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + _Command);
            // Indicamos que la salida 
            //del proceso se redireccione en un Stream
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            //Indica que el proceso no despliegue 
            //una pantalla negra (El proceso se ejecuta en background).
            procStartInfo.CreateNoWindow = false;
            //Inicializa el proceso
            Process proc = new Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            //Consigue la salida de la Consola(Stream) 
            //y devuelve una cadena de texto
            string result = proc.StandardOutput.ReadToEnd();
            //Muestra en pantalla la salida del Comando
            Console.WriteLine(result);
        }

    }
}
