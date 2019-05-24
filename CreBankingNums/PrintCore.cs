using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreBankingNums
{
    public class PrintCore
    {
        //Clase de configuracion de la impresora aplicacion
        private PrintConfig PrintConfig = null;
        //Clase PrintDocument
        private PrintDocument PrintDocument = null; //incializando el printer

        //Variables
        private string[] PRINT_TEXT = null;
        private float x, y;  //definiendo las coordenadas
        private int FONT_SIZE, COUNT_PRINTING = 0; // definiendo el tama;o de la funte
        private string VALUE_PRINT, FAMILY_NAME; //definiendo el valor y fuente de texto
        private FontStyle FONT_STYLE;

        public PrintCore()
        {
            //Psando los datos de configuracion
            PrintConfig = new PrintConfig();
            //PrintDocuemnt
            PrintDocument = new PrintDocument();
            //Configuracion de la imprecion
            PrintDocument = new PrintDocument();
            PrintDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            //Pasando el nombre de la impresora
            PrintDocument.PrinterSettings.PrinterName = PrintConfig.GET_CONFIG().NAME;

        }
        
        //ENVIANDO
        public void setReference(string data)
        {
            //Pasando la configuracion 
            PrintConfig.SET_CONFIG(data);
            //Refrezcando la configuracion
            PrintConfig = new PrintConfig();
            //Pasando el nombre de la impresora
            PrintDocument.PrinterSettings.PrinterName = PrintConfig.GET_CONFIG().NAME;
        }

        //Listado de printer instalados
        public string devices()
        {
            String val = "";

            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                val += "|" + PrinterSettings.InstalledPrinters[i];
            }

            return val;
        }

        //Enviando a imprimi
        public void send(String txt)
        {
            PRINT_TEXT = txt.Split('~');

            PrintDocument.Print();
            //Limpiando
            COUNT_PRINTING = 0;
            Array.Clear(PRINT_TEXT, 0, PRINT_TEXT.Length);

        }

        //Main print
        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (PrintConfig.GET_CONFIG().TYPE_PRINTING == 0)
            {
                Driver_Core(e, PRINT_TEXT[COUNT_PRINTING]);

            }
            else if (PrintConfig.GET_CONFIG().TYPE_PRINTING == 1)
            {
                e.Graphics.DrawString(PRINT_TEXT[COUNT_PRINTING], new Font(PrintConfig.GET_CONFIG().FAMILY_NAME, PrintConfig.GET_CONFIG().FONT_SIZE), Brushes.Black,
                       0, 0, new StringFormat());

            }
            //Contador
            COUNT_PRINTING += 1;
            //Imprimiendo vas veces
            e.HasMorePages = (PRINT_TEXT.Length != COUNT_PRINTING);

        }
        
        //Imprimir con Driver Actualizado
        private void Driver_Core(PrintPageEventArgs e, string val)
        {
            //_arrays_obj
            int isave_ = 0;
            int _out = 0;
            
            int MARGIN_BOTTOM = 0; //Margin Bottom
            int MARGIN_TOP = 0; //Margin top

            String[] frame, Separate, _obj = val.Split(';'); //Almacenando los datos de imrecion

            try
            {
                for (int i = 0; i < _obj.Length; i++)
                {
                    bool text_center = false;
                    frame = _obj[i].Split('|');

                    try
                    {
                        for (int i_ = 0; i_ < frame.Length; i_++)
                        {
                            Separate = frame[i_].Split('=');

                            if (Separate[0].ToLower() == "x")
                            {
                                if (Separate[1] == "center")
                                {
                                    text_center = true;
                                }
                                else if (int.TryParse(Separate[1], out _out))
                                {
                                    x = float.Parse(Separate[1]);
                                }
                                else
                                {
                                    x = 0;
                                }

                            }
                            else if (Separate[0].ToLower() == "y")
                            {
                                if (Separate[1] == "+")
                                {
                                    isave_++;
                                    y = isave_ * 20;
                                }
                                else if (Separate[1] != "#")
                                {
                                    if (int.TryParse(Separate[1], out _out))
                                    {
                                        y = float.Parse(Separate[1]);
                                    }
                                    else
                                    {
                                        y = 0;
                                    }
                                }
                            }
                            else if (Separate[0].ToLower() == "fontsize")
                            {
                                FONT_SIZE = int.Parse(Separate[1]);
                            }
                            else if (Separate[0].ToLower() == "fontstyle")
                            {
                                if (Separate[1].ToLower() == "bold")
                                {
                                    FONT_STYLE = FontStyle.Bold;
                                }
                                else if (Separate[1].ToLower() == "italic")
                                {
                                    FONT_STYLE = FontStyle.Italic;
                                }
                                else if (Separate[1].ToLower() == "strikeout")
                                {
                                    FONT_STYLE = FontStyle.Strikeout;
                                }
                                else if (Separate[1].ToLower() == "underline")
                                {
                                    FONT_STYLE = FontStyle.Underline;
                                }
                                else
                                {
                                    FONT_STYLE = FontStyle.Regular;
                                }
                            }
                            else if (Separate[0].ToLower() == "val")
                            {
                                VALUE_PRINT = Separate[1];
                            }
                            else if (Separate[0].ToLower() == "font")
                            {
                                FAMILY_NAME = Separate[1];
                            }
                            else if (Separate[0].ToLower() == "mb")
                            {
                                MARGIN_BOTTOM = int.Parse(Separate[1]);
                            }
                            else if (Separate[0].ToLower() == "mt")
                            {
                                MARGIN_TOP = int.Parse(Separate[1]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("[0]Ocurrio algun error. Mas detallado: " + ex.Message);
                        break;
                    }
                    finally
                    {
                        if (VALUE_PRINT != null)
                        {
                            //70 - 100
                            float alignment_x = text_center ? VALUE_PRINT.Length >= 37 ? 0 : ((100 - VALUE_PRINT.Length + 25) - (VALUE_PRINT.Length + (FONT_SIZE * 2))) : x;
                            //Fonz
                            Font font = new Font(FAMILY_NAME, FONT_SIZE, FONT_STYLE);
                            //Dibujando
                            e.Graphics.DrawString(VALUE_PRINT, font, Brushes.Black, alignment_x, y, new StringFormat());
                            //Resetando font style
                            FONT_STYLE = FontStyle.Regular;
                        }
                    }

                }

            }
            catch (Exception exp)
            {
                MessageBox.Show("[1]Ocurrio algun error. Mas detallado: " + exp.Message);
            }
            finally
            {
                //liberando las variables
                _obj = frame = Separate = null;
                VALUE_PRINT = FAMILY_NAME = null;
                FONT_SIZE = 0;
                x = y = 0;
            }

        }//Final del core

    }
}
