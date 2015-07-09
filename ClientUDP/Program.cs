using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientUDP
{
    class Program
    {
        static void Main(string[] args)
        {
            // Adresse du serveur à joindre
            IPAddress ip_addressServer = IPAddress.Parse("192.168.211.54");
            String userString = "", serveurRetour;
            byte[] b_userString;
            byte[] b_serverStringResponse = new byte[255];
            Socket sock = null;

            // userData
            Console.WriteLine("Saisir une chaine\n");
            userString = Console.ReadLine();

            try
            {
                // Création de la socket
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                // Connexion au serveur (PAS de connect)
                IPEndPoint serveur = new IPEndPoint(ip_addressServer, 6000);
                EndPoint serveurRemote = (EndPoint)serveur;

                //Envoie des données
                b_userString = Encoding.UTF8.GetBytes(userString);
                sock.SendTo(b_userString, serveur);
                sock.ReceiveFrom(b_serverStringResponse, ref serveurRemote);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }
            finally
            {
                // Fermeture de la connexion
                if (sock != null)
                {
                    sock.Shutdown(SocketShutdown.Both);
                    sock.Close();
                }
            }

            serveurRetour = Encoding.UTF8.GetString(b_serverStringResponse);

            
            Console.WriteLine(serveurRetour);
            Console.ReadKey();
        }
    }
}
