using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;

public class SnmpService{
    public SnmpService(){

    }

    public static List<Variable> GetOperation(string ip){
       var result = Messenger.Get(VersionCode.V2,
                           new IPEndPoint(IPAddress.Parse(ip), 161),
                           new OctetString("public"),
                           [new Variable(new ObjectIdentifier("1.3.6.1.2.1.25.3.2.1.5.1"))],
                           1000);
       return (List<Variable>)result;
    }

    public static List<Variable> WalkOperation(string ip){
            var result = new List<Variable>();
            Messenger.Walk(VersionCode.V1,
               new IPEndPoint(IPAddress.Parse(ip), 161),
               new OctetString("public"),
               new ObjectIdentifier("1.3.6.1.2.1.43.11.1.1.6"),
               result,
               1000,
               WalkMode.WithinSubtree);
        return result;
      
    }
}