using CommandLine;

namespace SaveUp.MigrateDatabase.CLI;

public class CommandlineOptions
{
    [Option(
        shortName: 'c',
        longName: "connectionstring",
        Required = false,
        HelpText = "Die Verbindungszeichenfolge zur Datenbank, auf der die Migration ausgeführt werden soll.")]
    public string Connectionstring { get; set; }
}