using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using MaiMvc.Models.MailManager.Mail;
using System.Configuration;
using System.Net.Mime;

namespace MaiMvc.Models
{
    public class Mail
    {
        #region Property
        /// <summary>
        /// Definiamo tutte le proprietà che deve avere la nostra Mail
        /// </summary>
        Message message = null;

        string smtpHost = string.Empty;
        int port = 25;

        // Chi invia la mail
        string mailSender = string.Empty;

        // Lista di destinatari (TO)
        List<string> lstDestinatari = null;

        // Lista Copia Conoscenza (CC)
        List<string> lstCc = null; 
        #endregion

        
        public Mail(Message message, string smtpHost, string mailSender, int port,
            List<string> lstTo, List<string> lstCc)
        {
            // La parola chiave THIS si riferisce all'istanza corrente della classe e viene anche utilizzata 
            // come modificatore del primo parametro di un metodo di estensione
            this.message = message;
            this.lstDestinatari = lstTo;
            this.lstCc = lstCc;

            this.mailSender = mailSender;
            this.smtpHost = smtpHost;
            this.port = port;

            //CONFIGURATION MANAGER, ottiene i dati di AppSettingsSection per la configurazione predefinita dell'applicazione corrente
            if (port == 0)
                this.mailSender = ConfigurationManager.AppSettings.Get("Port");

            if (mailSender.Length == 0)
                this.mailSender = ConfigurationManager.AppSettings.Get("mailSender");

            if (smtpHost.Length == 0)
                this.smtpHost = ConfigurationManager.AppSettings.Get("smtpHost");
        }

        /// <summary>
        /// Invia la mail
        /// AlternateView: Rappresenta il formato in cui visualizzare un messaggio di posta elettronica.
        /// </summary>
        public void Send()
        {
            try
            {
                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(message.Body, null, MediaTypeNames.Text.Html);

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(this.mailSender);
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                mail.Subject = message.Title;
                mail.Body = message.Body;

                if (this.lstDestinatari != null)
                    foreach (string s in this.lstDestinatari)
                        mail.To.Add(s);

                if (this.lstCc != null)
                    foreach (string s in this.lstCc)
                        mail.CC.Add(s);

                for (int i = 0; i < message.ImageList.Count; i++)
                {
                    string s = message.ImageList[i];
                    var inlineLogo = new LinkedResource(s, MediaTypeNames.Image.Gif);
                    inlineLogo.ContentId = string.Format("[IMAGE{0}]", i);

                    avHtml.LinkedResources.Add(inlineLogo);
                }
                mail.AlternateViews.Add(avHtml);

                SmtpClient smtp = new SmtpClient(this.smtpHost, this.port);
                smtp.Send(mail);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}