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
                           3000);
       return (List<Variable>)result;
    }



    public static List<Variable> BulkOperation(string ip){

        var bulkPdu = new List<Variable>
{
    // =========================================================================
    // SCALAIRES UNIVERSELS (Placer nonRepeaters = 3 dans la requête GetBulk)
    // =========================================================================
    new Variable(new ObjectIdentifier("1.3.6.1.2.1.43.5.1.1.1.1")),   // prtGeneralStatus : Statut Printer-MIB (ex: idle, printing)
    new Variable(new ObjectIdentifier("1.3.6.1.2.1.25.3.2.1.5.1")),  // hrDeviceStatus : Statut Host-Resources (repli constructeurs)
    new Variable(new ObjectIdentifier("1.3.6.1.2.1.43.18.1.8.1")),   // prtAlertDescription : Description de l'alerte courante

    // =========================================================================
    // TABLES / ARBRES (Repétitions gérées par maxRepetitions)
    // =========================================================================

    // --- Consommables Standard ---
    new Variable(new ObjectIdentifier("1.3.6.1.2.1.43.11.1.1.4")),   // prtMarkerSuppliesType : Type de consommable
    new Variable(new ObjectIdentifier("1.3.6.1.2.1.43.11.1.1.6")),   // prtMarkerSuppliesDescription : Nom/Description du consommable
    new Variable(new ObjectIdentifier("1.3.6.1.2.1.43.11.1.1.8")),   // prtMarkerSuppliesMaxCapacity : Capacité maximale
    new Variable(new ObjectIdentifier("1.3.6.1.2.1.43.11.1.1.9")),   // prtMarkerSuppliesLevel : Niveau actuel

    // --- Alertes Standard ---
    new Variable(new ObjectIdentifier("1.3.6.1.2.1.43.18.1.2")),     // prtAlertSeverity : Sévérité des alertes

    // --- Extensions Lexmark (1.3.6.1.4.1.641) ---
    new Variable(new ObjectIdentifier("1.3.6.1.4.1.641.2.1.1.1.2")), // lexmarkConsumableName : Nom consommable Lexmark
    new Variable(new ObjectIdentifier("1.3.6.1.4.1.641.2.1.1.1.3")), // lexmarkConsumableDescription : Description Lexmark
    new Variable(new ObjectIdentifier("1.3.6.1.4.1.641.2.1.1.1.4")), // lexmarkConsumableType : Type Lexmark
    new Variable(new ObjectIdentifier("1.3.6.1.4.1.641.2.1.1.1.6")), // lexmarkConsumableLevel : Niveau Lexmark
    new Variable(new ObjectIdentifier("1.3.6.1.4.1.641.2.1.1.1.7")), // lexmarkConsumableClassUnit : Unité de mesure Lexmark
    new Variable(new ObjectIdentifier("1.3.6.1.4.1.641.6.5.1.1.6")), // lexmarkAlertDescription : Texte alerte Lexmark
    new Variable(new ObjectIdentifier("1.3.6.1.4.1.641.6.5.1.1.7")), // lexmarkAlertSrvCode : Code service Lexmark
    new Variable(new ObjectIdentifier("1.3.6.1.4.1.641.6.5.1.1.9")), // lexmarkAlertSeverity : Sévérité Lexmark (3=Erreur, 4=Alerte)

    // --- Extensions Brother (1.3.6.1.4.1.2435) ---
    new Variable(new ObjectIdentifier("1.3.6.1.4.1.2435.2.3.9.4.2.1.5.5.8")), // Chaine hexadécimale toners Brother (81=K, 82=C, 83=M, 84=Y)

    // --- Extensions Epson (1.3.6.1.4.1.1248) ---
    new Variable(new ObjectIdentifier("1.3.6.1.4.1.1248.1.1.2.1.3.1.1")),     // epsonConsumableDescription : Nom consommable Epson
    new Variable(new ObjectIdentifier("1.3.6.1.4.1.1248.1.1.2.1.3.1.2")),     // epsonConsumableLevel : Niveau Epson (Ignorer si < 0)
    new Variable(new ObjectIdentifier("1.3.6.1.4.1.1248.1.1.2.1.3.1.3"))      // epsonConsumableCapacity : Capacité max Epson
};




            GetBulkRequestMessage message = new(0,
                                                          VersionCode.V2,
                                                          new OctetString("public"),
                                                          3,
                                                          10,
                                                           bulkPdu
                                                          );
ISnmpMessage response = message.GetResponse(1000, new IPEndPoint(IPAddress.Parse(ip), 161));

var result = response.Pdu().Variables;

        return (List<Variable>)result;

    }
}
