using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicationProcess.December2020.Domain.Entities
{
    public class ExternalServiceHttpResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }

    public class Country
    {
        public string Name { get; set; }
        public List<string> TopLevelDomain { get; set; }
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public List<string> CallingCodes { get; set; }
        public string Capital { get; set; }
        public List<string> AltSpellings { get; set; }
        public string Region { get; set; }
        public string SubRegion { get; set; }
        public int Population { get; set; }
        public List<double> LatLng { get; set; }
        public string Demonym { get; set; }
        public double? Area { get; set; }
        public object Gini { get; set; }
        public List<string> Timezones { get; set; }
        public List<object> Borders { get; set; }
        public string NativeName { get; set; }
        public string NumericCode { get; set; }
        public List<Currency> Currencies { get; set; }
        public List<Language> Languages { get; set; }
        public Translations Translations { get; set; }
        public string Flag { get; set; }
        public List<object> RegionalBlocs { get; set; }
        public string Cioc { get; set; }
    }

    #region dependent properties to the country entity
    public class Currency
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
    }

    public class Language
    {
        public string Iso6391 { get; set; }
        public string Iso6392 { get; set; }
        public string Name { get; set; }
        public string NativeName { get; set; }
    }

    public class Translations
    {
        public string De { get; set; }
        public string Es { get; set; }
        public string Fr { get; set; }
        public string Ja { get; set; }
        public string It { get; set; }
        public string Br { get; set; }
        public string Pt { get; set; }
        public string Nl { get; set; }
        public string Hr { get; set; }
        public string Fa { get; set; }
    } 

    #endregion
}
