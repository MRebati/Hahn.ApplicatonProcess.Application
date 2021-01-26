  export interface Currency {
      code: string;
      name: string;
      symbol: string;
  }

  export interface Language {
      iso639_1: string;
      iso639_2: string;
      name: string;
      nativeName: string;
  }

  export interface Translations {
      de: string;
      es: string;
      fr: string;
      ja: string;
      it: string;
      br: string;
      pt: string;
      nl: string;
      hr: string;
      fa: string;
  }

export interface country {
  name: string;
  topLevelDomain: string[];
  alpha2Code: string;
  alpha3Code: string;
  callingCodes: string[];
  capital: string;
  altSpellings: string[];
  region: string;
  subregion: string;
  population: number;
  latlng: number[];
  demonym: string;
  area: number;
  gini?: any;
  timezones: string[];
  borders: any[];
  nativeName: string;
  numericCode: string;
  currencies: Currency[];
  languages: Language[];
  translations: Translations;
  flag: string;
  regionalBlocs: any[];
  cioc: string;
}
