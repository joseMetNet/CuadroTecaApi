using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace fotoTeca.Email
{
    public class EnviarEMail
    {
        public string mailDestinatario { get; set; }
        public string asunto { get; set; }
        public string cuerpo { get; set; }
        public string mailOrigen { get; set; }
        public string nombreOrigen { get; set; }
        public string usuario { get; set; }
        public string contrasena { get; set; }
        public bool mailEnviado { get; set; }

        //Envía un correo electrónico con los datos pasados por setter o bien en el constructor de la clase
        public void enviarEmail()
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    var fromAddress = new MailAddress(mailOrigen, "CuadroTeca");
                    mail.From = new MailAddress(mailOrigen);
                    string[] coreodes = mailDestinatario.Split(";");
                    foreach (var email in coreodes)
                    {
                        mail.To.Add(new MailAddress(email));
                    }
                    mail.Subject = asunto;
                    mail.Body = cuerpo;
                    mail.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.UseDefaultCredentials = false;
                        //smtp.Credentials = new NetworkCredential(fromAddress.Address, contrasena);
                        smtp.Credentials = new NetworkCredential("cuadrotecasoporte@gmail.com", "ASDF1234ss");
                        smtp.EnableSsl = true;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Timeout = 30000;
                        smtp.Send(mail);
                        mailEnviado = true;
                    }
                }
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                mailEnviado = false;
                throw new ArgumentException("Se genero error al enviar el Correo:" + ex.Message);
            }
        }

        public void EnviarCorreo(string cuerpo, string conectionString, string pasuto, string pDestinatarios)
        {
            EnviarEMail eMail = new EnviarEMail();
            eMail.mailDestinatario = pDestinatarios;
            eMail.asunto = pasuto;
            eMail.cuerpo = cuerpo;
            ValorConfiguracion emailNotinficacio = new ValorConfiguracion("EmailNotificacionCuadroTeca", conectionString);
            eMail.mailOrigen = emailNotinficacio.Valor;
            eMail.nombreOrigen = "CuadroTeca";
            eMail.usuario = emailNotinficacio.Valor;
            eMail.contrasena = new ValorConfiguracion("ClaveNotificacionCuadroTeca", conectionString).Valor;
            eMail.enviarEmail();
        }

        public String cuerpoRecuperarContraseña(string firstName, string passNew)
        {
            string strBody = "<HTML>";
            strBody += "<head><style type=\"text/css\">.curpointer {cursor: pointer;}</style></head> ";
            strBody += "<Body style='font-family: Arial, icomoon, sans-serif; font-size: 12px; color: #1F1F1F'> ";
            strBody += "<p> ¡Hola " + firstName + "!</p>";
            strBody += "<p> Te informamos que has solicitado un cambio de contraseña en nuestra plataforma.</p>";
            strBody += "<p> Tu nueva contraseña es la siguiente " +passNew + " </p>";
            strBody += "<p>&iexcl;Gracias!</p>";
            strBody += "<p>Atentamente Equipo <b>CuadroTeca</b>.</p>";
            strBody += "<img src=\'http://cuadrotecanueva.azurewebsites.net/assets/img/logo-cuadroteca.png' width = \"300\" height=\"180\" alt=\"logo\">";
            strBody += "<p>Si no has solicitado este correo por favor ignoralo.</p>";
            strBody += "<br><br>";
            strBody += "</Body>";
            strBody += "</HTML>";
            //strBody = strBody.Replace("{1}", celular);
            return strBody;
        }
        public String cuerpoComprador( string Fullname, string NameStatus)
        {
            string strBody = "<HTML>";
            strBody += "<head><style type=\"text/css\">.curpointer {cursor: pointer;}</style></head> ";
            strBody += "<Body style='font-family: Arial, icomoon, sans-serif; font-size: 12px; color: #1F1F1F'> ";
            strBody += "<p> ¡Hola " + Fullname + "!</p>";
            strBody += "<p> Te informamos que la compra que has realizado en nuestra plataforma ha sido exitosa.</p>";
            strBody += "<p> el estado actual de tu producto es <b> "+ NameStatus +" </b></p>";
            strBody += "<p> " + Fullname + " te estaremos informando sobre el proceso de tu compra</p>";
            strBody += "<p>&iexcl;Gracias!</p>";
            strBody += "<p>Atentamente Equipo <b>CuadroTeca</b>.</p>";
            strBody += "<img src=\'http://cuadrotecanueva.azurewebsites.net/assets/img/logo-cuadroteca.png' width = \"300\" height=\"180\" alt=\"logo\">";
            strBody += "<p>Si no has solicitado este correo por favor ignorelo.</p>";
            strBody += "<br><br>";
            strBody += "</Body>";
            strBody += "</HTML>";
            //strBody = strBody.Replace("{1}", celular);
            return strBody;
        }

        public String cuerpoAdmins(string Fullname, int idOrder, string NameStatus)
        {
            string strBody = "<HTML>";
            strBody += "<head><style type=\"text/css\">.curpointer {cursor: pointer;}</style></head> ";
            strBody += "<Body style='font-family: Arial, icomoon, sans-serif; font-size: 12px; color: #1F1F1F'> ";
            strBody += "<p> ¡Hola administrador" + Fullname + "!</p>";
            strBody += "<p> Te informamos que han realizado una nueva compra en la plataforma.</p>";
            strBody += "<p> el estado actual del producto es <b> " + NameStatus + " </b> el identificador de esta orden es el" + idOrder +"</p>";
            strBody += "<p> " + Fullname + " te estaremos informando sobre el proceso este producto</p>";
            strBody += "<p>&iexcl;Gracias!</p>";
            strBody += "<p>Atentamente Equipo <b>CuadroTeca</b>.</p>";
            strBody += "<img src=\'http://cuadrotecanueva.azurewebsites.net/assets/img/logo-cuadroteca.png' width = \"300\" height=\"180\" alt=\"logo\">";
            strBody += "<p>Si no has solicitado este correo por favor ignorelo.</p>";
            strBody += "<br><br>";
            strBody += "</Body>";
            strBody += "</HTML>";
            //strBody = strBody.Replace("{1}", celular);
            return strBody;
        }

    }
}
