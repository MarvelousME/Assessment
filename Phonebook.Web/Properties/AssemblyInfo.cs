using System.Reflection;
using System.Runtime.InteropServices;

// Le informazioni generali relative a un assembly sono controllate dal seguente
// set di attributi. Modificare i valori di questi attributi per modificare le informazioni
// associate a un assembly.
[assembly: AssemblyTitle("Marvin Saunders")]
[assembly: AssemblyDescription("Assessment Project to create simple Phonebook application")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Saunders Consulting")]
[assembly: AssemblyProduct("Assessment")]
[assembly: AssemblyCopyright("Copyright © Saunders Consulting 2019")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Se si imposta il valore di ComVisible su falso, i tipi nell'assembly non sono più visibili
// dai componenti COM. Se è necessario accedere al tipo nell'assembly da
// COM, impostare su true l'attributo ComVisible per tale tipo.
[assembly: ComVisible(false)]

// Se il progetto viene esposto a COM, il GUID seguente verrà utilizzato per creare l'ID di typelib
[assembly: Guid("ed64ee3d-74e0-48dc-9e8e-6589e1661ebb")]

// Le informazioni sulla versione di un assembly sono costituite dai quattro valori seguenti:
//
//      Versione principale
//      Versione secondaria
//      Numero build
//      Revisione
//
// È possibile specificare tutti i valori o lasciare i valori predefiniti per Revisione e Numeri build
// utilizzando l'asterisco (*) come illustrato di seguito:
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
