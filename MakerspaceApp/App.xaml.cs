using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using System.Windows;
using GS.SCard;
using GS.Util.Hex;
using System.IO;

namespace MakerspaceApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //public MakerspaceDataDataContext db;
        public static string UID;
        public static string UNI;
        public static List<string> adminsEmails = new List<string>();

        private void Application_StartUp(object sender, StartupEventArgs e)
        {

            try
            {
                //make email field in db 
                //this is just to test 
                using (StreamReader sr = new StreamReader("C:\\Users\\barriosj\\documents\\visual studio 2013\\Projects\\MakerspaceApp\\MakerspaceApp\\Admin.txt"))
                {
                    while (sr.Peek() > -1)
                    {
                        adminsEmails.Add(sr.ReadLine());
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

         MainWindow mw = new MainWindow();
        mw.Show();
          //UserInventory  = new UserInventory();
        //  ui.Show();
        }


        public static string cardProtocol()
        {

            WinSCard scard = new WinSCard();
            try
            {
                
                scard.EstablishContext();
                scard.ListReaders();
                string readerName = scard.ReaderNames[1];
                scard.WaitForCardPresent(readerName);

                
                scard.Connect(readerName);
                byte[] cmdApdu = { 0xFF, 0xCA, 0x00, 0x00, 00 }; // Get Card UID ...
                byte[] respApdu = new byte[10];
                int respLength = respApdu.Length;

                scard.Transmit(cmdApdu, cmdApdu.Length, respApdu, ref respLength);

                //find a better place for this
                App.UID = HexFormatting.ToHexString(respApdu, true);
               
                //he wanted some kinda beeping sound when someone swipes their card
                System.Media.SystemSounds.Beep.Play();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return App.UID;
        }
        public static void  appReset(){
            App.UNI = null;
            App.UID = null;
            
        }
        public static Boolean isValid()
        {
          
           MakerspaceDataDataContext db = new MakerspaceDataDataContext();
            User userInQuestion;
            
                var data = from User in db.Users
                          where User.Id == App.UID
                          select User;
           var array= data.ToArray<User>();
          userInQuestion=  array[0];
            string tempUNI=userInQuestion.UNI;
            string tempUID=userInQuestion.Id;

            MessageBox.Show(App.UID);
            MessageBox.Show(tempUID);
                if ( App.UID.Trim()== tempUID.Trim())
                {
                    App.UNI = tempUNI;

                    return true;
                }
                else
                {
                    MessageBox.Show("Wrong ID");
                    App.appReset();
                    return false;
                }

        }

        public static Boolean RepeatedUID(string UID)
        {
            MakerspaceDataDataContext db = new MakerspaceDataDataContext();
            var data = from User in db.Users
                       select User.Id;
            var list = data.ToList<String>();
            if (list.Contains(UID))
            {
                //this must be a repeated UID
                MessageBox.Show("repeat UID");
                return true;
            }
            else
            {
                return false;
            }

        }

        public static void recordTran()
        {
            MakerspaceDataDataContext db = new MakerspaceDataDataContext();

            Tran trans = new Tran { UNI=App.UNI, date=DateTime.Now , Id=System.Guid.NewGuid() };
            db.Trans.InsertOnSubmit(trans);

            try { db.SubmitChanges(); }
            catch (Exception ex){
                MessageBox.Show(ex.ToString());
            }


                           
        }

        public static void recordTran(string item, int amt)
        {
            MakerspaceDataDataContext db = new MakerspaceDataDataContext();
            Tran trans = new Tran { UNI = App.UNI, amount = amt, ItemName = item, date = DateTime.Now, Id = System.Guid.NewGuid() };
            db.Trans.InsertOnSubmit(trans);
            try { db.SubmitChanges(); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        public static void subtractFromInventory(string item,int amtTaken )
        {

            MakerspaceDataDataContext db = new MakerspaceDataDataContext();
            var inv = from e in db.Inventories
                      where e.ItemID == item
                      select e;
          
            foreach (var i in inv)
            {
                i.AmountInStock = i.AmountInStock - amtTaken;
                MessageBox.Show(i.AmountInStock.ToString());
//===========================================================================
                if (i.AmountInStock < 0)
                {
                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                    foreach (string email in adminsEmails)
                    {
                        mail.To.Add(email);
                    };
                    mail.From = new System.Net.Mail.MailAddress("jonathanbarrios81@gmail.com");
                   
                    mail.Subject = "We have run out of " + i.ItemID+" -> this note was auto-generated by the Makerspace app as a TEST";
                    mail.Body = " last transaction info-> UNI:" + App.UNI + "AmountTaken:" + amtTaken + "Time:" + DateTime.Now;
                    System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient();
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("jonathanbarrios81", "******");
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Host = "smtp.gmail.com";
                    SmtpServer.Send(mail);

                    //client.Send(mail);
//================================================================================
                }//=============
            }


             //--------------------------------------------------------------------------------
            //========================================================================================*/
            try
            {
                //MessageBox.Show("hi");
                db.SubmitChanges();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());

            }

        }

        


    }
}
