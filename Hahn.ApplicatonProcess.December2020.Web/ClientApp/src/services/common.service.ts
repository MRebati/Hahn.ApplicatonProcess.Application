import { ApiService } from './../infrastructure/apiService';
import { country } from "./../models/country";
import { inject } from "aurelia-dependency-injection";
import { HttpClient } from "aurelia-fetch-client";

@inject(HttpClient)
export class CommonService extends ApiService {
  constructor(http: HttpClient) {
    super(http);
  }
  
  getCountry(): Promise<Array<country>> {
    return this.http
      .fetch("common/all-countries", {
        method: "get",
      })
      .then((response) => response.json())
      .then((countries: country[]) => {
        localStorage.setItem("valid-countries", JSON.stringify(countries));
        return countries;
      })

      .catch((error) => {
        console.log("an error occured on retrieving countires.", error);
        return [];
      });
  }
}
