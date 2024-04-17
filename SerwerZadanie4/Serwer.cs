using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [ServiceContract]
    public interface IZadanie2
    {
        [OperationContract]
        string Test(string arg);
    }

    public class Zadanie2 : IZadanie2
    {
        public string Test(string arg)
        {
            return $"argument: {arg}";
        }
    }

    public class Server
    {
        public static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(Zadanie2));
            var metadata = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (metadata == null)
            {
                metadata = new ServiceMetadataBehavior();
            }
            host.Description.Behaviors.Add(metadata);
            host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexNamedPipeBinding(), "net.pipe://localhost/metadata");
            host.AddServiceEndpoint(typeof(IZadanie2), new NetNamedPipeBinding(), "net.pipe://localhost/ksr-wcf1-zad2");
            host.AddServiceEndpoint(typeof(IZadanie2), new NetTcpBinding(), "net.tcp://127.0.0.1:55765");

            host.Open();
            Console.WriteLine("Host opened");


            Console.ReadLine();
            host.Close();
            Console.WriteLine("Host closed");
            Console.ReadLine();
        }
    }
}