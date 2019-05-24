using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBankingNums
{
    public class Config
    {
        private ConfigModel CONFIG_MODEL; //json
        private String PATH_PRINT_CONFIG = Config.PathEv("APP_CONFIG.txt");

        public Config()
        {
            try
            {
                //SI EL PATH EXISTE
                if (File.Exists(PATH_PRINT_CONFIG))
                {
                    GET_GILE_CONFIG();
                }
                else
                {
                    SET_FILE_CONFIG();
                }
            }
            catch
            {
                //En el caso de que al leer el archivo de configuracion de un error, entonces crear uno nuevo
                SET_FILE_CONFIG();
            }
            

        }

        //LEYENDO LA CONFIGURACION
        public void GET_GILE_CONFIG()
        {
            SET_CONFIG(File.ReadAllText(PATH_PRINT_CONFIG));
        }

        //PASANDO LA CONFIGURACION
        private void SET_FILE_CONFIG()
        {
            CONFIG_MODEL = new ConfigModel
            {
                URL_FULL_PATH_LINK_NAME = "https://bankingnums.com/login", // Host for default
                URL_HOST_NAME = "https://bankingnums.com", // Host for default
                URL_PATH_NAME = "/login"
            };

            File.WriteAllText(PATH_PRINT_CONFIG, JsonConvert.SerializeObject(CONFIG_MODEL));
        }

        //PSANDO LA CONFIGURACION
        public void SET_CONFIG(string config)
        {
            CONFIG_MODEL = JsonConvert.DeserializeObject<ConfigModel>(config);
            //Guardando la configuracion en el archivo de configuracion
            File.WriteAllText(PATH_PRINT_CONFIG, JsonConvert.SerializeObject(CONFIG_MODEL));
        }

        //OBTENEINDO LA CONFIGURACION
        public ConfigModel GET_CONFIG()
        {
            return CONFIG_MODEL;
        }

        //Entorno de aplicacion
        public static string _pathEV = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\BANKINGNUMS\";
        //Retornando el Path enviroment
        public static string PathEv(string url)
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\BANKINGNUMS\" + url;
        }
    }
}
