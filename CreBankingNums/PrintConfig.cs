using Newtonsoft.Json;
using System;
using System.IO;

namespace CoreBankingNums
{
    class PrintConfig
    {

        private PrintModel PRINT_MODEL; //json
        private readonly String PATH_PRINT_CONFIG  = Config.PathEv("PRINT_CONFIG.txt");

        //Variables de obteneci
        public PrintConfig()
        {
            //SI EL PATH EXISTE
            if (File.Exists(PATH_PRINT_CONFIG))
            {
                SET_CONFIG(File.ReadAllText(PATH_PRINT_CONFIG));
            }
            else
            {
                PRINT_MODEL = new PrintModel();
                File.WriteAllText(PATH_PRINT_CONFIG, JsonConvert.SerializeObject(PRINT_MODEL));
            }

        }

        public void SET_CONFIG(string config)
        {
            PRINT_MODEL = JsonConvert.DeserializeObject<PrintModel>(config);
            //Guardando la configuracion en el archivo de configuracion
            File.WriteAllText(PATH_PRINT_CONFIG, JsonConvert.SerializeObject(PRINT_MODEL));
        }

        public PrintModel GET_CONFIG()
        {
            return PRINT_MODEL;
        }

    }
}
