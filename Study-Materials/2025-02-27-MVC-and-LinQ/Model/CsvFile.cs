using System;

namespace linq_oppgavegjennomgang.Model;

public class CsvFile
{
    public List<CsvModel> Rows {get;set;} = [];
    public CsvFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return;
        }
        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            Rows.Add(new CsvModel(line));
        }
    }
}
