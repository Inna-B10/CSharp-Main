using System;
using Microsoft.VisualBasic;

namespace linq_oppgavegjennomgang.Model;

public class CsvModel
{
    public DateTime Date {get;set;}
    public string City {get;set;}
    public string State {get;set;}
    public string Country {get;set;}
    public string Shape {get;set;}
    public int Duration {get;set;}
    public string Comments {get;set;}
    public DateOnly DatePosted {get;set;}
    public double Lat {get;set;}
    public double Long {get;set;}
    public CsvModel(string csv)
    {
        var csvData = csv.Split(",");
        if (DateTime.TryParse(csvData[0], out var res1))
        {
            this.Date = res1;
        }
        this.City = csvData[1];
        this.State = csvData[2];
        this.Country = csvData[3];
        this.Shape = csvData[4];
        if (int.TryParse(csvData[5], out var res2))
        {
            this.Duration = res2;
        }
        this.Comments = csvData[7];
        if (DateOnly.TryParse(csvData[8], out var res3))
        {
            this.DatePosted = res3;
        }
        if (double.TryParse(csvData[9], out var res4))
        {
            this.Lat = res4;
        }
        if (double.TryParse(csvData[10], out var res5))
        {
            this.Long = res5;
        }
    }
}
