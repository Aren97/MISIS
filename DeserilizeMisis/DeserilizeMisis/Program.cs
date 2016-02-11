using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

namespace DeserilizeMisis
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateGetInfoFile CGIF = new CreateGetInfoFile();
            DeSerialization DS = new DeSerialization();
            DS.Deserialize(CGIF.Creat());
        }
    }

    public class BasketballPlayers
    {
        public string PlayerName { get; set; }
        public string Team { get; set; }
        public int Score { get; set; }
    }

    public class CreateGetInfoFile
    {
        public WebResponse Answer;
        public Stream Creat()
        {
            WebRequest inquiry = WebRequest.Create("http://api.lod-misis.ru/testassignment");
            Answer = inquiry.GetResponse();
            Stream stream = Answer.GetResponseStream();
            return stream;
        }
    }

    public class DeSerialization
    {
        public void Deserialize (Stream stream)
        {
            DataContractJsonSerializer DCJS = new DataContractJsonSerializer(typeof(List<BasketballPlayers>));
            List<BasketballPlayers> Basket = (List<BasketballPlayers>)DCJS.ReadObject(stream);

            var team = from i in Basket group i by i.Team;

            foreach(var group in team)
            {
                var Score = from i in @group orderby i.Score descending select i;
                Console.WriteLine("|-------------------------------|");
                Console.WriteLine("|Team :" + group.Key + "|");
                Console.WriteLine("|-------------------------------|");
                Console.WriteLine("|Player Name|" + "\t   |Total score|");
                Console.WriteLine("|-------------------------------|");
                foreach (var Bask in Score)
                {
                    Console.WriteLine("|" + Bask.PlayerName + "\t  |"+Bask.Score);
                    Console.WriteLine("|-------------------------------|");
                }
            }
            Console.ReadKey();
        }
    }

}
