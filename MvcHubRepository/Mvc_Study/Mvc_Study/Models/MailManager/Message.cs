using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaiMvc.Models.MailManager.Mail
{
    //PUBLIC, una routine diventa accessibile da tutte le istanze della classe.
    public class Message
    {
        /// <summary>
        /// definiamo le proprietà per il nostro messaggio
        /// </summary>
        public string Title { get; set; }
        public string Body { get; set; }
        public List<string> ImageList { get; set; }

        //PRIVATE, impedisce che la routine sia visibile al di fuori della classe di in cui è definita.
        private string Template { get; set; }

        // COSTRUTTORE ha lo stesso nome della classe e non restituisce alcun valore 

        /// <COSTRUTTORE>
        /// Il costruttore è COSTRUTTORE per impostare le proprietà che la classe deve avere al momento della sua creazione: 
        /// nel nostro esempio, quando si crea una istanza della classe Message, le sue variabili 
        /// Title, Body, ImageList e Template assumono i valori specificati nel relativo argomento del costruttore.
        /// </COSTRUTTORE>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <param name="templateHTML"></param>
        public Message(string title, string message, string templateHTML)
        {
            this.Title = title;
            this.Template = templateHTML;
            this.Body = CreateBody(message);
        }

        public Message(string title, string message, string templateHTML, List<string> imageList)
        {
            this.Title = title;
            this.Template = templateHTML;
            this.Body = CreateBody(message);
            this.ImageList = imageList;
        }

        /// <summary>
        /// Costruisce il messaggio in base al template
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public string CreateBody(string body)
        {
            string temp = this.Template;

            temp = temp.Replace("[TITLE]", this.Title);
            temp = temp.Replace("[MESSAGE]", body);

            return temp;
        }
    }
}