using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;

public class SnmpService{
    public SnmpService(){

    }

    public static async Task<List<Variable>> getOperation(string ip){
       var result = Messenger.Get(VersionCode.V1,
                           new IPEndPoint(IPAddress.Parse(ip), 161),
                           new OctetString("public"),
                           [new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.1.0"))],
                           1000);
       return (List<Variable>)result;
    }

    public static async Task<List<Variable>> walkOperation(string ip){
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